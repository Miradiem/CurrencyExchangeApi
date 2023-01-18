using CurrencyExchangeApi.Api;
using FluentAssertions;
using Xunit;

namespace CurrecyExchange.Tests.Api
{
    public class ApiClientTests
    {
        [Fact]
        public void ShouldCreateApiClient()
        {
            var sut = CreateSut();
            var result = sut.Create("test");

            result.BaseUrl.Should().Be("https://testingcall.com/test");
        }

        private ApiClient CreateSut() =>
            new ApiClient("https://testingcall.com/");
    }
}