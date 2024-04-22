using Biblioteca._2._0.Application.Dtos.Alunos;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Extension.Utils;
using Biblioteca._2._0.Metadados.VersaoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("Alunos/v1")]
    public class AlunoController : PrincipalController
    {
        private readonly IAlunoAppService alunoAppService;

        public AlunoController(IAlunoAppService servicoAluno, ILogger<AlunoController> logger)
        {
            base._logger = logger;
            alunoAppService = servicoAluno;
        }

        /// <summary>
        /// Cadastro do Aluno
        /// </summary>
        /// <param name="dto">Dados para Cadastro</param>
        /// <returns>Dados do Aluno Cadastrado</returns>
        /// <remarks>DtoAluno</remarks>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar(AlunoDto dto)
        {
            var resposta = alunoAppService.CadastreAluno(dto);
            return RespostaResponalizada(resposta);

        }

        /// <summary>
        /// Atualiza um Cadastro de Aluno
        /// </summary>
        /// <param name="dto">Dados para Atualização</param>
        /// <returns>Dados do Aluno Atualizado</returns>
        /// <remarks>DtoAluno</remarks>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpPut("Atualizar")]
        public IActionResult Atualizar(AlunoDto dto)
        {
            var resposta = alunoAppService.AtualizeAluno(dto);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Remove um Aluno Cadastrado
        /// </summary>
        /// <param name="Id">AlunoId numérico</param>
        /// <returns>Resultado da Operação</returns>
        /// <remarks>AlunoId -> numérico</remarks>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [Authorize(Roles = Permissoes.ADMINISTRADOR)]
        [HttpDelete("Deletar/{Id}")]
        public IActionResult Deletar(int Id)
        {
            try
            {
                _logger.LogInformation("Iniciando a busca das informações");
                var resultado = alunoAppService.ObtenhaAluno(Id);
                if (resultado.PossuiValor())
                {

                    _logger.LogInformation("Deletando o aluno");

                    var res = alunoAppService.DeleteAluno(Id);

                    return res.PossuiValor()
                        ? StatusCode(204, new { Informacao = res })
                        : StatusCode(200, new { Informacao = "Aluno deletado" });

                }
                else
                {
                    _logger.LogInformation("Sem informacoes na busca das informações");
                    return StatusCode(204, new { Informacao = "Não foi encontrado registros" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao deletar informações", ex);
                return StatusCode(500, new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Obtem uma Coleção com Todos os Alunos Cadastrados
        /// </summary>
        /// <returns>Coleção com Todos Alunos</returns>
        /// <remarks>Sem parâmtros</remarks>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObtenhaTodosAlunos")]
        public IActionResult ObtenhaTodosAlunos()
        {
            try
            {
                _logger.LogInformation("Iniciando a busca das informações");
                var resposta = alunoAppService.ObtenhaTodosAlunos();


                if (resposta.PossuiValor() && resposta.PossuiLinhas())
                {
                    _logger.LogInformation("Convertendo as informações");
                    List<AlunoDto> lista = new List<AlunoDto>();

                    resposta.ToList().ForEach(x => lista.Add(new AlunoDto() { Id = x.Id, Nome = x.Nome, Email = x.Email, Telefone = x.Telefone }));

                    _logger.LogInformation("Retornando as informações");
                    return StatusCode(200, lista);
                }
                else
                {
                    _logger.LogInformation("Sem informações");
                    return StatusCode(204, new { Inconsistencia = "Sem dados para retornar." });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no serviço de busca", ex);
                return StatusCode(500, new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Obtem um Aluno pelo Id
        /// </summary>
        /// <param name="Id">AlunoId numérico</param>
        /// <returns>Aluno Cadastrado</returns>
        /// <remarks>AlunoId -> numérico</remarks>
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        [HttpGet("ObtenhaAluno/{Id}")]
        public IActionResult ObtenhaAluno(int Id)
        {
          var resposta = alunoAppService.ObtenhaAluno(Id);
          return  RespostaResponalizada(resposta);
        }
    }
}
