using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.DTOs.Response
{
    public class VtexStatusResponseDTO
    {
            public string PlatformStatus { get; set; }
            public List<VtexComponentResponseDTO> Components { get; set; }
            public List<VtexIncindentResponseDTO> OngoingIncidents { get; set; }
        
    }
}
