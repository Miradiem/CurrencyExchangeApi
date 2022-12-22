using AutoFixture;
using CurrencyExchangeApi.App;
using CurrencyExchangeApi.CurrencyExchange;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrecyExchange.Tests.CurrencyExchange
{
    public class ExchangeControllerTests
    {
        private readonly Mock<Exchange> _exchangeMock;
        private Fixture _fixture;
        private ExchangeController _controller;
        

        public ExchangeControllerTests()
        {
            _exchangeMock = new Mock<Exchange>();
            _fixture = new Fixture();
        }
        [Fact]
        public async Task ShouldGetExchange()
        {

            var q = new QuoteQuery()
            {
                BaseCurrency = "USD",
                QuoteCurrency = "GBP",
                BaseAmount = 1
            };
            //_controller = new ExchangeController()
            var result = await _controller.Get(q);
            //var obj = result as ObjectResult;

            result.Should().Be(200);
        }
    }
}
