using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;


namespace Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos
{
    public class FichaEmprestimoAlunoItensDto : EntidadeBase
    {
        public int LivroId { get; set; }

        public decimal Quantidade { get; set; }

        public FichaEmprestimoAlunoItensStatusEnum StatusItem { get; set; }

    }
}
