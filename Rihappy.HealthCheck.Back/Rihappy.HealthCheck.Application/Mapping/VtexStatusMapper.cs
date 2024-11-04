using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using static Rihappy.HealthCheck.Application.DTOs.Response.StatusResponseDTO;
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


        public StatusResponseDTO MapSuperAppToDto(HealthSuperApp healthSuperApp)
        {
            var statusResponseDto = new StatusResponseDTO
            {
                Status = healthSuperApp.Status,
                TotalDuration = healthSuperApp.TotalDuration,
                Entries = healthSuperApp.Entries.ToDictionary(
                    entry => entry.Key,
                    entry => new EntryDto
                    {
                        //Data = entry.Value.Data as Dictionary<string, List<List<object>>>,
                        Duration = entry.Value.Duration,
                        Status = entry.Value.Status,
                        Description = entry.Value.Description,
                        Tags = entry.Value.Tags
                    })
            };

            return statusResponseDto;
        }

        
     private string InferComponentStatus(VtexStatus vtexStatus, string componentId)
{
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

    if (vtexStatus.Summary.AffectedComponents != null)
    {
        var affectedComponent = vtexStatus.Summary.AffectedComponents
            .FirstOrDefault(c => c.ComponentId == componentId);

        if (affectedComponent != null)
        {
            return affectedComponent.Status == "partial_outage" ? "Degraded" : affectedComponent.Status;
        }
    }

    string worstStatus = "Operational"; 
    if (vtexStatus.Summary.OngoingIncidents != null)
    {
        foreach (var incident in vtexStatus.Summary.OngoingIncidents)
        {
            if (incident.ComponentImpacts != null)
            {
                foreach (var impact in incident.ComponentImpacts.Where(impact => impact.ComponentId == componentId))
                {
                    if (impact.Status == "full_outage") return "Full Outage";
                    if (impact.Status == "partial_outage" || impact.Status == "degraded") worstStatus = "Degraded";
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
