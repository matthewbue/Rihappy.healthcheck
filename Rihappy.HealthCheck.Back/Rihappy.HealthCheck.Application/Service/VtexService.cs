using Microsoft.Extensions.Logging;
using Rihappy.HealthCeck.API.Mapping;
using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Domain.Entities;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rihappy.HealthCheck.Domain.Entities.VtexIncident;

namespace Rihappy.HealthCheck.Application.Service
{
    public class VtexService : IVtexService
    {
        private readonly IVtexRepository _healthRepository;
        private readonly ILogger<VtexService> _logger;

        public VtexService(IVtexRepository healthRepository, ILogger<VtexService> logger)
        {
            _healthRepository = healthRepository;
            _logger = logger;
        }

        public async Task <List<VtexIncindentResponseDTO>> GetIncidentsAsync(DateTime startAt, DateTime endAt)
        {
            try
            {
                _logger.LogInformation("Fetching incidents from {StartAt} to {EndAt}", startAt, endAt);
                var incidents = await _healthRepository.GetIncidentsAsync(startAt, endAt);
                VtexStatusMapper _mapper = new VtexStatusMapper();
                var resultDto = _mapper.MapIncidentToDto(incidents.Incidents);
                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching incidents.");
                throw;
            }
        }

        public async Task<VtexStatusResponseDTO> GetStatusAsync()
        {
            try
            {
               
                var status = await _healthRepository.GetStatusAsync();
               
                VtexStatusMapper _mapper = new VtexStatusMapper();
                var resultDto = _mapper.MapToDto(status);
                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching status.");
                throw;
            }
        }

        
    }
}
