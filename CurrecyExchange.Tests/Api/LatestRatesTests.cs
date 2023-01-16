using CurrencyExchangeApi.Api;
using FluentAssertions;
using Flurl.Http.Testing;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CurrecyExchange.Tests.Api
{
    public class LatestRatesTests
    {
        private readonly ITestOutputHelper _output;

        public LatestRatesTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldGetLatestRates()
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
            var client = new ApiClient("https://testingcall.com/test");

            return new LatestRates(client).GetRates("USD");
        }
           
    }
}
