using Microsoft.EntityFrameworkCore;
using Rihappy.HealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Data.Rest.Data
{
    public class HealthDbContext : DbContext
    {
        public HealthDbContext(DbContextOptions<HealthDbContext> options) : base(options) 
        {
        }
            public DbSet<Health> Healths { get; set; }
    }
}
