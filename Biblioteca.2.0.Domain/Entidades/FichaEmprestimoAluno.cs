using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;


namespace Biblioteca._2._0.Domain.Entidades
{
    public class FichaEmprestimoAluno : EntidadeBase
    {
    
        public Guid Codigo { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }

        public string Observacoes { get; set; }

        public FichaEmprestimoAlunoStatusEnum StatusEmprestimo { get; set; }

        public IList<FichaEmprestimoItem> FichaEmprestimoItens { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime DataEntregaEmprestimo { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

  
    }
}
