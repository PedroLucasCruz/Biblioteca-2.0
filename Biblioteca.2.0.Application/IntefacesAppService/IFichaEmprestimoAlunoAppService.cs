using Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface IFichaEmprestimoAlunoAppService
    {
        /// <summary>
        /// Cadastro de Nova Ficha de Emprestimo
        /// </summary>
        /// <param name="dados">Os Dados para Validação e Cadastro da Ficha</param>
        /// <returns>InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno></returns>
        InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> CadastreFicha(FichaEmprestimoAlunoDto dados);

        /// <summary>
        /// Exclui a Ficha de Emprestimo
        /// </summary>
        /// <param name="FichaId">Os Dados para Validação e Exclusão da Ficha</param>
        /// <returns>InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno></returns>
        InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> FinalizeFicha(FichaEmprestimoAluno dados);

        /// <summary>
        /// Executa a Entrega do Livro Individual da Ficha
        /// </summary>
        /// <param name="FichaId">Identificação da Ficha</param>
        /// <param name="LivroId">Identificação do Livro</param>
        /// <returns>InconsistenciaDeValidacaoTipado</returns>
        InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExecuteEntregaDeLivro(int FichaId, int LivroId);

        /// <summary>
        /// Finaliza a Ficha de Emprestimo do Aluno
        /// </summary>
        /// <param name="dados">Os Dados Para Finalização da Ficha De Emprestimo</param>
        /// <returns>InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno></returns>
        InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExcluaFicha(int FihaId);

        /// <summary>
        /// Obtem a Ficha pelo Código
        /// </summary>
        /// <param name="FichaId"></param>
        /// <returns>Ficha de Emprestimo</returns>
        FichaEmprestimoAluno ObtenhaFichaPorCodigo(int FichaId);

        /// <summary>
        /// Obtem uma coleção de Fichas pelo código do aluno e limite de registros
        /// </summary>
        /// <param name="AlunoId">Identificação do Aluno</param>
        /// <param name="limiteRegistros">Limite de Registros Retornados</param>
        /// <returns>Lista de Ficha de Emprestimo</returns>
        IList<FichaEmprestimoAluno> ObtenhaFichasDoAlunoPorCodigo(int AlunoId, int limiteRegistros);

        /// <summary>
        /// Obtem as fichas do aluno seguindo os critérios
        /// </summary>
        /// <param name="AlunoId">Identificação do Aluno</param>
        /// <param name="DataInicial">Data Inicial de Cadastro</param>
        /// <param name="DataFinal">Data Final de Cadastro</param>
        /// <param name="situacao">Situação da Ficha</param>
        /// <param name="limiteRegistros">Limite de Registros Retornados</param>
        /// <returns>Lista de ficha de Alunos</returns>
        IList<FichaEmprestimoAluno> ObtenhaFichasDoAlunoPorCodigoEhIntervaloEhSituacao(int AlunoId,
                                                                                        DateTime DataInicial,
                                                                                        DateTime DataFinal,
                                                                                        FichaEmprestimoAlunoStatusEnum situacao,
                                                                                        int limiteRegistros);

        /// <summary>
        /// Obtem as fichas atrasadas de entrega em 8 dias corridos
        /// </summary>
        /// <param name="DataInicial">Data Inicial de Cadastro</param>
        /// <param name="DataFinal">Data final de cadastro</param>
        /// <param name="limiteRegistros">LImite de registros retornados</param>
        /// <returns>Lista de Fichas em Atraso</returns>
        IList<FichaEmprestimoAluno> ObtenhaFichasEmAtrasoDeEntregaPorIntervalo(DateTime DataInicial, DateTime DataFinal, int limiteRegistros);

        /// <summary>
        /// Obtem uma coleção total das Fichas de Cadastro
        /// </summary>
        /// <returns>Lista com todas as fichas</returns>
        IList<FichaEmprestimoAluno> ObtenhaTodasFichas();

    }
}
