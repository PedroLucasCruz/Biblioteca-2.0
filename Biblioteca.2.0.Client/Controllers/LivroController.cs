using Biblioteca._2._0.Application.Dtos.Livros;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Enumeradores.Livros;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Metadados.VersaoApi;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Livros/v1")]
    public class LivroController : PrincipalController
    {
        private readonly ILivroAppService _servicoLivro;

        public LivroController(ILivroAppService servicoLivro, ILogger<LivroController> logger)
        {
            _logger = logger;
            _servicoLivro = servicoLivro;
        }

        /// <summary>
        /// Cadastra um Livro
        /// </summary>
        /// <param name="dto">DtoLivro</param>
        /// <returns>Dados Cadastrados</returns>
        [HttpPost("Cadastrar")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult Cadastre(LivroDto dto)
        {
            dto.TipoOperacaoDeDadosEnum = LivrosTipoOperacaoDeDadosEnum.CADASTRAR;
            var resposta = _servicoLivro.Cadastrar(dto);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Atualiza um Livro
        /// </summary>
        /// <param name="dto">DtoLivro</param>
        /// <returns>Dados Atualizados</returns>
        [HttpPut("Atualizar")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult Atualize(LivroDto dto)
        {
            dto.TipoOperacaoDeDadosEnum = LivrosTipoOperacaoDeDadosEnum.ALTERAR;
            var resposta = _servicoLivro.Cadastrar(dto);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Remove um Livro por Id
        /// </summary>
        /// <param name="id">LivroId -> numérico</param>
        /// <returns>Resposta da Reqquisição</returns>
        [HttpDelete("Deletar/{Id}")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult Delete(int id)
        {
            var resposta = _servicoLivro.Deletar(id);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Obtem um Livro pelo Id
        /// </summary>
        /// <param name="Id">LivroId -> numérico</param>
        /// <returns></returns>
        [HttpGet("Obtenha/{Id}")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaLivro(int Id)
        {
            var resposta = _servicoLivro.ObtenhaLivro(Id);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Obtem uma Coleção de Livros
        /// </summary>
        /// <returns>Coleção de Livros</returns>
        [HttpGet("ObtenhaTodos")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaTodosLivros()
        {
            var resposta = _servicoLivro.ObtenhaTodosLivros();
            return RespostaResponalizada(resposta);
        }
    }
}
