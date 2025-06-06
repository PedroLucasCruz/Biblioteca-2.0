﻿using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Extension.Utils;
using Biblioteca._2._0.Infra.Data.JWT.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca._2._0.Infra.Data.JWT.Servico
{
    public class ServicoDeToken : IServicoDeToken
    {
        private readonly IConfiguration _configuracao;
        private readonly ILogger<ServicoDeToken> _logger;

        public ServicoDeToken(IConfiguration configuracao, ILogger<ServicoDeToken> logger)
        {
            _configuracao = configuracao;
            _logger = logger;
        }

        public string GerarToken(Usuario usuario)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Secretkey = Encoding.ASCII.GetBytes(_configuracao.GetSection("Secret").Value);
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                //dados do usuario passados no token
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("Nome", usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Permissao.ToString()),
                    new Claim("Id", usuario.Id.ToString())
                }),
                //validade do token
                Expires = DateTime.Now.AddHours(8),

                //credencial do token (chave secreta)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Secretkey),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            SecurityToken TokenNovo;

            try
            {
                TokenNovo = TokenHandler.CreateToken(TokenDescriptor);
                return TokenHandler.WriteToken(TokenNovo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar Token", ex.Message);
                return "erro";
            }

        }

        public string RenoveToken(string token)
        {

            string tokenNovo = "";
            var TokenHandler = new JwtSecurityTokenHandler();
            var Secretkey = Encoding.ASCII.GetBytes(_configuracao.GetSection("Secret").Value);            
            var tokenvalidationparameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secretkey),
                ValidateIssuer = false,
                ValidateAudience = false
                
            };
            
            var ret = TokenHandler.ValidateToken(token, tokenvalidationparameters, out var tokenValidado);

            Usuario usuario = new Usuario();

            if (ret.PossuiValor()) 
            {
                foreach (var claim in ret.Claims)
                {
                    if (claim.Type.Equals("Nome")) 
                    {
                        usuario.Nome = claim.Value.ToString();
                    }
                    if (claim.Type.Contains("/identity/claims/role"))
                    {  
                        foreach ( UsuarioPermissaoEnum tipo in Enum.GetValues(typeof(UsuarioPermissaoEnum)))
                        {
                            if (tipo.ToString().Equals(claim.Value.ToString())) 
                            {
                                usuario.Permissao = tipo;
                            }
                        }
                    }
                    if (claim.Type.Equals("Id"))
                    {
                        usuario.Id = int.Parse(claim.Value.ToString());
                    }
                }

                tokenNovo = GerarToken(usuario);
            }
           
            return tokenNovo;
        }

        public bool ValidarToken(string token)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Secretkey = Encoding.ASCII.GetBytes(_configuracao.GetSection("Secret").Value);
            
            SecurityToken tokenInformado = TokenHandler.ReadToken(token);

            
            var tokenvalidationparameters = new TokenValidationParameters()
            {
               IssuerSigningKey = new SigningCredentials(
                    new SymmetricSecurityKey(Secretkey),
                    SecurityAlgorithms.HmacSha256Signature).Key,
            };

          
            var ret = TokenHandler.ValidateToken(token, tokenvalidationparameters, out var tokenValidado);


            return ret.Identity.IsAuthenticated;
        }
    }
}
