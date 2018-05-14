using EventBus.EventBus.Abstractions;
using Subscriber.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace ConsoleSubscriber.IntegrationEvents.EventHandling
{
    public class ValueChangedIntegrationEventHandler : IIntegrationEventHandler<ValueChangedIntegrationEvent>
    {
        public Task Handle(ValueChangedIntegrationEvent @event)
        {
            Console.Write(@event.ProductId + " " + @event.NewPrice + " " +   @event.OldPrice + " | ");
            return Task.CompletedTask;
        }

    }
}
