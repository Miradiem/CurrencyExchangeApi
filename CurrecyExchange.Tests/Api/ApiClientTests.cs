using CurrencyExchangeApi.Api;
using FluentAssertions;
using Flurl.Http;
using Xunit;
using Xunit.Abstractions;

namespace CurrecyExchange.Tests.Api
{
    public class ApiClientTests
    {
        private readonly ITestOutputHelper _output;

        public ApiClientTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldCreateApiClient()
        {
            var sut = CreateSut();

            sut.BaseUrl.Should().Be("https://testingcall.com/test");

            _output.WriteLine("Client: {0}", "https://testingcall.com/test");
        }

        private IFlurlClient CreateSut() =>
            new ApiClient("https://testingcall.com/").Create("test");
    }
}
