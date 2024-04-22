using Biblioteca._2._0.Application.Dtos.Autores;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.AppService
{
    internal class AutorAppService : IAutorAppService
    {
        private readonly IServicoAutor _serviceAutor;

        public AutorAppService(IServicoAutor serviceAutor)
        {
            _serviceAutor = serviceAutor;
        }

        public InconsistenciaDeValidacao Atualizar(AlterarAutorDto dto)
        {
            if (!dto.IsValid()) return dto.RetornarInconsistencia();
           
            return _serviceAutor.Atualizar(dto.ObterEntidade());
        }

        public InconsistenciaDeValidacao Cadastrar(CadastrarAutorDto dto)
        {
            if (!dto.IsValid()) return dto.RetornarInconsistencia();


            return _serviceAutor.Cadastrar(dto.ObterEntidade());
        }

        public InconsistenciaDeValidacao Deletar(int Id)
        {
           return _serviceAutor.Deletar(Id);
        }

        public Autor ObterPorId(int Id)
        {
            return _serviceAutor.ObterPorId(Id);
        }

        public IList<Autor> ObterTodos()
        {
            return _serviceAutor.ObterTodos();
        }
    }
}
