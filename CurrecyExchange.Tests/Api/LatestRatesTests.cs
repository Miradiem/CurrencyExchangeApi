using Flurl.Http;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CurrencyExchangeApi.Api;

namespace CurrecyExchange.Tests.Api
{
    public class LatestRatesTests
    {
        private readonly IRates _rates;

        [Fact]
        public async Task ShouldGetRates()
        {
            var testRates = _rates.GetRates("USD");


            using (var httpTest = new HttpTest())
            {
                var ratesTest = "'rates': '{'USD': '1'}'";
                httpTest
                    .ForCallsTo("*https://testingcall.com/test*")
                    .WithVerb("Get")
                    .RespondWith(ratesTest, 200);

                var result = await "https://testingcall.com/test".GetAsync();
                Assert.Contains("USD", await result.GetStringAsync());
            }
           
        }
    }
}
