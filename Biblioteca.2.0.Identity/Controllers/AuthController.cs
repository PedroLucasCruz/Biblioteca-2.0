using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteca._2._0.Identity.Controllers
{
    [ApiController] //Usado para liberar os entendimento dos schemas do swagger, decorando com está anotação você esta dizendo que ela é uma api controller, o schemas trafegam json e não formulario
    [Route("api/identidade/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
       

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings //IOption é uma opção de leitura que aspnet da como suporte, possui o valor que é exposto atraves da classe de representação que no caso é o AppSettings
          
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value; //Pega as configurações do AppSettings.Json em tempo de execução 
          
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
          
            if (!ModelState.IsValid) //Validação da model de registro de usuário
                return CustomResponse(ModelState); //Custom response de dentro da MainController
          
            var user = new IdentityUser //Estancia do identity recebendo no UserName o Email para que o usuario utilze o email para autenticação no usuario
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);
            if (result.Succeeded)
            {
             
               // await _signInManager.SignInAsync(user, false);//Não estamos fazendo login, apenas gerando login, então este metodo está fazendo o login na aplicãção e não precisa chamar
                return CustomResponse(await GerarJwt(usuarioRegistro.Email)); //Se funcionar chegando nesta linha retorna 200Ok sucesso e gera o Token 
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return CustomResponse(); //Caso o result succeded não funcione retorna 
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState); //Validação da model de registro de usuário

            var result = await _signInManager.PasswordSignInAsync(
                usuarioLogin.Email, //Parametro para validar pelo Email/Usuario
                usuarioLogin.Senha, //Parametro para  validar pela senha 
                false, //parametro para dizer se o usuario é persistente 
                true //parametro para bloquear o usuário caso faça muitas tentativas de login, desbloqueia o usuario depois de 5min
                );

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuario temporariamente bloqueado por tentativa inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpGet("deslogar")]
        public async Task<ActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
           
            return CustomResponse();
        }

        //Retornar o Usuario logado
        //Retornar qualquer dado do usuário deve ser passado o bearer token na autorização para retornar os dados
        //Se o Bearer token não for passado não retorna nada
        [Authorize]
        [HttpPost("retornar-usuario-logado")]
        public async Task<ActionResult> GetUserLogin()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (HttpContext.User.Identity.IsAuthenticated)
            return CustomResponse(user);               
            return CustomResponse();         
        }


        //Quando este metodo de Gerar JWT for chamado estará em um fluxo em que o usuário já foi autenticado
        //Portanto para esse fluxo não tem por que verificar se foi encontrado ou não.
        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email); //buscando usuário 
            var claims = await _userManager.GetClaimsAsync(user); //Gerando lista de claims

            var identityClaims = await ObterClaimsUsuario(claims, user); //Obter as claims 
            var encodedToken = CodificarToken(identityClaims); //Codificar o Token

          return  ObterRespostaToken(encodedToken, user, claims);

        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);


            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //valor unico para o token
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //Quando o token vai expirar
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //Quando o token foi emitido

            //Roles: Um papel
            //Claims: Um dado aberto, pode representar tanto uma permissão quanto um dado do usuário
            //Dentro de Claims é acrescentado as roles(papeis) do usuário represetados pelo nome "role"
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            //Estancia do identity com todas as claims em um lugar só
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret); //Gera uma squencia de bites para gerar a chave 

            //Criando o Token
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor, //Quem é o emissor 
                Audience = _appSettings.ValidoEm, //Audiencia 
                Subject = identityClaims, //Dados do usuário, coleção de claims 
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras), //expiração em horas no formato utc, duas hora pra frente no padrão utc
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //chave por parametro e o tipo de algoritmo de cryptografia 
            });

            //gera o token codificado com base na chave(key)
            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            //Preenchar  a resposta  
            var response = new UsuarioRespostaLogin
            {
                AcessToken = encodedToken, //O proprio token codificado 
                ExpireIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds, //pegando os segundo totais da hora exata da geração do token, jogando duas hora a frente e retornando em segundos para saber exatamente quando o token vai expirar  
                UsuarioToken = new UsuarioToken //
                {
                    Id = user.Id, //Id
                    Email = user.Email, //Email 
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value }) //Passando claims do usuário para manipular em json 
                }
            };

            return response;
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
       
    } 
}
