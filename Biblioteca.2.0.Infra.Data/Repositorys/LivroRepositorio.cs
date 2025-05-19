using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca._2._0.Infra.Data.Repositorys
{
    internal class LivroRepositorio : EFRepositorioGenerico<Livro>, ILivroRepositorio
    {
        public LivroRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }

        public void CadastrarLivros(IList<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!_DbSet.Where(x => x.Codigo == livro.Codigo).Any())
                {
                    _DbSet.Add(livro);
                }
            }
            _contexto.SaveChanges();
        }

        public Livro ConsultarLivroPorIdEditar(int idEditora)
        {
            return _DbSet.Where(x => x.EditoraId == idEditora).FirstOrDefault();
        }

        public List<Livro> RetornarLivrosComAutores()
        {
          return ObtenhaDbSet().Include(x => x.Editora).Include(a => a.Autores).ToList();
        }
    }
}
