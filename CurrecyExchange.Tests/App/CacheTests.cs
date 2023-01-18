using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentAssertions;
using Xunit;

namespace CurrecyExchange.Tests.App
{
    public class CacheTests
    {
        [Fact]
        public void ShouldCacheTwoObjects()
        {
            var sut = CreateSut();

            var usdResult = (ExchangeRates)sut.Get("USD");
            var gbpResult = (ExchangeRates)sut.Get("GBP");
            var eurResult = (ExchangeRates)sut.Get("EUR");

            usdResult.Should().BeNull();
            gbpResult.Rates.Should().ContainKey("GBP");
            eurResult.Rates.Should().ContainKey("EUR");
        }

        private LRUCache CreateSut()
        {
            var usdRates = new ExchangeRates();
            var gbpRates = new ExchangeRates();
            var eurRates = new ExchangeRates();

            usdRates.Rates.Add("USD", 1);
            gbpRates.Rates.Add("GBP", 2);
            eurRates.Rates.Add("EUR", 3);

            var cache = new LRUCache(2);
            cache.Put("USD", usdRates);
            cache.Put("GBP", gbpRates);
            cache.Put("EUR", eurRates);

            return cache;
        }
    }
}
