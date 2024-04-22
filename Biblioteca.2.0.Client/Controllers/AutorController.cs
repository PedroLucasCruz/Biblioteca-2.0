using Biblioteca._2._0.Application.Dtos.Autores;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Metadados.VersaoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("Autores/v1")]
    public class AutorController : PrincipalController
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(ILogger<AutorController> logger, IAutorAppService autorAppService)
        {
            _logger = logger;
            _autorAppService = autorAppService;
        }

        /// <summary>
        /// Cadastra um Autor
        /// </summary>
        /// <param name="dto">DtoAutor</param>
        /// <returns>Autor Cadastrado</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar(CadastrarAutorDto dto)
        {
            var retorno = _autorAppService.Cadastrar(dto);
            return RespostaResponalizada(retorno);
        }

        /// <summary>
        /// Atualiza um Autor
        /// </summary>
        /// <param name="dto">DtoAutor</param>
        /// <returns>Autor Atualizado</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPut("Atualizar")]
        public IActionResult Atualizar(AlterarAutorDto dto)
        {
            var retorno = _autorAppService.Atualizar(dto);
            return RespostaResponalizada(retorno);
        }

        /// <summary>
        /// Remove um Autor
        /// </summary>
        /// <param name="id">AutorId -> Numérico</param>
        /// <returns>Retorno da Requisição</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [Authorize(Roles = Permissoes.ADMINISTRADOR)]
        [HttpDelete("Deletar{id}")]
        public IActionResult Deletar(int id)
        {
            var retorno = _autorAppService.Deletar(id);
            return RespostaResponalizada(retorno);
        }

        /// <summary>
        /// Obtenha um Autor por Id
        /// </summary>
        /// <param name="id">AutorId -> numérico</param>
        /// <returns>Dados do Autor</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var retorno = _autorAppService.ObterPorId(id);
            return RespostaResponalizada(retorno);
        }

        /// <summary>
        /// Obtem uma coleção com Todos Autores
        /// </summary>
        /// <returns>Coleção de Autores</returns>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var retorno = _autorAppService.ObterTodos();
            return RespostaResponalizada(retorno);
        }

    }
}
