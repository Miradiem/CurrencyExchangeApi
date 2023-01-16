using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CurrecyExchange.Tests.App
{
    public class CacheTests
    {
        private readonly ITestOutputHelper _output;

        public CacheTests(ITestOutputHelper output)
        {
            _output = output;
        }

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

            _output.WriteLine("{0}",
                "1.USD out of capacity\n" +
                "2.GBP exists\n" +
                "3.Eur exists");
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
