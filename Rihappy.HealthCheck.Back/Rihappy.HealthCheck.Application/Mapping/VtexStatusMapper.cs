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
                    .Where(c => new[] { "Portal/CMS", "Store Framework", "FastStore", "Sales App", "3rd Party Apps" }.Contains(c.Name))
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
                    .Where(c => new[] { "Order Placement", "Shipping Calculation", "Pricing Calculation", "Payments Gateway", "Payment Connectors" }.Contains(c.Name))
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
                    .Where(c => new[] { "Catalog Management", "Content Management", "Order Management", "Marketplace Connectors", "Admin Operations" }.Contains(c.Name))
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
                    .Where(c => new[] { "VTEX IO", "Master Data", "API Integrations" }.Contains(c.Name))
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
    if (vtexStatus.Summary.OngoingIncidents != null)
    {
        var ongoingIncident = vtexStatus.Summary.OngoingIncidents
            .FirstOrDefault(incident => incident.Updates != null && 
                incident.Updates.Any(update => update.Message != null && update.Message.Contains(componentId)));

        if (ongoingIncident != null)
        {
            return "Degraded";
        }
    }

    // Verificar se está em manutenção agendada
    if (vtexStatus.Summary.ScheduledMaintenances != null)
    {
      //  var scheduledMaintenance = vtexStatus.Summary.ScheduledMaintenances
       //     .FirstOrDefault(maintenance => maintenance.Components != null &&
       //         maintenance.Components.Any(c => c.ComponentId == componentId));

       // if (scheduledMaintenance != null)
       // {
       //     return "Under Maintenance";
      //  }
    }

    // Caso contrário, assume que está operacional
    return "Operational";
}

        public List<VtexIncindentResponseDTO> MapIncidentToDto(List<Incident> incidents)
        {
            return incidents.Select(incident => new VtexIncindentResponseDTO
            {
                //Name = incident.Updates.FirstOrDefault()?.Message ?? "No Name Available",
                Status = incident.Status,
                LastUpdate = incident.Updates.LastOrDefault()?.PublishedAt.ToString("yyyy-MM-dd HH:mm")
            }).ToList();
        }
    }

}
