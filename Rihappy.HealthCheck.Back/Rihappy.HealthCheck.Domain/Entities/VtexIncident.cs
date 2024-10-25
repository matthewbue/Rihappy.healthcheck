using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class VtexIncident
    {
        public List<Incident> Incidents { get; set; }

        public class Incident
        {
            public List<Update> Updates { get; set; }
            public List<ComponentImpact> ComponentImpacts { get; set; }
            public List<StatusSummary> StatusSummaries { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Id { get; set; }
            public string StatusPageId { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public List<AffectedComponent> AffectedComponents { get; set; }
            public string Type { get; set; }
        }

        public class Update
        {
            public DateTime PublishedAt { get; set; }
            public string Id { get; set; }
            public Message Message { get; set; }
            public string MessageString { get; set; }
            public string ToStatus { get; set; }
            public List<ComponentStatus> ComponentStatuses { get; set; }
            public bool AutomatedUpdate { get; set; }
        }

        public class Message
        {
            public string Type { get; set; }
            public List<DocContent> Content { get; set; }
        }

        public class DocContent
        {
            public string Type { get; set; }
            public List<DocTextContent> Content { get; set; }
        }

        public class DocTextContent
        {
            public string Type { get; set; }
            public string Text { get; set; }
        }

        public class ComponentStatus
        {
            public string ComponentId { get; set; }
            public string Status { get; set; }
        }

        public class ComponentImpact
        {
            public DateTime StartAt { get; set; }
            public DateTime EndAt { get; set; }
            public string Id { get; set; }
            public string ComponentId { get; set; }
            public string StatusPageIncidentId { get; set; }
            public string Status { get; set; }
        }

        public class StatusSummary
        {
            public DateTime StartAt { get; set; }
            public DateTime EndAt { get; set; }
            public string WorstComponentStatus { get; set; }
        }

        public class AffectedComponent
        {
            public string ComponentId { get; set; }
            public string Status { get; set; }
        }
    }
}
