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

        [HttpGet("account")]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                var accounts = await _healthSuperAppService.GetAccountAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting incidents.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> GetCheckout()
        {
            try
            {
                var checkouts = await _healthSuperAppService.GetCheckoutAsync();
                return Ok(checkouts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting status.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("catalog")]
        public async Task<IActionResult> GetCatalog()
        {
            try
            {
                var catalogs = await _healthSuperAppService.GetCatalogAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting component impacts.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
