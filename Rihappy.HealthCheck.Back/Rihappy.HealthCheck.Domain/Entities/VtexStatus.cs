using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class VtexStatus
    {
        public Summary Summary { get; set; }
    }

    public class Summary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subpath { get; set; }
        public string SupportUrl { get; set; }
        public string SupportLabel { get; set; }
        public string PublicUrl { get; set; }
        public string LogoUrl { get; set; }
        public string FaviconUrl { get; set; }
        public List<Component> Components { get; set; }
        public bool SubscriptionsDisabled { get; set; }
        public string DisplayUptimeMode { get; set; }
        public bool AllowSearchEngineIndexing { get; set; }
        public List<AffectedComponent> AffectedComponents { get; set; }

        [JsonPropertyName("Ongoing_incidents")]
        public List<OngoingIncident> OngoingIncidents { get; set; }
        public List<ScheduledMaintenance> ScheduledMaintenances { get; set; }
        public Structure Structure { get; set; }
        public string GoogleAnalyticsTag { get; set; }
        public string TermsOfServiceUrl { get; set; }
        public string PrivacyPolicyUrl { get; set; }
        public bool ExposeStatusSummaryApi { get; set; }
        public string Theme { get; set; }
        public string Locale { get; set; }
        public string PageType { get; set; }
        public DateTime DataAvailableSince { get; set; }
    }

    public class Component
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StatusPageId { get; set; }
        public string Description { get; set; }
    }

    public class AffectedComponent
    {
        // Propriedades para componentes afetados, se necessário
    }

    public class OngoingIncident
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public List<IncidentUpdate> Updates { get; set; }
    }

    public class IncidentUpdate
    {
        public string Message { get; set; } 
        public DateTime PublishedAt { get; set; } 
    }

    public class ScheduledMaintenance
    {
        // Propriedades para manutenção programada, se necessário
    }

    public class Structure
    {
        public string Id { get; set; }
        public string StatusPageId { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public Group Group { get; set; }
    }

    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool DisplayAggregatedUptime { get; set; }
        public bool Hidden { get; set; }
        public List<ComponentGroup> Components { get; set; }
    }

    public class ComponentGroup
    {
        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("display_uptime")]
        public bool DisplayUptime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("data_available_since")]
        public DateTime DataAvailableSince { get; set; }
    }

}
