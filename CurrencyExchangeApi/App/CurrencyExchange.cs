using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.Cache;

namespace CurrencyExchangeApi.App
{
    public class CurrencyExchange
    {
        private readonly ILRUCache _cache;
        private readonly string _baseCurrency;
        private readonly string _quoteCurrency;
        private readonly int _baseAmount;
        

        public CurrencyExchange(ILRUCache cache, ExchangeQuery query)
        {
            _cache = cache;
            _baseCurrency = query.BaseCurrency;
            _quoteCurrency = query.QuoteCurrency;
            _baseAmount = query.BaseAmount;
        }
   
        private async Task<ExchangeRates> RatesCache()
        {
            var exchangeRates = _cache.Get(_baseCurrency);

            if (exchangeRates is null)
            {
                exchangeRates = await new ExchangeApi(
                new ApiClient("https://api.exchangerate-api.com/v4/latest")
                .Create(_baseCurrency))
                .Rates();

                _cache.Put(_baseCurrency, exchangeRates);
            }

            return exchangeRates;
        }

        public async Task<Exchange> GetExchange()
        {
            var exchangeRates = await RatesCache();

            var exchange = new Exchange()
            {
                ExchangeRate = exchangeRates.Rates[_quoteCurrency],
                QuoteAmount = (int)Math.Round(exchangeRates.Rates[_quoteCurrency] * _baseAmount,
                   MidpointRounding.AwayFromZero)

            };

            return exchange;
        }

    }
}
