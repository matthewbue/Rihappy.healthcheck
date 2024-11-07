using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Interface.Repositories
{
    public interface IHealthRepository
    {
        Task<VtexIncident> GetIncidentsAsync(DateTime startAt, DateTime endAt);

        Task<VtexStatus> GetStatusAsync();

        Task<VtexComponent> GetComponentImpactsAsync(DateTime startAt, DateTime endAt);
    }
}