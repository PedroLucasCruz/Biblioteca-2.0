using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoAutor : IServicoAutor
    {
        private readonly IAutorRepositorio _autoresRepositorio;

        private readonly ILivroAutoresRepositorio _livroAutoresRepositorio;

        public ServicoAutor(IAutorRepositorio autoresRepositorio, ILivroAutoresRepositorio livroAutoresRepositorio)
        {
            _autoresRepositorio = autoresRepositorio;
            _livroAutoresRepositorio = livroAutoresRepositorio;
        }
      
        public InconsistenciaDeValidacao Atualizar(Autor autor)
        {

            var autorRetorno = ObterPorId(autor.Id);

            if (!autorRetorno.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = "Não registrado" };

            _autoresRepositorio.Altere(autorRetorno);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }

        public InconsistenciaDeValidacao Cadastrar(Autor autor)
        {           

            _autoresRepositorio.Cadastre(autor);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }

        public InconsistenciaDeValidacao Deletar(int id)
        {
            var autor = ObterPorId(id);

            if (!autor.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = "Não registrado" };

            var livrosAutores = _livroAutoresRepositorio.ConsultarLivroAutoresPorIdAutor(id);

            if (livrosAutores.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = $"Existem livros relacionados a este autor, ele não pode ser deletado" };

            _autoresRepositorio.Delete(autor.Id);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }

        public Autor ObterPorId(int Id)
        {
            return _autoresRepositorio.ObtenhaPorId(Id);
        }

        public IList<Autor> ObterTodos()
        {
            return _autoresRepositorio.ObtenhaTodos();
        }
    }
}
