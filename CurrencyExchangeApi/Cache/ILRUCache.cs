using CurrencyExchangeApi.Api;

namespace CurrencyExchangeApi.Cache
{
    public interface ILRUCache
    {
        public ExchangeRates Get(string key);
        public void Put(string key, ExchangeRates value);

    }
}
