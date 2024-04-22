using Biblioteca._2._0.Domain.Core.Messages.Common.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Core.Messages.Common.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> ObterNotificacoes()
        {
            return _notifications;
        }

        public virtual bool TemNotificacao()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
