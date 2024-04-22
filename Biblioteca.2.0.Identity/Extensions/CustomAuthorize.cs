using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace Biblioteca._2._0.Identity.Extensions
{
    public class CustomAuthorize
    {
        //Receber o contexto, o nome da claim e o valor da claim
        //verifica se está realmente autenticado
        //depois verifica se possui alguma claim e contem o valor que está exigindo
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

        //Herda do TypeFlterAttrbiute mas reeescre a forma de validação 
        //Setando uma base novas de uma classe que herda IAuthorizationFilter
        //Está substituindo a maneira traidicional de gerar a autenticação e autorização

        public class ClaimsAuthorizeAttibute : TypeFilterAttribute
        {
            public ClaimsAuthorizeAttibute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
            {
                Arguments = new object[] { new Claim(claimName, claimValue) };
            }
        }

        public class RequisitoClaimFilter : IAuthorizationFilter
        {
            private readonly Claim _claim;

            public RequisitoClaimFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult(401); //401 não te conheço caso não esteja autorizado/autenticado
                    return;
                }

                if(!CustomAuthorize.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    context.Result = new StatusCodeResult(403); //Caso saiba quem é o usuario mas não esteja autorizado a executa a ação 
                }
            }
        }

    }


}
