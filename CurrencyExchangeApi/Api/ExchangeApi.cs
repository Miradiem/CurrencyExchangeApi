using Flurl.Http;

namespace CurrencyExchangeApi.Api
{
    public class ExchangeApi
    {
        private readonly IFlurlClient _client;

        public ExchangeApi(IFlurlClient client)
        {
            _client = client;
        }

        public async Task<ExchangeRates> Rates()
        {
           var exchangeRates = await _client
                .Request()
                .GetJsonAsync<ExchangeRates>();

            if (exchangeRates.Rates is null) throw new ApplicationException("Exchange rates not found");

            return exchangeRates;
        }
    }
}

