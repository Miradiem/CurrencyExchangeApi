using Flurl.Http;

namespace CurrencyExchangeApi.Api
{
    public class LatestRates : IRates
    {
        private readonly ApiClient _client;

        public LatestRates(ApiClient client)
        {
            _client = client;
        }

        public async Task<ExchangeRates> GetRates(string baseCurrency)
        {
            var exchangeRates = await _client
                .Create(baseCurrency)
                .Request()
                .GetJsonAsync<ExchangeRates>();

            if (exchangeRates.Rates.Any() is false)
            {
                throw new NotImplementedException("Exchange rates not found.");
            }

            return exchangeRates;
        }       
    }
}
