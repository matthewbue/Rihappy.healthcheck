using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using static Rihappy.HealthCheck.Domain.Entities.VtexIncident;

namespace Rihappy.HealthCeck.API.Mapping
{
    public class VtexStatusMapper
    {
        public VtexStatusResponseDTO MapToDto(VtexStatus vtexStatus)
        {
            var vtexStatusDto = new VtexStatusResponseDTO
            {
                CategoryName = vtexStatus.Summary.Name,
                Components = new List<CategoryComponentsDto>
                {
                    new CategoryComponentsDto
                    {
                        GroupName = "Storefront",
                        Components = vtexStatus.Summary.Structure.Items
                            .SelectMany(item => item.Group.Components)
                            .Where(c => new[] { "Portal/CMS", "Store Framework", "FastStore", "Sales App", "3rd Party Apps" }
                            .Contains(c.Name))
                            .Select(component => new ComponentDto
                            {
                                Name = component.Name,
                                Description = component.Description,
                                Status = InferComponentStatus(vtexStatus, component.ComponentId)
                            }).ToList()
                    },
                    new CategoryComponentsDto
                    {
                        GroupName = "Checkout",
                        Components = vtexStatus.Summary.Structure.Items
                            .SelectMany(item => item.Group.Components)
                            .Where(c => new[] { "Order Placement", "Shipping Calculation", "Pricing Calculation", "Payments Gateway", "Payment Connectors" }
                            .Contains(c.Name))
                            .Select(component => new ComponentDto
                            {
                                Name = component.Name,
                                Description = component.Description,
                                Status = InferComponentStatus(vtexStatus, component.ComponentId)
                            }).ToList()
                    },
                    new CategoryComponentsDto
                    {
                        GroupName = "Admin",
                        Components = vtexStatus.Summary.Structure.Items
                            .SelectMany(item => item.Group.Components)
                            .Where(c => new[] { "Catalog Management", "Content Management", "Order Management", "Marketplace Connectors", "Admin Operations" }
                            .Contains(c.Name))
                            .Select(component => new ComponentDto
                            {
                                Name = component.Name,
                                Description = component.Description,
                                Status = InferComponentStatus(vtexStatus, component.ComponentId)
                            }).ToList()
                    },
                    new CategoryComponentsDto
                    {
                        GroupName = "Developer Tools",
                        Components = vtexStatus.Summary.Structure.Items
                            .SelectMany(item => item.Group.Components)
                            .Where(c => new[] { "VTEX IO", "Master Data", "API Integrations" }
                            .Contains(c.Name))
                            .Select(component => new ComponentDto
                            {
                                Name = component.Name,
                                Description = component.Description,
                                Status = InferComponentStatus(vtexStatus, component.ComponentId)
                            }).ToList()
                    }
                }
            };
            return vtexStatusDto;
        }

        private string InferComponentStatus(VtexStatus vtexStatus, string componentId)
        {
                // Verificar se há manutenções programadas afetando o componente
                if (vtexStatus.Summary.ScheduledMaintenances != null)
                {
                    var maintenance = vtexStatus.Summary.ScheduledMaintenances
                        .FirstOrDefault(m => m.Components != null && 
                            m.Components.Any(c => c.ComponentId == componentId));

                    if (maintenance != null)
                    {
                        return "Under Maintenance";
                    }
                }

                // Verificar se o componente está listado em "AffectedComponents"
                if (vtexStatus.Summary.AffectedComponents != null)
                {
                    var affectedComponent = vtexStatus.Summary.AffectedComponents
                        .FirstOrDefault(c => c.ComponentId == componentId);

                    if (affectedComponent != null)
                    {
                        return affectedComponent.Status == "partial_outage" ? "Degraded" : affectedComponent.Status;
                    }
                }

                // Verificar se há incidentes em andamento afetando o componente e capturar o pior status
                string worstStatus = "Operational"; // Status padrão
                if (vtexStatus.Summary.OngoingIncidents != null)
                {
                    foreach (var incident in vtexStatus.Summary.OngoingIncidents)
                    {
                        if (incident.ComponentImpacts != null)
                        {
                            foreach (var impact in incident.ComponentImpacts
                                .Where(impact => impact.ComponentId == componentId))
                            {
                                if (impact.Status == "partial_outage")
                                {
                                    worstStatus = "Degraded";
                                }
                                else if (impact.Status == "degraded" && worstStatus != "Degraded")
                                {
                                    worstStatus = "Degraded";
                                }
                            }
                        }
                    }
                }

            return worstStatus;
        }


        public List<VtexIncindentResponseDTO> MapIncidentToDto(List<Incident> incidents)
        {
            return incidents.Select(incident => new VtexIncindentResponseDTO
            {
                Status = incident.Status,
                LastUpdate = incident.Updates.LastOrDefault()?.PublishedAt.ToString("yyyy-MM-dd HH:mm")
            }).ToList();
        }
    }
}
