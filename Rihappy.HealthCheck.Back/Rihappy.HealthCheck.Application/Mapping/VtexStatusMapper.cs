using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using static Rihappy.HealthCheck.Domain.Entities.VtexIncident;

namespace Rihappy.HealthCeck.API.Mapping
{
    public class VtexStatusMapper
    {
        public VtexStatusResponseDTO MapToDto(VtexStatus status)
        {
            var dto = new VtexStatusResponseDTO
            {
                PlatformStatus = status.Summary.OngoingIncidents.Count == 0 ? "Fully Operational" : "Issues Reported",
                Components = status.Summary.Components.Select(c => new VtexComponentResponseDTO
                {
                    Name = c.Name,
                    Status = c.Description 
                }).ToList(),
                //OngoingIncidents = status.Summary.OngoingIncidents.Select(incident => new VtexIncindentResponseDTO
                //{
                //    Name = incident.Name,
                //    Status = incident.Status,
                //    LastUpdate = incident.Updates.LastOrDefault()?.PublishedAt.ToString("yyyy-MM-dd HH:mm")
                //}).ToList(),
            };

            return dto;
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
