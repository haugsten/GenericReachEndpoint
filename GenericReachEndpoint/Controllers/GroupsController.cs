using System;
using GenericReachEndpoint.Models;
using GenericReachEndpoint.Security;
using Microsoft.AspNetCore.Mvc;

namespace GenericReachEndpoint.Controllers
{

    public class GroupsController : Controller
    {

        private readonly IRepository _repository;

        public GroupsController(IRepository repository)
        {
            _repository = repository;
        }

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

            await _repository.CreateItemAsync(entity);
        }

        public class AddRecipientObject
        {
            public string Recipient { get; set; }
            public long TriggerFireId { get; set; }
        }

        
        [HttpDelete("api/1.0/groups/{groupId}/recipients/{recipient}")]
        [RequireKey]
        public async void Delete(string groupId, string recipient)
        {
            var entity = new LogMessage()
            {
                Id = Guid.NewGuid(),
                ApiKey = HttpContext.Request.Headers["apikey"],
                Timestamp = DateTime.Now,
                Action = "Remove from group",
                GroupId = groupId,
                Body = recipient
            };

            await _repository.CreateItemAsync(entity);
        }       
    }
}
