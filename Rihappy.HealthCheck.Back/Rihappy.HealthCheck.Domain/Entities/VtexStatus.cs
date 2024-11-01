using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class VtexStatus
    {
        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    public class Summary
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subpath")]
        public string Subpath { get; set; }

        [JsonProperty("support_url")]
        public string SupportUrl { get; set; }

        [JsonProperty("support_label")]
        public string SupportLabel { get; set; }

        [JsonProperty("public_url")]
        public string PublicUrl { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("favicon_url")]
        public string FaviconUrl { get; set; }

        [JsonProperty("components")]
        public List<Component> Components { get; set; }

        [JsonProperty("subscriptions_disabled")]
        public bool SubscriptionsDisabled { get; set; }

        [JsonProperty("display_uptime_mode")]
        public string DisplayUptimeMode { get; set; }

        [JsonProperty("allow_search_engine_indexing")]
        public bool AllowSearchEngineIndexing { get; set; }

        [JsonProperty("affected_components")]
        public List<AffectedComponent> AffectedComponents { get; set; }

        [JsonProperty("ongoing_incidents")]
        public List<OngoingIncident> OngoingIncidents { get; set; }

        [JsonProperty("scheduled_maintenances")]
        public List<ScheduledMaintenance> ScheduledMaintenances { get; set; }

        [JsonProperty("structure")]
        public Structure Structure { get; set; }

        [JsonProperty("google_analytics_tag")]
        public string GoogleAnalyticsTag { get; set; }

        [JsonProperty("terms_of_service_url")]
        public string TermsOfServiceUrl { get; set; }

        [JsonProperty("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }

        [JsonProperty("expose_status_summary_api")]
        public bool ExposeStatusSummaryApi { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("page_type")]
        public string PageType { get; set; }

        [JsonProperty("data_available_since")]
        public DateTime DataAvailableSince { get; set; }
    }

    public class Component
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status_page_id")]
        public string StatusPageId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class AffectedComponent
    {
        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ComponentImpact
    {
        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_at")]
        public DateTime StartAt { get; set; }
    }

    public class OngoingIncident
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("affected_components")]
        public List<AffectedComponent> AffectedComponents { get; set; }

        [JsonProperty("component_impacts")]
        public List<ComponentImpact> ComponentImpacts { get; set; }

        [JsonProperty("updates")]
        public List<IncidentUpdate> Updates { get; set; }
    }

    public class IncidentUpdate
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
    }

    public class ScheduledMaintenance
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("scheduled_start_at")]
        public DateTime ScheduledStartAt { get; set; }

        [JsonProperty("scheduled_end_at")]
        public DateTime ScheduledEndAt { get; set; }

        [JsonProperty("components")]
        public List<AffectedComponent> Components { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("impact")]
        public string Impact { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class Structure
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status_page_id")]
        public string StatusPageId { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }

    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("display_aggregated_uptime")]
        public bool DisplayAggregatedUptime { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("components")]
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
