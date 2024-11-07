using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.DTOs.Response
{
    public class HealthCheckSuperAppResponseDTO
    {
        public string? ServiceName { get; set; }
        public string? Status { get; set; }
        public string? Duration { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
