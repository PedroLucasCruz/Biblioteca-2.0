using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories.Base;
using Biblioteca._2._0.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Infra.Data.Repositorys.Base
{
    public class EFRepositorioGenerico<T> :  IRepositorioGenerico<T> where T : EntidadeBase
    {

        protected ApplicationDbContext _contexto;

        protected DbSet<T> _DbSet;

        public EFRepositorioGenerico(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _DbSet = _contexto.Set<T>();
        }

        public T Altere(T Entidade)
        {
            _DbSet.Update(Entidade);
            _contexto.SaveChanges();
            return Entidade;
        }

        public T Cadastre(T Entidade)
        {

            _DbSet.Add(Entidade);
            _contexto.SaveChanges();
            _contexto.Entry(Entidade).Reload(); //Atualiza a entidade no contexto para objter os valores adicionados
            return Entidade;
        }

        public bool Delete(int Id)
        {

            try
            {
                var resp = _DbSet.FirstOrDefault(x => x.Id == Id);
                _DbSet.Remove(resp);
                _contexto.SaveChanges();
                Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DbSet<T> ObtenhaDbSet()
        {
            return _DbSet;
        }

        public void Dispose()
        {

            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }

        public T ObtenhaPorId(int Id)
        {
            return _DbSet.AsNoTracking().FirstOrDefault(x => x.Id == Id);
        }

        public IList<T> ObtenhaTodos()
        {
            return _DbSet.ToList();
        }
    }
}
