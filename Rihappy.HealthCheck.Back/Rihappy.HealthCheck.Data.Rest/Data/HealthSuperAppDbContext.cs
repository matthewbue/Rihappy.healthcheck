using Microsoft.EntityFrameworkCore;
using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Data.Rest.Data
{
    public class HealthSuperAppDbContext : DbContext
    {
        public HealthSuperAppDbContext(DbContextOptions<HealthSuperAppDbContext> options) : base(options) 
        { 
        }
        public DbSet<HealthSuperApp> healthSuperApps { get; set; }
    }
}
