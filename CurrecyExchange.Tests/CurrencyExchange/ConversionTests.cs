using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.CurrencyExchange;
using FluentAssertions;
using Xunit;

namespace CurrecyExchange.Tests.CurrencyExchange
{
    public class ConversionTests
    {
        [Fact]
        public void ShouldGetExchange()
        {
            var exchangeRates = new ExchangeRates();
            exchangeRates.Rates.Add("USD", 1);

            var sut = CreateSut();
            var result = sut.GetExchange(exchangeRates, "USD", 100);

            result.ExchangeRate.Should().Be(1);
            result.QuoteAmount.Should().Be(100);
        }

        private Conversion CreateSut() =>
            new Conversion();
    }
}
