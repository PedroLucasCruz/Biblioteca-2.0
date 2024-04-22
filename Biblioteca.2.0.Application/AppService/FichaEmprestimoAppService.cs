

using Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;

namespace Biblioteca._2._0.Application.AppService
{
    public class FichaEmprestimoAppService : IFichaEmprestimoAlunoAppService
    {
        private readonly IServicoFichaEmprestimoAluno _servicoFichaEmprestimoAluno;

        public FichaEmprestimoAppService(IServicoFichaEmprestimoAluno servicoFichaEmprestimoAluno)
        {
            _servicoFichaEmprestimoAluno = servicoFichaEmprestimoAluno;
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> CadastreFicha(FichaEmprestimoAlunoDto dados)
        {

          return  _servicoFichaEmprestimoAluno.CadastreFicha(dados);

        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExcluaFicha(int FihaId)
        {
            throw new NotImplementedException();
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExecuteEntregaDeLivro(int FichaId, int LivroId)
        {
            throw new NotImplementedException();
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> FinalizeFicha(FichaEmprestimoAluno dados)
        {
            throw new NotImplementedException();
        }

        public FichaEmprestimoAluno ObtenhaFichaPorCodigo(int FichaId)
        {
            throw new NotImplementedException();
        }

        public IList<FichaEmprestimoAluno> ObtenhaFichasDoAlunoPorCodigo(int AlunoId, int limiteRegistros)
        {
            throw new NotImplementedException();
        }

        public IList<FichaEmprestimoAluno> ObtenhaFichasDoAlunoPorCodigoEhIntervaloEhSituacao(int AlunoId, DateTime DataInicial, DateTime DataFinal, FichaEmprestimoAlunoStatusEnum situacao, int limiteRegistros)
        {
            throw new NotImplementedException();
        }

        public IList<FichaEmprestimoAluno> ObtenhaFichasEmAtrasoDeEntregaPorIntervalo(DateTime DataInicial, DateTime DataFinal, int limiteRegistros)
        {
            throw new NotImplementedException();
        }

        public IList<FichaEmprestimoAluno> ObtenhaTodasFichas()
        {
            throw new NotImplementedException();
        }
    }
}
