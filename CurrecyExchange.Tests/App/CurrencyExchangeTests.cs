using CurrencyExchangeApi.App;
using CurrencyExchangeApi.Cache;
using System.Threading.Tasks;
using FluentAssertions;

namespace CurrecyExchange.Tests.App
{
    public class CurrencyExchangeTests
    {
        public async Task ShouldGetExchange()
        {
            var sut = CreateSut();
            var exchange = await sut.GetExchange();
            exchange.Should().NotBeNull();
        }

        private CurrencyExchange CreateSut()
        {
            var cache = new LRUCache();
            var query = new ExchangeQuery();
            query.BaseCurrency = "USD";
            query.QuoteCurrency = "GBP";
            query.BaseAmount = 100;

            return new CurrencyExchange(cache, query);  
        }
    }
}
