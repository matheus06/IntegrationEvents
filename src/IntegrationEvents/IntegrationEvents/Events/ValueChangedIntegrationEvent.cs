using EventBus.EventBus.Events;

namespace Publisher.IntegrationEvents.Events
{
    public class ValueChangedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; private set; }
        public decimal NewPrice { get; private set; }
        public decimal OldPrice { get; private set; }

        public ValueChangedIntegrationEvent(int productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
