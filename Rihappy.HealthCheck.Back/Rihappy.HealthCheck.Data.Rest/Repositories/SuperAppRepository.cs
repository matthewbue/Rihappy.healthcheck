using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rihappy.HealthCheck.Application.DTOs.Response;
using Rihappy.HealthCheck.Domain.Entities;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Data.Rest.Repositories
{
    public class SuperAppRepository : ISuperAppRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SuperAppRepository> _logger;

        public SuperAppRepository(HttpClient httpClient, ILogger<SuperAppRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<SuperApp> GetSuperAppAccountAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Account/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var healthCheckResponse = JsonConvert.DeserializeObject<SuperApp>(content); 

            if(healthCheckResponse is null)
            {
                return new SuperApp();
            }

            return healthCheckResponse;
        }

        public async Task<SuperApp> GetSuperAppCheckoutAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Checkout/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var healthCheckResponse = JsonConvert.DeserializeObject<SuperApp>(content);

            if (healthCheckResponse is null)
            {
                return new SuperApp();
            }

            return healthCheckResponse;
        }

        public async Task<SuperApp> GetSuperAppCatalogAsync()
        {
            var response = await _httpClient.GetAsync("https://api-superapp.gruporihappy.com.br/api/Catalog/hc");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve health check status. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var healthCheckResponse = JsonConvert.DeserializeObject<SuperApp>(content);
            if (healthCheckResponse is null)
            {
                return new SuperApp();
            }

            return healthCheckResponse;
        }
    }
}