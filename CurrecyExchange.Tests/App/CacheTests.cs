using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CurrecyExchange.Tests.App
{
    public class CacheTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Mock<ILRUCache> _cacheMock;

        public CacheTests(ITestOutputHelper output)
        {
            _output = output;
            _cacheMock = new Mock<ILRUCache>();
        }

        [Fact]
        public void ShouldReceiveRatesCache()
        {
            var sut = CreateSut();

            var result = sut.Get("USD");

            result.Should().NotBeNull();
        }

        private LRUCache CreateSut()
        {
            var exchangeRates = new ExchangeRates();
            exchangeRates.Rates.Add("USD", 1);

            var cache = _cacheMock.Object;
            cache.Put("USD", exchangeRates);

            return (LRUCache)cache;
        }
    }
}
