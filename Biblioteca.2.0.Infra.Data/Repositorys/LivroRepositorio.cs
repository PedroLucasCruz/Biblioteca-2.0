using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
