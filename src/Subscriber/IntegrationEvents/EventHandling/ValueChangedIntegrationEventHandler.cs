using EventBus.EventBus.Abstractions;
using System.Threading.Tasks;
using Subscriber.IntegrationEvents.Events;

namespace Subscriber.IntegrationEvents.EventHandling
{
    public class ValueChangedIntegrationEventHandler : IIntegrationEventHandler<ValueChangedIntegrationEvent>
    {
        public Task Handle(ValueChangedIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }

    }
}
