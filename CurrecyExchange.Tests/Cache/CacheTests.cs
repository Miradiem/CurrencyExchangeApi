using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.Cache;
using FluentAssertions;
using System.Collections.Generic;

namespace CurrecyExchange.Tests.Cache
{
    public class CacheTests
    {
        public void ShouldReceiveRatesCache()
        {
            var sut = CreateSut();
            var rates = sut.Get("USD");
            rates.Should().NotBeNull();
        }
        private LRUCache CreateSut()
        {
            var exchangeRates = new ExchangeRates()
            {
                Rates = new Dictionary<string, decimal>()
            };
            exchangeRates.Rates.Add("USD", 1);

            var cache = new LRUCache();
            cache.Put("USD", exchangeRates);

            return cache;
        }
    }
}
