using System;
using GenericReachEndpoint.Models;
using GenericReachEndpoint.Security;
using Microsoft.AspNetCore.Mvc;

namespace GenericReachEndpoint.Controllers
{

    public class GroupsController : Controller
    {

        [HttpPost("api/1.0/groups/{groupId}/recipients")]
        [RequireKey]
        public async void Post(string groupId, [FromBody] AddRecipientObject message)
        {
            var entity = new LogMessage()
            {
                Id = Guid.NewGuid(),
                ApiKey = HttpContext.Request.Headers["apikey"],
                Timestamp = DateTime.Now,
                Action = "Add to group",
                GroupId = groupId,
                Body = message
            };

            await DocumentDbRepository<LogMessage>.CreateItemAsync(entity);
        }

        public class AddRecipientObject
        {
            public string Recipient { get; set; }
            public long TriggerFireId { get; set; }
        }

        
        [HttpDelete("api/1.0/groups/{groupId}/recipients")]
        [RequireKey]
        public async void Delete(string groupId, [FromBody] DeleteRecipientObject message)
        {
            var entity = new LogMessage()
            {
                Id = Guid.NewGuid(),
                ApiKey = HttpContext.Request.Headers["apikey"],
                Timestamp = DateTime.Now,
                Action = "Remove from group",
                GroupId = groupId,
                Body = message
            };

            await DocumentDbRepository<LogMessage>.CreateItemAsync(entity);
        }

        public class DeleteRecipientObject
        {
            public string Recipient { get; set; }
        }
    }
}
