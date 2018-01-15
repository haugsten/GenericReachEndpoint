using System;
using GenericReachEndpoint.Models;
using GenericReachEndpoint.Security;
using Microsoft.AspNetCore.Mvc;

namespace GenericReachEndpoint.Controllers
{
    [Route("api/1.0/[controller]")]
    public class TestController : Controller
    {

        [HttpPost]
        [RequireKey]
        public async void Post([FromBody] TestObject message)
        {
            var entity = new LogMessage()
            {
                Id = Guid.NewGuid(),
                ApiKey = HttpContext.Request.Headers["apikey"],
                Timestamp = DateTime.Now,
                Action = "Connection test",
                Body = message
            };

            await DocumentDbRepository<LogMessage>.CreateItemAsync(entity);
        }

        public class TestObject
        {
            public string Recipient { get; set; }
        }
    }
}