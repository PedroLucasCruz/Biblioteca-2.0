using Biblioteca._2._0.Application.Dtos.Livros;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;



namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface ILivroAppService
    {
        IList<Livro> ObtenhaTodosLivros();

        Livro ObtenhaLivro(int Id);

        InconsistenciaDeValidacaoTipado<Livro> Cadastrar(LivroDto livro);

        InconsistenciaDeValidacaoTipado<Livro> Atualizar(LivroDto livro);

        InconsistenciaDeValidacaoTipado<Livro> Deletar(int Id);
    }
}
