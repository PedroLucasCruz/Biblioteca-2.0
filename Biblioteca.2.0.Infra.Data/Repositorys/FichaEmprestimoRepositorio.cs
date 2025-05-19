using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Infra.Data.Repositorys
{
    public class FichaEmprestimoRepositorio : EFRepositorioGenerico<FichaEmprestimoAluno>, IServicoUsuarioImplRepositorio
    {
        public FichaEmprestimoRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }

        public bool LivroEmUso(int Id)
        {
          return  _DbSet.AsNoTracking().Any(x => x.FichaEmprestimoItens.Any(x => x.Livro.Id == Id));        
        }

    }
}
