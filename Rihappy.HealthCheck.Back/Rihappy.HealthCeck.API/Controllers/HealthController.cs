using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rihappy.HealthCheck.Application.Interface.Service;

namespace Rihappy.HealthCeck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(IHealthService healthService, ILogger<HealthController> logger)
        {
            _healthService = healthService;
            _logger = logger;
        }

        [HttpGet("incidents")]
        public async Task<IActionResult> GetIncidents([FromQuery] DateTime startAt, [FromQuery] DateTime endAt)
        {
            try
            {
                var incidents = await _healthService.GetIncidentsAsync(startAt, endAt);
                return Ok(incidents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting incidents.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var status = await _healthService.GetStatusAsync();
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting status.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("component-impacts")]
        public async Task<IActionResult> GetComponentImpacts([FromQuery] DateTime startAt, [FromQuery] DateTime endAt)
        {
            try
            {
                var impacts = await _healthService.GetComponentImpactsAsync(startAt, endAt);
                return Ok(impacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting component impacts.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
