using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Subscriber.Controllers
{
    [Route("api/[controller]")]
    public class SubscriberController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "SubscriberController OK", "SubscriberController Online" };
        }

    }
}
