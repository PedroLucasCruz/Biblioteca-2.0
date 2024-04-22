using Biblioteca._2._0.Application.Dtos.Livros;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.AppService
{
    public class LivroAppService : ILivroAppService
    {
        private readonly IServicoLivro _servicoLivro;

        public LivroAppService(IServicoLivro servicoLivro)
        {
            _servicoLivro = servicoLivro;
        }

        public InconsistenciaDeValidacaoTipado<Livro> Atualizar(LivroDto livro)
        {
            if (!livro.IsValid()) return livro.RetornarInconsistencia();
             
            return _servicoLivro.Atualizar(livro.ObterEntidade());
        }

        public InconsistenciaDeValidacaoTipado<Livro> Cadastrar(LivroDto livro)
        {
            if (!livro.IsValid()) return livro.RetornarInconsistencia();

            return _servicoLivro.Cadastrar(livro.ObterEntidade());
        }

        public InconsistenciaDeValidacaoTipado<Livro> Deletar(int Id)
        {
            return _servicoLivro.Deletar(Id);
        }

        public Livro ObtenhaLivro(int Id)
        {
            return _servicoLivro.ObtenhaLivro(Id);
        }

        public IList<Livro> ObtenhaTodosLivros()
        {
            return   _servicoLivro.ObtenhaTodosLivros();
        }
    }
}
