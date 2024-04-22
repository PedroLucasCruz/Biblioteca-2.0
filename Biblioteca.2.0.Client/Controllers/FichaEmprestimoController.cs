using Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Extension.Utils;
using Biblioteca._2._0.Metadados.VersaoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("FichaEmprestimos/v1")]
    public class FichaEmprestimoController : PrincipalControllerTipado<FichaEmprestimoAlunoDto>
    {
        private readonly IFichaEmprestimoAlunoAppService _fichaEmprestimoAlunoAppService;
        private const int LIMITE_DE_REGISTRO_PADRAO = 100;

        public FichaEmprestimoController(ILogger<FichaEmprestimoController> logger, IFichaEmprestimoAlunoAppService fichaEmprestimoAlunoAppService)
        {
            _logger = logger;
            _fichaEmprestimoAlunoAppService = fichaEmprestimoAlunoAppService;
        }

        /// <summary>
        /// Cadastra a Ficha de Emprestimo do Aluno
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Ficha cadastrada do Aluno</returns>
        ///<remarks>
        ///JSON - Objeto para cadastro padrão.
        ///ENUMERADORES: StatusItem => (1-ENTREGUE, 2-A_ENTREGAR), statusEmprestimoDescricao: texto (NORMAL, ATRASADA, ENTREGUE)
        /// </remarks>
        [HttpPost("Cadastrar")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult Cadastrar(FichaEmprestimoAlunoDto dto)
        {
            var resposta = _fichaEmprestimoAlunoAppService.CadastreFicha(dto);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Finalizar a Ficha de Emprestimo do Aluno
        /// </summary>
        /// <param name="FichaId"></param>
        /// <returns>Objeto da Ficha de Emprestimo</returns>
        /// <remarks>
        /// Parametro: Id tipo numérico
        /// </remarks>
        [HttpPost("Finalizar/{FichaId:int}")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult Finalizar(int FichaId)
        {
            var ficha = _fichaEmprestimoAlunoAppService.ObtenhaFichaPorCodigo(FichaId);
            if (!ficha.PossuiValor()) { return RespostaResponalizada(ficha); }


            var resposta = _fichaEmprestimoAlunoAppService.FinalizeFicha(ficha);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Executa a entrega de um livro contida em uma Ficha de Emprestimo
        /// </summary>
        /// <param name="LivroId"></param>
        /// <param name="FichaId"></param>
        /// <returns>Resultado da operação</returns>
        [HttpPost("EntregarLivro")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult EntregarLivro([FromQuery] int LivroId, [FromQuery] int FichaId)
        {
            var resposta = _fichaEmprestimoAlunoAppService.ExecuteEntregaDeLivro(LivroId, FichaId);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Exclui a Ficha de Emprestimo
        /// </summary>
        /// <param name="FichaId"></param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("ExcluirFicha/{FichaId:int}")]
        [Authorize]
        public IActionResult ExcluirFicha(int FichaId)
        {
            var resposta = _fichaEmprestimoAlunoAppService.ExcluaFicha(FichaId);
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// Obtem todas as fichas
        /// </summary>
        /// <returns>Coleção com todas as fichas</returns>
        [HttpGet("ObtenhaTodasFichas")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaTodasFichas()
        {
            var resposta = _fichaEmprestimoAlunoAppService.ObtenhaTodasFichas();
            return RespostaResponalizada(resposta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="DataFim"></param>
        /// <param name="AlunoId"></param>
        /// <param name="Situacao"></param>
        /// <returns></returns>
        [HttpGet("ObtenhaFichasDoAlunoNoIntervalo")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaTodosFichasDoAlunoNoIntervalo([FromQuery] string DataInicio,
                                                                  [FromQuery] string DataFim,
                                                                  [FromQuery] int AlunoId,
                                                                  [FromQuery] int Situacao)
        {

            DateTime dateIni;
            DateTime dateFim;

            if (!DateTime.TryParse(DataInicio, out dateIni) || !DateTime.TryParse(DataFim, out dateFim))
            {
                return RespostaResponalizada(null);
            }

            var resposta = _fichaEmprestimoAlunoAppService.ObtenhaFichasDoAlunoPorCodigoEhIntervaloEhSituacao(AlunoId, dateIni, dateFim, (FichaEmprestimoAlunoStatusEnum)Situacao, LIMITE_DE_REGISTRO_PADRAO);
            return RespostaResponalizada(resposta);
        }


        /// <summary>
        /// Obtem as fichas no intervalo em atraso de entrega a 8 dias
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="DataFim"></param>
        /// <returns>Coleção com Fichas em atraso</returns>
        [HttpGet("ObtenhaFichasNoIntervaloEmAtraso")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaFichasNoIntervaloEmAtraso([FromQuery] string DataInicio,
                                                                 [FromQuery] string DataFim)
        {

            DateTime dateIni;
            DateTime dateFim;

            if (!DateTime.TryParse(DataInicio, out dateIni) || !DateTime.TryParse(DataFim, out dateFim))
            {
                return RespostaResponalizada(null);
            }

            var resposta = _fichaEmprestimoAlunoAppService.ObtenhaFichasEmAtrasoDeEntregaPorIntervalo(dateIni, dateFim, LIMITE_DE_REGISTRO_PADRAO);
            return RespostaResponalizada(resposta);
        }


        /// <summary>
        /// Obtem as fichas do aluno
        /// </summary>
        /// <param name="AlunoId"></param>
        /// <returns>Fichas do aluno</returns>
        /// <remarks>Parametro: Id tipo numérico</remarks>
        [HttpGet("ObtenhaAsFichasDoAluno/{AlunoId:int}")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaAsFichasDoAluno(int AlunoId)
        {
            var resposta = _fichaEmprestimoAlunoAppService.ObtenhaFichasDoAlunoPorCodigo(AlunoId, LIMITE_DE_REGISTRO_PADRAO);
            return RespostaResponalizada(resposta);

        }

        /// <summary>
        /// Obtem a ficha
        /// </summary>
        /// <param name="FichaId"></param>
        /// <returns>Ficha Emprestimo</returns>
        /// <remarks>Parametro: Id tipo numérico</remarks>
        [HttpGet("ObtenhaFicha/{FichaId:int}")]
        [Authorize]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult ObtenhaFicha(int FichaId)
        {
            var resposta = _fichaEmprestimoAlunoAppService.ObtenhaFichaPorCodigo(FichaId);
            return RespostaResponalizada(resposta);

        }

    }
}
