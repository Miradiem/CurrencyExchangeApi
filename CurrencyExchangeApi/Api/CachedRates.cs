using CurrencyExchangeApi.App;

namespace CurrencyExchangeApi.Api
{
    public class CachedRates : IRates
    {
        private readonly ILRUCache _cache;
        private readonly LatestRates _latestRates;

        public CachedRates(ILRUCache cache, LatestRates latestRates)
        {
            _cache = cache;
            _latestRates = latestRates;
        }

        public async Task<ExchangeRates> GetRates(string baseCurrency)
        {
            var exchangeRates = _cache.Get(baseCurrency);

            if (exchangeRates is null)
            {
                exchangeRates = await _latestRates.GetRates(baseCurrency);

                _cache.Put(baseCurrency, exchangeRates);
            }

            return (ExchangeRates)exchangeRates;
        }
    }
}
