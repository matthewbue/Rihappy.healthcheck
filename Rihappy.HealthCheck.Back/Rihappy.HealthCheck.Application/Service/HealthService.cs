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
    public class HealthService : IHealthService
    {
        private readonly IHealthRepository _healthRepository;
        private readonly ILogger<HealthService> _logger;

        public HealthService(IHealthRepository healthRepository, ILogger<HealthService> logger)
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

        public async Task<VtexComponent> GetComponentImpactsAsync(DateTime startAt, DateTime endAt)
        {
            try
            {
                _logger.LogInformation("Fetching component impacts from {StartAt} to {EndAt}", startAt, endAt);
                var componentImpacts = await _healthRepository.GetComponentImpactsAsync(startAt, endAt);
                return componentImpacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching component impacts.");
                throw;
            }


        }
    }
}
