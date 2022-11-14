using CurrencyExchangeApi.Api;
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
         
        }

        private Conversion CreateSut()
        {

            return new Conversion();
        }
    }
}
