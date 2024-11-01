using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Domain.Entities
{
    public class HealthSuperApp
    {
        public string Status { get; set; }
        public string TotalDuration { get; set; }
        public Dictionary<string, HealthEntry> Entries { get; set; }
    }

    public class HealthEntry
    {
        public object Data { get; set; } 
        public string Duration { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
}

