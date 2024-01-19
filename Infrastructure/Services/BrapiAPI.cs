using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace Infrastructure.Services
{
    public class BrapiAPI: IBrapiService
    {
        private readonly string _brapiAPIKey;
        private readonly string _brapiBaseURL;
        private HttpClient _client = new HttpClient();

        public BrapiAPI(IOptions<ExternalAPIConfigurations> configurations)
        {
            _brapiAPIKey = configurations.Value.BRAPI_API_KEY;
            _brapiBaseURL = configurations.Value.BRAPI_URL;
        }

        public async Task<dynamic> FetchCompanyData(string symbol)
        {
            var url = $"{_brapiBaseURL}{symbol}?token={_brapiAPIKey}";
            return await FetchUrl(url);
        }

        private async Task<dynamic> FetchUrl(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject(responseContent);
        }
    }
}
