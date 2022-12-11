using Flurl.Http;

namespace CurrencyExchangeApi.Api
{
    public class RatesApi
    {
        private readonly IFlurlClient _client;

        public RatesApi(IFlurlClient client)
        {
            _client = client;
        }

        public async Task<ExchangeRates> Rates()
        {
            var exchangeRates = await _client
                .Request()
                .GetJsonAsync<ExchangeRates>();

            if (exchangeRates.Rates is null)
            {
                throw new Exception();
            }
                
            return exchangeRates;
        }
    }
}

