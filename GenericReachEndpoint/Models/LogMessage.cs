using System;
using Newtonsoft.Json;

namespace GenericReachEndpoint.Models
{
    public class LogMessage
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "body")]
        public object Body { get; set; }

        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "groupid")]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "apikey")]
        public string ApiKey { get; set; }
    }
}