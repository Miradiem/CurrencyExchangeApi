using CurrencyExchangeApi.App;
using CurrencyExchangeApi.CurrencyExchange;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using Xunit;

namespace CurrecyExchange.Tests.Validation
{
    public class ValidationTests
    {
       

        [Theory, MemberData(nameof(ExchangeQueryTestData))]
        public void ShouldValidateQuery(QuoteQuery query)
        {
            var sut = CreateSut();
            var result = sut.TestValidate(query);

            result.ShouldHaveValidationErrorFor(e => e.BaseCurrency);
            result.ShouldHaveValidationErrorFor(e => e.QuoteCurrency);
            result.ShouldHaveValidationErrorFor(e => e.BaseAmount);
        }

        public static IEnumerable<object[]> ExchangeQueryTestData =>
           new List<object[]> {
                new object[] { new QuoteQuery() { BaseCurrency = "USDD", QuoteCurrency = "GB", BaseAmount = 0 }},
                new object[] { new QuoteQuery() { BaseCurrency = "usd", QuoteCurrency = "gBP", BaseAmount = 0 }},
                new object[] { new QuoteQuery() { BaseCurrency = "123", QuoteCurrency = "1BP", BaseAmount = 0 }},
                new object[] { new QuoteQuery() { BaseCurrency = "", QuoteCurrency = "", BaseAmount = 0 }}
           };

        private QueryValidator CreateSut()
        {
            return new QueryValidator();
        }

        

        
    }
}
