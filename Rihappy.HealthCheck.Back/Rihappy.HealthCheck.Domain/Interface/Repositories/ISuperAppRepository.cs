using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Interface.Repositories
{
    public interface ISuperAppRepository
    {
        Task<SuperApp> GetSuperAppAccountAsync();

        Task<SuperApp> GetSuperAppCheckoutAsync();

        Task<SuperApp> GetSuperAppCatalogAsync();
    }
}