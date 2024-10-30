using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Data.Rest.Repositories
{
    public class HealthSuperAppRepository : IHealthSuperAppRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HealthSuperAppRepository> _logger;

        public HealthSuperAppRepository(HttpClient httpClient, ILogger<HealthSuperAppRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<HealthSuperApp> GetSuperAppAccountAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Account/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var healthCheckResponse = JsonSerializer.Deserialize<HealthSuperApp>(content);

            return healthCheckResponse;
        }

        public async Task<HealthSuperApp> GetSuperAppCheckoutAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Checkout/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var healthCheckResponse = JsonSerializer.Deserialize<HealthSuperApp>(content);

            return healthCheckResponse;
        }

        public async Task<HealthSuperApp> GetSuperAppCatalogAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Catalog/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var healthCheckResponse = JsonSerializer.Deserialize<HealthSuperApp>(content);

            return healthCheckResponse;
        }
    }
}
