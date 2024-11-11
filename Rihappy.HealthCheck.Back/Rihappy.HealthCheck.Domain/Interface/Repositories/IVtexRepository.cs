using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Interface.Repositories
{
    public interface IVtexRepository
    {
        Task<VtexIncident> GetIncidentsAsync(DateTime startAt, DateTime endAt);

        Task<Vtex> GetStatusAsync();

    }
}