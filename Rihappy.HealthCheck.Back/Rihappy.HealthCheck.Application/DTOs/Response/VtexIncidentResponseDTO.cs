using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.DTOs.Response
{
    public class VtexIncindentResponseDTO
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? LastUpdate { get; set; }
    }
}