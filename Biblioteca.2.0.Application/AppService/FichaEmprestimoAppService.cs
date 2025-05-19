

using Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Domain.Services;
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

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> CadastreFicha(FichaEmprestimoAlunoDto dto)
        { 
 
            if (!dto.IsValid()) return dto.RetornarInconsistencia();

            return _servicoFichaEmprestimoAluno.CadastreFicha(dto.ObterEntidade());
            
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExcluaFicha(int FihaId)
        {           
            return _servicoFichaEmprestimoAluno.ExcluaFicha(FihaId);
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ExecuteEntregaDeLivro(int FichaId, int LivroId)
        {
           return _servicoFichaEmprestimoAluno.ExecuteEntregaDeLivro(FichaId, LivroId);
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> FinalizeFicha(FichaEmprestimoAluno dados)
        {
            return _servicoFichaEmprestimoAluno.FinalizeFicha(dados);
        }

        public FichaEmprestimoAluno ObtenhaFichaPorCodigo(int FichaId)
        {
            return _servicoFichaEmprestimoAluno.ObtenhaFichaPorCodigo(FichaId);
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
