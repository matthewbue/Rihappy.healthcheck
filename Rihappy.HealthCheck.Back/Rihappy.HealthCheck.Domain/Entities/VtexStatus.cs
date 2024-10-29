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

    // Classe para representar os componentes afetados por incidentes
    public class AffectedComponent
    {
        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } // Exemplo: partial_outage, major_outage, etc.
    }

    // Classe para impactos de componentes, se necessário
    public class ComponentImpact
    {
        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } // Exemplo: partial_outage, degraded, etc.

        [JsonProperty("start_at")]
        public DateTime StartAt { get; set; } // Quando começou o impacto
    }

    public class OngoingIncident
    {
        public string Name { get; set; }
        public string Status { get; set; }

        [JsonProperty("affected_components")]
        public List<AffectedComponent> AffectedComponents { get; set; } // Lista de componentes afetados

        [JsonProperty("component_impacts")]
        public List<ComponentImpact> ComponentImpacts { get; set; } // Lista de impactos nos componentes

        public List<IncidentUpdate> Updates { get; set; }
    }

    public class IncidentUpdate
    {
        public string Message { get; set; } 
        public DateTime PublishedAt { get; set; } 
    }

public class ScheduledMaintenance
{
    [JsonProperty("id")]
    public string Id { get; set; } // Identificador único da manutenção

    [JsonProperty("name")]
    public string Name { get; set; } // Nome ou descrição curta da manutenção

    [JsonProperty("status")]
    public string Status { get; set; } // Status da manutenção (ex: scheduled, in_progress, completed)

    [JsonProperty("scheduled_start_at")]
    public DateTime ScheduledStartAt { get; set; } // Data e hora de início da manutenção

    [JsonProperty("scheduled_end_at")]
    public DateTime ScheduledEndAt { get; set; } // Data e hora do fim previsto da manutenção

    [JsonProperty("components")]
    public List<AffectedComponent> Components { get; set; } // Lista de componentes afetados pela manutenção

    [JsonProperty("description")]
    public string Description { get; set; } // Descrição mais detalhada do que será realizado na manutenção

    [JsonProperty("impact")]
    public string Impact { get; set; } // Impacto previsto durante a manutenção (ex: serviço interrompido, desempenho reduzido)

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } // Data e hora de quando a manutenção foi criada ou agendada

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; } // Última data e hora em que as informações de manutenção foram atualizadas
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
