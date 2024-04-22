using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;

namespace Biblioteca._2._0.Domain.Interfaces.Service
{
    public interface IServicoAutor
    {
        IList<Autor> ObterTodos();

        Autor ObterPorId(int Id);

        InconsistenciaDeValidacao Cadastrar(Autor dto);

        InconsistenciaDeValidacao Atualizar(Autor autor);

        InconsistenciaDeValidacao Deletar(int Id);
    }
}
