using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GenericReachEndpoint.Models;
using GenericReachEndpoint.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GenericReachEndpoint.Controllers
{
    [Route("api/1.0/[controller]")]
    public class LogController : Controller
    {

        [HttpGet]
        [RequireKey]
        public async Task<IEnumerable<LogMessage>> Get()
        {
            var apiKey = HttpContext.Request.Headers["apikey"].ToString();

            var log = await DocumentDbRepository<LogMessage>.GetItemsAsync(x => x.ApiKey == apiKey, y => y.Timestamp);

            return log.Select(x => new LogMessage
            {
                Id = x.Id,
                Timestamp = x.Timestamp,
                Action = x.Action,
                GroupId = x.GroupId,
                Body = x.Body
            });
        }
    }
}