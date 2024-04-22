using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca._2._0.Domain.Core.Messages
{
    public abstract class Command : Message, IRequest<Guid>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
