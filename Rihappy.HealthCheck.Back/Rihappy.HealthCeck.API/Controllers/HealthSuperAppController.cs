using Microsoft.AspNetCore.Mvc;
using Rihappy.HealthCeck.API.Controllers;
using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Application.Service;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthSuperAppController : ControllerBase
    {
        private readonly IHealthSuperAppService _healthSuperAppService;
        private readonly ILogger<HealthSuperAppController> _logger;

        public HealthSuperAppController(IHealthSuperAppService healthSuperAppService, ILogger<HealthSuperAppController> logger)
        {
            _healthSuperAppService = healthSuperAppService;
            _logger = logger;
        }

        [HttpGet("superApp")]
        public async Task<IActionResult> GetSuperApp()
        {
            try
            {
                var accounts = await _healthSuperAppService.GetHealthSuperAppAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting incidents.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}