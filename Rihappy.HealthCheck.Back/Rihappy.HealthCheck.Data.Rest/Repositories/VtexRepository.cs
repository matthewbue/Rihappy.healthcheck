using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rihappy.HealthCheck.Data.Rest.Settings;
using Rihappy.HealthCheck.Domain.Entities;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Data.Rest.Repositories
{
    public class VtexRepository : IVtexRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VtexRepository> _logger;
        private readonly VtexUrls _urls;

        public VtexRepository(HttpClient httpClient, ILogger<VtexRepository> logger, IOptions<HealthCheckSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = settings.Value.Vtex;

            _httpClient.BaseAddress = new Uri(_urls.BaseUrl);
        }

        public async Task<VtexIncident> GetIncidentsAsync(DateTime startAt, DateTime endAt)
        {
            var startAtFormatted = startAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            var endAtFormatted = endAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            var requestUri = $"{_urls.IncidentsUrl}?start_at={Uri.EscapeDataString(startAtFormatted)}&end_at={Uri.EscapeDataString(endAtFormatted)}";

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to retrieve incidents with status code: {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Failed to retrieve incidents. Status code: {response.StatusCode}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VtexIncident>(jsonString);

            return result ?? new VtexIncident();
        }

        public async Task<Vtex> GetStatusAsync()
        {
            var requestUri = "";  

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to retrieve VTEX status with status code: {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Failed to retrieve VTEX status. Status code: {response.StatusCode}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Vtex>(jsonString);

            return result ?? new Vtex();
        }
    }
}
