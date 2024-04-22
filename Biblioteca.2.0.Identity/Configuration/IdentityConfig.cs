using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Biblioteca._2._0.Identity.Configuration
{
    public static class IdentityConfig
    {

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

           services.AddDbContext<ApplicationDbContext>(
           options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
           //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Configuração de suporte ao Identity             
           services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>() //Adicionando as regras de perfil de usuario

                .AddErrorDescriber<IdentityMensagenPortugues>() //Implementa IdentityMensagenPortugues para sobre escrever os valores da classe, essa classe herda de IdentityErrorDescriber

                .AddEntityFrameworkStores<ApplicationDbContext>() //especificando que você vai trabalhar com este dbcontext para passar informacoes ao banco de dados no EF 

                .AddDefaultTokenProviders(); //Adicionado recursos, autenticação por email etc, Token que são gerados, criptografia para reconhecimento e autenticação 

            #region //JWT

            var appSettingsSection = configuration.GetSection("AppSettings"); //Pegou o nó do arquivo de configuração appSettings
            services.Configure<AppSettings>(appSettingsSection); //Configure o appsetting atraves do appseting section, configurando no middlaware/pipelane
                                                                  //para que a classe AppSettings represente os dados da sessão appSettingsSection vindo das configurações

            var appSettings = appSettingsSection.Get<AppSettings>(); //Obtem a AppSettings já populada
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); //Transforma a chave do token em uma sequencia de bytes para uso mais adiante

            //O padrão de autenticação depende da forma de implementação
            //neste caso o privider de autenticação que está sendo utilizado é AuthenticationScheme do Json web Token
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => //quando você usa AddJwtBearer, você está dizendo que está adicionado suporte para este tipo especifico de token

            {
                bearerOptions.RequireHttpsMetadata = false;

                bearerOptions.RequireHttpsMetadata = true; //requerer acesso pelo https por segurança 
                bearerOptions.SaveToken = true; //Esse cara que guarda o Token no httpContext para validar se está logado, dizer que o token vai ser guardado na estanci assim que o login for realizado com sucesso
                bearerOptions.TokenValidationParameters = new TokenValidationParameters //parametro de validação do token logo a baixo
                {
                    ValidateIssuerSigningKey = true, //Audiencia quer dizer onde o token pode ser utilizado, pode ser utilziado em dominio X ou Y, você vai validar com base na assinatura
                    IssuerSigningKey = new SymmetricSecurityKey(key), //Assinatura do emissor criada a partida da classe SymmetricSecurityKey
                    ValidateIssuer = true, //Issuer valido para que o token seja valida apenas para dentro das apis que você quiser, então sempre validar o emissor, não aceitar o token de outros emissores para as apis
                    ValidateAudience = true, //Validar para quais dominios esse token é válido

                    // ValidAudiences = appSettings.Emissor, //valida varios pontos de audiencia 

                    ValidAudience = appSettings.ValidoEm, //Criar a audiencia de para onde o token será valido
                    ValidIssuer = appSettings.Emissor //Issuer valido 

                    //Caso não seja o mesmo token dentro da mesma audiencia o token não vai ser válido para api

                };
            });
            #endregion


            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
