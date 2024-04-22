using Biblioteca._2._0.Application.Dtos.Editoras;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;



namespace Biblioteca._2._0.Application.AppService
{
    public class EditoraAppService : IEditoraAppService
    {
        private readonly IServicoEditora _servicoEditora;
        public InconsistenciaDeValidacao Atualizar(AlterarEditoraDto dto)
        {
           if (!dto.IsValid()) return dto.RetornarInconsistencia();

          return  _servicoEditora.Atualizar(dto.ObterEntidade());
        }

        public InconsistenciaDeValidacao Cadastrar(CadastroEditoraDto dto)
        {
            if (!dto.IsValid()) return dto.RetornarInconsistencia();

           return _servicoEditora.Cadastrar(dto.ObterEntidade());
        }

        public InconsistenciaDeValidacao Deletar(int Id)
        {
            return _servicoEditora.Deletar(Id);
        }

        public Editora ObterPorId(int Id)
        {
          return _servicoEditora.ObterPorId(Id);
        }

        public IList<Editora> ObterTodos()
        {
            return _servicoEditora.ObterTodos();
        }
    }
}
