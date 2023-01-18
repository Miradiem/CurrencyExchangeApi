using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using Xunit;

namespace CurrecyExchange.Tests.Api
{
    public class CachedRatesTests
    {
        private readonly Mock<ILRUCache> _cacheMock;

        public CachedRatesTests()
        {
            _cacheMock = new Mock<ILRUCache>();
        }

        [Fact]
        public void ShouldGetCachedRates()
        {
            using (var httpTest = new HttpTest())
            {
                var exchangeRates = new ExchangeRates();
                exchangeRates.Rates.Add("USD", 1);

                httpTest
                    .ForCallsTo("https://testingcall.com/test/USD")
                    .RespondWithJson(exchangeRates);

                var sut = CreateSut();
                var result = sut.GetRates("USD");

                result.Result.Rates.Should().Contain("USD", 1);
            }
        }

        private CachedRates CreateSut()
        {
            var latestRates = new LatestRates(new ApiClient("https://testingcall.com/test"));

            return new CachedRates(_cacheMock.Object, latestRates);
        }
    }
}
