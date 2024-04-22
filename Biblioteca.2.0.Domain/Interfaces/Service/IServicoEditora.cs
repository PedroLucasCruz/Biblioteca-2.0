using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Domain.Interfaces.Service
{
    public interface IServicoEditora
    {
        IList<Editora> ObterTodos();

        Editora ObterPorId(int Id);

        InconsistenciaDeValidacao Cadastrar(Editora dto);

        InconsistenciaDeValidacao Atualizar(Editora dto);

        InconsistenciaDeValidacao Deletar(int Id);

    }
}
