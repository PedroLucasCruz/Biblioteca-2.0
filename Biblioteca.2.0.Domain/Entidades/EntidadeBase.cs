using Biblioteca._2._0.Domain.Core.DomainObjects;


namespace Biblioteca._2._0.Domain.Entidades
{
    /// <summary>
    /// Objeto comum a todas as outras entidades
    /// </summary>
    public class EntidadeBase : Entity
    {
        public virtual bool EhValido<T>(T valor) where T : class
        {
            return valor != null;
        }
    }
}
