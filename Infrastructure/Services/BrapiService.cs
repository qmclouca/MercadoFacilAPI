using Domain.Entities.Brapi;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace Infrastructure.Services
{
    public class BrapiService: IBrapiService
    {
        private readonly string _brapiAPIKey;
        private readonly string _brapiBaseURL;
        private HttpClient _client = new HttpClient();

        public BrapiService(IOptions<ExternalAPIConfigurations> configurations)
        {
            _brapiAPIKey = configurations.Value.BRAPI_API_KEY;
            _brapiBaseURL = configurations.Value.BRAPI_URL;
        }

        public async Task<dynamic> GetCompanyQuote(string symbol)
        {
            var url = $"{_brapiBaseURL}quote/{symbol}?token={_brapiAPIKey}";
            return await FetchUrl(url);
        }

        private async Task<dynamic> FetchUrl(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();            
            return JsonConvert.DeserializeObject<BrapiQuote>(responseContent);
        }
    }
}
