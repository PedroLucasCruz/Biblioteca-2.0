

namespace Biblioteca._2._0.Domain.Entidades
{
    public class Editora : EntidadeBase
    {
        public Editora() { }

        public string Cnpj { get; set; }

        public string Nome { get; set; }

        public string Cidade { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}
