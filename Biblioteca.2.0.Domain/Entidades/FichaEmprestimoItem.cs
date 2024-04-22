using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;


namespace Biblioteca._2._0.Domain.Entidades
{
    public class FichaEmprestimoItem : EntidadeBase
    {
        public Guid Codigo { get; set; }

        public int FichaEmprestimoAlunoId { get; set; }

        public FichaEmprestimoAluno FichaEmprestimoAluno { get; set; }

        public int LivroId { get; set; }

        public Livro Livro { get; set; }

        public decimal Quantidade { get; set; }

        public FichaEmprestimoAlunoItensStatusEnum StatusItem { get; set; }

        public DateTime DataStatusItem { get; set; }

    }
}
