namespace Rihappy.HealthCheck.Data.Rest.Settings
{
    public class HealthCheckSettings
        {
            public SuperAppUrls? SuperApp { get; set; }
            public VtexUrls? Vtex { get; set; }
        }

    public class SuperAppUrls
    {
        public string? AccountUrl { get; set; }
        public string? CheckoutUrl { get; set; }
        public string? CatalogUrl { get; set; }
    }

    public class VtexUrls
    {
        public string? BaseUrl { get; set; }
        public string? IncidentsUrl { get; set; }
    }
}

