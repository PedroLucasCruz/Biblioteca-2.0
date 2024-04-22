using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoEditora : IServicoEditora
    {
        private readonly IEditoraRepositorio _editoraRepositorio;
        private readonly ILivroRepositorio _livroRepositorio;

        public ServicoEditora(IEditoraRepositorio editoraRepositorio, ILivroRepositorio livroRepositorio)
        {
            _editoraRepositorio = editoraRepositorio;
            _livroRepositorio = livroRepositorio;
        }

        public InconsistenciaDeValidacao Atualizar(Editora dto)
        {
           

            var editora = ObterPorId(dto.Id);

            if (editora.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = "Não registrado" };

            editora.DataAtualizacao = DateTime.Now;

            _editoraRepositorio.Altere(editora);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }

        public InconsistenciaDeValidacao Cadastrar(Editora dto)
        {
            //dto.Codigo = Guid.NewGuid();
            dto.DataCriacao = DateTime.Now;
            dto.DataAtualizacao = DateTime.Now;

            _editoraRepositorio.Cadastre(dto);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }

        public InconsistenciaDeValidacao Deletar(int id)
        {
            var editora = ObterPorId(id);

            if (!editora.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = "Não registrado" };

            var livro = _livroRepositorio.ConsultarLivroPorIdEditar(id);

            if (livro.PossuiValor()) return new InconsistenciaDeValidacao { Mensagem = $"O livro: {livro.Titulo} está relacionado a está editora, ela não pode ser deletada" };

            _editoraRepositorio.Delete(editora.Id);

            return new InconsistenciaDeValidacao { Mensagem = "Sucesso" };
        }



        public Editora ObterPorId(int Id)
        {
            return _editoraRepositorio.ObtenhaPorId(Id);
        }

        public IList<Editora> ObterTodos()
        {
            return _editoraRepositorio.ObtenhaTodos();
        }
    }
}
