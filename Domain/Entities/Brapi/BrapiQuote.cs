using Newtonsoft.Json;

namespace Domain.Entities.Brapi
{
    public class BrapiQuote
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("requestedAt")]
        public DateTime RequestedAt { get; set; }

        [JsonProperty("took")]
        public string Took { get; set; }
    }

    public class Result
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        // Outros campos omitidos por brevidade...

        [JsonProperty("marketCap")]
        public long MarketCap { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        [JsonProperty("priceEarnings")]
        public double PriceEarnings { get; set; }

        [JsonProperty("earningsPerShare")]
        public double EarningsPerShare { get; set; }

        [JsonProperty("logourl")]
        public string LogoUrl { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}