using Biblioteca._2._0.Application.Dtos.Usuarios;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Metadados.VersaoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Usuarios/v1")]
    public class UsuarioController : PrincipalControllerTipado<UsuarioController>// ControladorAbstratoComContexto<UsuarioController>
    {
        private readonly IUsuarioAppService _servicoUsuario;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioAppService servicoUsuario)
        {
            _logger = logger;
            _servicoUsuario = servicoUsuario;
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="dto">DtoUsuario</param>
        /// <returns>Dados Atualizados</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [Authorize(Roles = Permissoes.ADMINISTRADOR)]
        [HttpPut("Atualizar")]
        public IActionResult Atualizar(UsuarioDto dto)
        {
               var resposta = _servicoUsuario.AtualizeUsuario(dto);

                _logger.LogInformation("Retornando Cadastro do Usuário");
                return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Remove um Usuário Pelo Id
        /// </summary>
        /// <param name="Id">UsuarioId</param>
        /// <returns>Resposta da Requisição</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [Authorize(Roles = Permissoes.ADMINISTRADOR)]
        [HttpDelete("Deletar/{Id}")]
        public IActionResult Deletar(int Id)
        {
            try
            {
                _logger.LogInformation("Iniciando a busca das informações");
            

                    var res = _servicoUsuario.DeleteUsuario(Id);
                    return RespostaResponalizada(res);
             

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao deletar informações", ex);
                return StatusCode(500, new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Obtem uma Coleção de Usuarios
        /// </summary>
        /// <returns>Coleção de Usuários</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObtenhaTodos")]
        public IActionResult ObtenhaTodos()
        {
                _logger.LogInformation("Iniciando a busca das informações");
                var resultado = _servicoUsuario.ObtenhaTodosUsuarios();
                return RespostaResponalizada(resultado);
        }

        /// <summary>
        /// Obtem um Usuario Pelo Id
        /// </summary>
        /// <param name="Id">UsuarioId -> numérico</param>
        /// <returns>Dados do Usuario</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObtenhaUsuario/{Id}")]
        public IActionResult ObtenhaUsuario(int Id)
        {
            var retorno = _servicoUsuario.ObterUsuarioId(Id);
            return RespostaResponalizada(retorno);
        }

    }
}
