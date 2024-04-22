using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Biblioteca._2._0.Client.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                var nomeXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, nomeXml);


                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca API", Version = "v1.0" });

                c.IncludeXmlComments(caminhoArquivo);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                {
                                                    {
                                                        new OpenApiSecurityScheme
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Type = ReferenceType.SecurityScheme,
                                                                Id = "Bearer"
                                                            }
                                                        },
                                                        Array.Empty<string>()
                                                    }
                                                });
            });

        }
    }
}
