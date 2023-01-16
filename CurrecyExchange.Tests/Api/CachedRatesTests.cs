using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CurrecyExchange.Tests.Api
{
    public class CachedRatesTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Mock<ILRUCache> _cacheMock;

        public CachedRatesTests(ITestOutputHelper output)
        {
            _output = output;
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

                var result = CreateSut();
                result.Result.Rates.Should().Contain("USD", 1);

                _output.WriteLine("{0}", "\"USD\", 1");
            }
        }

        private Task<ExchangeRates> CreateSut()
        {
            var latestRates = new LatestRates(new ApiClient("https://testingcall.com/test"));

            return  new CachedRates(_cacheMock.Object, latestRates).GetRates("USD");
        }
    }
}
