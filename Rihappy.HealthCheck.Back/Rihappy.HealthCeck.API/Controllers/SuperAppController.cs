using Microsoft.AspNetCore.Mvc;
using Rihappy.HealthCeck.API.Controllers;
using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Application.Service;
using System.Net.Mime;
using System.Net;
using System.Threading.Tasks;
using Rihappy.HealthCheck.Domain.Entities;

namespace Rihappy.HealthCheck.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperAppController : ControllerBase
    {
        private readonly ISuperAppService _healthSuperAppService;
        private readonly ILogger<SuperAppController> _logger;

        public SuperAppController(ISuperAppService healthSuperAppService, ILogger<SuperAppController> logger)
        {
            _healthSuperAppService = healthSuperAppService;
            _logger = logger;
        }

        /// <summary>
        /// Obtem status das API`S do super App.
        /// </summary>
        /// <returns>Retorna as informacoes do estado atual das apis que compoem o super App.</returns>
        /// <response code="200">Objeto contendo informações das Apis.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SuperApp), (int)HttpStatusCode.OK)]
        [HttpGet("status")]
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