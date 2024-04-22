using Biblioteca._2._0.Application.Dtos.Autores;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface IAutorAppService
    {      
            IList<Autor> ObterTodos();

            Autor ObterPorId(int Id);

            InconsistenciaDeValidacao Cadastrar(CadastrarAutorDto dto);

            InconsistenciaDeValidacao Atualizar(AlterarAutorDto dto);

            InconsistenciaDeValidacao Deletar(int Id);        
    }
}
