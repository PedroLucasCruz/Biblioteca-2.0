using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca._2._0.Domain.Entidades;

namespace Biblioteca._2._0.Domain.Interfaces.Repositories.Base
{
    public interface IRepositorioGenerico<T> : IDisposable where T : EntidadeBase
    {
        IList<T> ObtenhaTodos();

        T ObtenhaPorId(int Id);

        T Cadastre(T Entidade);

        T Altere(T Entidade);

        bool Delete(int Id);

       // DbSet<T> ObtenhaDbSet();
    }
}
