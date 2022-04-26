using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;
using System.Text;

namespace OrbitalWitnessAPI.API
{
    public class OWLegacyApiWrapper : IOWLegacyApiWrapper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private readonly string _auth;
        private readonly string _uri;

        public OWLegacyApiWrapper(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            _auth = GetAuth();
            _uri = GetUri();
        }

        private string GetAuth()
        {
            //Pull values out of configuration file
            string username = _configuration.GetSection("API:OWLegacy:Username").Value;
            string password = _configuration.GetSection("API:OWLegacy:Password").Value;

            //Format username and password
            return $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"))}";
        }

        private string GetUri()
        {
            //If in production, use this value
            string endpoint = _configuration.GetSection("API:OWLegacy:ProdUri").Value;

            #if DEBUG
            //If in debug environment, use the local uri
            endpoint = _configuration.GetSection("API:OWLegacy:DevelopmentUri").Value;
            #endif

            return endpoint;
        }

        public async Task<IList<RawScheduleNoticeOfLease>> GetSchedules()
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", _auth);

            return await client.GetFromJsonAsync<List<RawScheduleNoticeOfLease>>(_uri + "/schedules");
        }
    }
}
