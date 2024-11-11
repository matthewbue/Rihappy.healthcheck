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
    public class SuperAppService : ISuperAppService
    {
        private readonly ISuperAppRepository _healthSuperAppRepository;
        private readonly ILogger<SuperAppService> _logger;

        public SuperAppService(ISuperAppRepository healthSuperAppRepository, ILogger<SuperAppService> logger)
        {
            _healthSuperAppRepository = healthSuperAppRepository;
            _logger = logger;
        }

        public async Task<List<SuperApp>> GetHealthSuperAppAsync()
        {
            try
            {
                List<SuperApp> superAppList = new List<SuperApp>();
                var accounts = await _healthSuperAppRepository.GetSuperAppAccountAsync();
                accounts.GroupName = "Account";
                superAppList.Add(accounts);
                var checkouts = await _healthSuperAppRepository.GetSuperAppCheckoutAsync();
                checkouts.GroupName = "Checkout";
                superAppList.Add(checkouts);
                var catalogs = await _healthSuperAppRepository.GetSuperAppCatalogAsync();
                catalogs.GroupName = "Catalog";
                superAppList.Add(catalogs);
                return superAppList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching incidents.");
                throw;
            }
        }

    }   
}
