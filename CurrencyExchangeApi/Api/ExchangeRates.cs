using Newtonsoft.Json;

namespace CurrencyExchangeApi.Api
{
    public class ExchangeRates
    {
        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
