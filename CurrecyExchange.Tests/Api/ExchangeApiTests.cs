using CurrencyExchangeApi.Api;
using FluentAssertions;
using Flurl.Http;
using System.Threading.Tasks;

namespace CurrecyExchange.Api.Tests
{
    public class ExchangeApiTests
    {
        public async Task ShouldGetRates()
        {
            var sut = CreateSut();
            var rates = await sut.Rates();
            rates.Should().NotBeNull();
        }

        private static ExchangeApi CreateSut()
        {
            return new ExchangeApi(
                new FlurlClient("https://api.exchangerate-api.com/v4/latest/usd"));
        }
    }
}
