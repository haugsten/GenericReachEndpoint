using System;
using GenericReachEndpoint.Models;
using GenericReachEndpoint.Security;
using Microsoft.AspNetCore.Mvc;

namespace GenericReachEndpoint.Controllers
{

    [Route("api/1.0/[controller]")]
    public class MessagesController : Controller
    {
        
        [HttpPost]
        [RequireKey]
        public async void Post([FromBody] MessageObject message)
        {

            var entity = new LogMessage()
            {
                Id = Guid.NewGuid(),
                ApiKey = HttpContext.Request.Headers["apikey"],
                Timestamp = DateTime.Now,
                Action = "Send message",
                Body = message
            };

            await DocumentDbRepository<LogMessage>.CreateItemAsync(entity);
        }

        public class MessageObject
        {
            public string Recipient { get; set; }
            public long TriggerFireId { get; set; }
            public string MessageId { get; set; }
        }
    }
}