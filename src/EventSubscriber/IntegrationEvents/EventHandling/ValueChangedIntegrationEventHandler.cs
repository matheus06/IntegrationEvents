using EventBus.EventBus.Abstractions;
using EventSubscriber.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace EventSubscriber.IntegrationEvents.EventHandling
{
    class ValueChangedIntegrationEventHandler : IIntegrationEventHandler<ValueChangedIntegrationEvent>
    {
        public async Task Handle(ValueChangedIntegrationEvent @event)
        {
            await Task.Delay(10);
            Console.WriteLine(@event.ProductId);
            Console.WriteLine(@event.ProductId);
            Console.WriteLine(@event.ProductId);
        }
    }
}
