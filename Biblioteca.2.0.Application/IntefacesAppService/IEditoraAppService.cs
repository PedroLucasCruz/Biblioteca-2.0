using Biblioteca._2._0.Application.Dtos.Editoras;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface IEditoraAppService
    {
        IList<Editora> ObterTodos();

        Editora ObterPorId(int Id);

        InconsistenciaDeValidacao Cadastrar(CadastroEditoraDto dto);

        InconsistenciaDeValidacao Atualizar(AlterarEditoraDto dto);

        InconsistenciaDeValidacao Deletar(int Id);
    }
}
