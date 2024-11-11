using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class SuperApp
    {
        public string? GroupName { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("totalDuration")]
        public string? TotalDuration { get; set; }

        [JsonProperty("entries")]
        public Dictionary<string, HealthEntry>? Entries { get; set; }
    }

    public class HealthEntry
    {
        [JsonProperty("data")]
        public Dictionary<string, object>? Data { get; set; } 

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }
    }

    public class EndpointData
    {
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}