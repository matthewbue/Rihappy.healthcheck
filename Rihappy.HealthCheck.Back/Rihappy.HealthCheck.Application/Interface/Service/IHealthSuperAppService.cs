﻿using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.Interface.Service
{
    public interface IHealthSuperAppService
    {
        Task<HealthSuperApp> GetAccountAsync();
        Task<HealthSuperApp> GetCheckoutAsync();
        Task<HealthSuperApp> GetCatalogAsync();
    }
}
