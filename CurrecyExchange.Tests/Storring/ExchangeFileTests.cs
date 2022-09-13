using CurrencyExchangeApi.CurrencyExchange;
using CurrencyExchangeApi.Storring;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;

namespace CurrecyExchange.Tests.Storring
{
    public class ExchangeFileTests
    {
        public async Task ShouldStore()
        {
            var file = Path.GetTempFileName();

            var sut = new ExchangeFile(file);
            await sut.Save(new Exchange()
            {
                ExchangeRate = 1,
                QuoteAmount = 100
            });

            var result = await File.ReadAllTextAsync(file);
            result.Should().NotBeNullOrEmpty();
                
        }
       


    }
}
