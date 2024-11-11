using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rihappy.HealthCheck.Domain.Entities;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System.Net.Http.Headers;


namespace Rihappy.HealthCheck.Data.Rest.Repositories
{
    public class VtexRepository : IVtexRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VtexRepository> _logger;

        public VtexRepository(HttpClient httpClient, ILogger<VtexRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _httpClient.BaseAddress = new Uri("https://status.vtex.com/proxy/status.vtex.com/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _httpClient.DefaultRequestHeaders.Add("accept-language", "pt-PT,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            _httpClient.DefaultRequestHeaders.Add("cookie", "_ga=GA1.1.84154493.1727990566; _ga_M539S2TCMZ=GS1.1.1729520405.9.1.1729520537.0.0.0");
            _httpClient.DefaultRequestHeaders.Add("priority", "u=1, i");
            _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
            _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
        }

        public async Task<VtexIncident> GetIncidentsAsync(DateTime startAt, DateTime endAt)
        {
            var startAtFormatted = startAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            var endAtFormatted = endAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            var requestUri = $"incidents?start_at={Uri.EscapeDataString(startAtFormatted)}&end_at={Uri.EscapeDataString(endAtFormatted)}";

            var response = await _httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<VtexIncident>(jsonString);

            if (result is null)
            {
                return new VtexIncident();
            }

            return result;
        }

        public async Task<Vtex> GetStatusAsync()
        {
            var requestUri = "";

            var response = await _httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Vtex>(jsonString);

            if (result is null)
            {
                return new Vtex();
            }

            return result;
        }
    }
}
