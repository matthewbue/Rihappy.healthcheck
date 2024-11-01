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

namespace Rihappy.HealthCheck.Application.Service
{
    public class HealthSuperAppService : IHealthSuperAppService
    {
        private readonly IHealthSuperAppRepository _healthSuperAppRepository;
        private readonly ILogger<HealthSuperAppService> _logger;

        public HealthSuperAppService(IHealthSuperAppRepository healthSuperAppRepository, ILogger<HealthSuperAppService> logger)
        {
            _healthSuperAppRepository = healthSuperAppRepository;
            _logger = logger;
        }

        public async Task<HealthSuperApp> GetAccountAsync()
        {
            try
            {
                _logger.LogInformation("Fetching incidents from {StartAt} to {EndAt}");
                var accounts = await _healthSuperAppRepository.GetSuperAppAccountAsync();
                VtexStatusMapper _mapper = new VtexStatusMapper();
                var resultDto = _mapper.MapSuperAppToDto(accounts);
                return accounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching incidents.");
                throw;
            }
        }

        public async Task<HealthSuperApp> GetCheckoutAsync()
        {
            try
            {

                var checkouts = await _healthSuperAppRepository.GetSuperAppCheckoutAsync();

                VtexStatusMapper _mapper = new VtexStatusMapper();
                var resultDto = _mapper.MapSuperAppToDto(checkouts);
                return checkouts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching status.");
                throw;
            }
        }

        public async Task<HealthSuperApp> GetCatalogAsync()
        {
            try
            {
                _logger.LogInformation("Fetching component impacts from {StartAt} to {EndAt}");
                var catalogs = await _healthSuperAppRepository.GetSuperAppCatalogAsync();
                return catalogs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching component impacts.");
                throw;
            }


        }
    }
}
