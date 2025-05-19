using Biblioteca._2._0.Domain.Entidades;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca._2._0.Infra.Data.Interface
{
    public interface IRepositorioGenerico<T> : IDisposable where T : EntidadeBase
    {
        IList<T> ObtenhaTodos();

        T ObtenhaPorId(int Id);

        T Cadastre(T Entidade);

        T Altere(T Entidade);

        bool Delete(int Id);

        DbSet<T> ObtenhaDbSet();
    }
}
