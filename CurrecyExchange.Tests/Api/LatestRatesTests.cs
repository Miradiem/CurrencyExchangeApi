using CurrencyExchangeApi.Api;
using FluentAssertions;
using Flurl.Http.Testing;
using Xunit;

namespace CurrecyExchange.Tests.Api
{
    public class LatestRatesTests
    {
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

                var sut = CreateSut();
                var result = sut.GetRates("USD");

                result.Result.Rates.Should().Contain("USD", 1);
            }
        }

        private LatestRates CreateSut()
        {
            var client = new ApiClient("https://testingcall.com/test");

            return new LatestRates(client);
        }
    }
}
