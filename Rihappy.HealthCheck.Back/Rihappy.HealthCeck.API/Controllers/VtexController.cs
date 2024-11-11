using Microsoft.AspNetCore.Mvc;
using Rihappy.HealthCheck.Application.Interface.Service;
using System.Net.Mime;
using System.Net;
using Rihappy.HealthCheck.Application.DTOs.Response;

namespace Rihappy.HealthCeck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VtexController : ControllerBase
    {
        private readonly IVtexService _healthService;
        private readonly ILogger<VtexController> _logger;

        public VtexController(IVtexService healthService, ILogger<VtexController> logger)
        {
            _healthService = healthService;
            _logger = logger;
        }
        /// <summary>
        /// Obtem o historico de incidentes dos servicos Vtex.
        /// </summary>
        /// <returns>Retorna o historico de incidente dos servicos Vtex.</returns>
        /// <response code="200">Objeto contendo informações da simulação de carrinho.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(VtexIncindentResponseDTO), (int)HttpStatusCode.OK)]
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

        /// <summary>
        /// Obtem status dos servicos Vtex.
        /// </summary>
        /// <returns>Retorna as informacoes do estado atual dos servicos Vtex.</returns>
        /// <response code="200">Objeto contendo informações das Apis.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(VtexStatusResponseDTO), (int)HttpStatusCode.OK)]
        [HttpGet("status")]
        public async Task<IActionResult> GetStatusVtex()
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
    }
}