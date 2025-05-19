using Biblioteca._2._0.Application.Dtos.Usuarios;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Metadados.VersaoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.API.Controllers
{

    /// <summary>
    /// Porta de entrada para autenticação.
    /// </summary>
    [ApiController]
    [Route("Autenticacao/v1")]
    public class AutenticacaoController : PrincipalController
    {
       
        private readonly IUsuarioAppService _appServiceUsuario;

        public AutenticacaoController(ILogger<AutenticacaoController> logger, IUsuarioAppService servicoUsuario)
        {
            _logger = logger;          
            _appServiceUsuario = servicoUsuario;
        }

        /// <summary>
        /// Autêntica um Usuário
        /// </summary>
        /// <param name="dto">Dados do Usuário</param>
        /// <returns>Token de Autenticação JWT</returns>
        /// <remarks>
        /// Observação: Deve informar apenas Nome e Senha.
        /// {    
        ///     "nome": "string",  
        ///     "senha": "string", 
        /// }
        /// </remarks>
        [AllowAnonymous]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPost("AutenticarUsuario")]
        public IActionResult Autentique(UsuarioAutDto dto)
        {
            var retorno = _appServiceUsuario.AutentiqueUsuario(dto);
            return RespostaResponalizada(retorno);
        }


        /// <summary>
        /// Cadastra um usuário para Acesso do Sistema
        /// </summary>
        /// <param name="dto">Dados do Cadastro</param>
        /// <returns>Dados cadastrados</returns>
        /// <remarks>
        /// Observação: Permissões = (2 - OPERADOR, 1- ADMINISTRADOR )
        /// 
        /// {   
        ///     "nome": "string",  
        ///     "email": "string",  
        ///     "senha": "string",  
        ///     "permissao": 1
        /// }
        /// </remarks>
        [AllowAnonymous]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPost("CadastrarUsuario")]
        public IActionResult Cadastre(UsuarioDto dto)
        {
            var resposta = _appServiceUsuario.Cadastrar(dto);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Renova o tokem passado como paramêtro
        /// </summary>
        /// <param name="token">Token para renovação</param>
        /// <returns>String Token Renovado</returns>
        [AllowAnonymous]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPost("RenovarToken/{token}")]
        public IActionResult RenovarToken(string token)
        {
            var resposta = _appServiceUsuario.RenoveToken(token);

            return RespostaResponalizada(resposta);
        }
    }
}
