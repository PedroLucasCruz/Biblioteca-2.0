using Biblioteca._2._0.Domain.Enumeradores.Livros;


namespace Biblioteca._2._0.Domain.Entidades
{
    public class Livro : EntidadeBase
    {
        public Guid Codigo { get; set; }

        public string Titulo { get; set; }

        public string SubTitulo { get; set; }

        public decimal QuantidadeEstoque { get; set; }

        public StatusLivroEnum Status { get; set; }

        public IList<LivroAutores> Autores { get; set; }

        public int EditoraId { get; set; }

        public Editora Editora { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

    }
}
