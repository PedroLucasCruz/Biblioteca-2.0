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
    internal class LivroAutoresRepositorio : EFRepositorioGenerico<LivroAutores>, ILivroAutoresRepositorio
    {
        public LivroAutoresRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }

        public IList<LivroAutores> ConsultarLivroAutoresPorIdAutor(int id)
        {
            return _DbSet.Where(x => x.AutorId == id).ToList();
        }
    }
}
