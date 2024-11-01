using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.DTOs.Response
{
    public class StatusResponseDTO
    {
        public string Status { get; set; }
        public string TotalDuration { get; set; }
        public Dictionary<string, EntryDto> Entries { get; set; }
    }

        public class EntryDto
        {
            public Dictionary<string, List<List<object>>> Data { get; set; } 
            public string Duration { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public List<string> Tags { get; set; }
        }

    
}
