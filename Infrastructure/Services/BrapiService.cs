using Domain.Entities;
using Domain.Entities.Brapi;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Infrastructure.Services
{
    public class BrapiService: IBrapiService
    {
        private readonly IShareService _shareService;
        private readonly string _brapiAPIKey;
        private readonly string _brapiBaseURL;
        private HttpClient _client = new HttpClient();

        public BrapiService(IOptions<ExternalAPIConfigurations> configurations, IShareService shareService)
        {
            _brapiAPIKey = configurations.Value.BRAPI_API_KEY;
            _brapiBaseURL = configurations.Value.BRAPI_URL;
            _shareService = shareService;
        }

        public async Task<dynamic> GetCompanyQuote(string symbol)
        {
            var url = $"{_brapiBaseURL}quote/{symbol}?token={_brapiAPIKey}";
            return await FetchUrl(url);
        }

        public async Task<dynamic> GetCompanyQuoteHistory(string symbol, int months)
        {
            var url = $"{_brapiBaseURL}quote/{symbol}?interval={months}mo&token={_brapiAPIKey}";
            return await FetchUrl(url);
        }

        private async Task<dynamic> FetchUrl(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            Share share = ConvertJsonToShare(responseContent);
            share.Id = Guid.NewGuid();
            try
            {
                await _shareService.AddAsync(share);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return share;
        }

        public Share ConvertJsonToShare(string jsonResponse)
        {
            var jsonObject = JObject.Parse(jsonResponse);

            var shareData = jsonObject["results"]?.First;

            if (shareData == null)
            {
                throw new InvalidOperationException("Invalid JSON response");
            }
            Share share = shareData.ToObject<Share>(); 
            return share;
        }

        public Task<string> SaveAllCompanyQuotes()
        {
            foreach (AcoesEnum symbol in Enum.GetValues(typeof(AcoesEnum)))
            {
                Console.WriteLine(symbol);
            }

            return "Todas as cotacoes salvas com sucesso!";
        }
    }
}
