using AutoFixture;
using CurrencyExchangeApi.Api;
using CurrencyExchangeApi.App;
using CurrencyExchangeApi.CurrencyExchange;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CurrecyExchange.Tests.CurrencyExchange
{
    public class ExchangeControllerTests
    {
        private readonly Mock<IRates> _mockRates;
        private readonly Mock<ILogger<ExchangeController>> _mockLogger;
        private readonly Fixture _fixture;
        private readonly IValidator<QuoteQuery> _validator;
        private readonly ExchangeController _controller;

        
        public ExchangeControllerTests()
        {
            _mockRates = new Mock<IRates>();
            _mockLogger = new Mock<ILogger<ExchangeController>>();
            _fixture = new Fixture();
            _validator = new QuoteValidator();
            _controller = new ExchangeController(_mockLogger.Object, _mockRates.Object, _validator);
        }

        [Fact]
        public async Task ShouldGetExchange()
        {
            var exhangeRates = _fixture.Create<Task<ExchangeRates>>();
            exhangeRates.Result.Rates.Add("USD", 1);

            _mockRates.Setup(rates => rates.GetRates("USD"))
                .Returns(exhangeRates);
                 
            var query = new QuoteQuery() 
            {
                BaseCurrency = "USD",
                QuoteCurrency = "USD",
                BaseAmount = 100
            };

            var result = await _controller.Get(query) as ObjectResult;

            result.StatusCode.Should().Be(200);
        }
    }
}
