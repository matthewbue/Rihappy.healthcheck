using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class VtexComponent
    {
        public List<IncidentLink> IncidentLinks { get; set; }
        public List<ComponentImpact> ComponentImpacts { get; set; }
        public List<ComponentUptime> ComponentUptimes { get; set; }

        public class IncidentLink
        {
            public DateTime PublishedAt { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Permalink { get; set; }
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

        public class ComponentUptime
        {
            // Espaço reservado para detalhes de tempo de atividade do componente, se necessário no futuro
        }
    }
}
