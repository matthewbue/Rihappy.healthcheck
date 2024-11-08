﻿using Microsoft.Extensions.Logging;
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

        public async Task<List<HealthSuperApp>> GetHealthSuperAppAsync()
        {
            try
            {
                List<HealthSuperApp> superAppList = new List<HealthSuperApp>();
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
