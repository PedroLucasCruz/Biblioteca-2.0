using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Domain.Interfaces.Service
{
    public interface IServicoLivro
    {
        IList<Livro> ObtenhaTodosLivros();

        Livro ObtenhaLivro(int Id);

        InconsistenciaDeValidacaoTipado<Livro> Cadastrar(Livro livro);

        InconsistenciaDeValidacaoTipado<Livro> Atualizar(Livro livro);

        InconsistenciaDeValidacaoTipado<Livro> Deletar(int Id);

    }
}
