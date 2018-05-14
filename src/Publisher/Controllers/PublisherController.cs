using EventBus.EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Publisher.IntegrationEvents.Events;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly IEventBus _eventBus;

        public PublisherController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var @event = new ValueChangedIntegrationEvent(id, id * 2, id * 3);
            _eventBus.Publish(@event);

            return ("Evento id " + id +" publicado!");
        }

    }
}
