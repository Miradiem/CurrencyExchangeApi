using CurrencyExchangeApi.App;
using CurrencyExchangeApi.CurrencyExchange;
using FluentAssertions;
using System.Threading.Tasks;

namespace CurrecyExchange.Tests.CurrencyExchange
{
    public class ConversionTests
    {
        public async Task ShouldGetExchange()
        {
            var sut = CreateSut();
            var exchange = await sut.GetExchange();
            exchange.Should().NotBeNull();
        }

        private Conversion CreateSut()
        {
            var cache = new LRUCache();
            var query = new QuoteQuery();
            query.BaseCurrency = "USD";
            query.QuoteCurrency = "GBP";
            query.BaseAmount = 100;

            return new Conversion(cache, query);  
        }
    }
}
