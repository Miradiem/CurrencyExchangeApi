﻿using CurrencyExchangeApi.App;
using CurrencyExchangeApi.Validation;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using Xunit;

namespace CurrecyExchange.Tests.Validation
{
    public class ValidationTestsXUnit
    {
        public static IEnumerable<object[]> ExchangeQueryTestData =>
            new List<object[]> {
                new object[] { new ExchangeQuery() { BaseCurrency = "USDD", QuoteCurrency = "GB", BaseAmount = 0 }},
                new object[] { new ExchangeQuery() { BaseCurrency = "usd", QuoteCurrency = "gBP", BaseAmount = 0 }},
                new object[] { new ExchangeQuery() { BaseCurrency = "123", QuoteCurrency = "1BP", BaseAmount = 0 }},
                new object[] { new ExchangeQuery() { BaseCurrency = "", QuoteCurrency = "", BaseAmount = 0 }}
            };

        [Theory, MemberData(nameof(ExchangeQueryTestData))]
        public void ShouldValidateQuery(ExchangeQuery query)
        {
            var sut = CreateSut();

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(e => e.BaseCurrency);
            result.ShouldHaveValidationErrorFor(e => e.QuoteCurrency);
            result.ShouldHaveValidationErrorFor(e => e.BaseAmount);
        }

        private QueryValidator CreateSut()
        {
            return new QueryValidator();
        }

        

        
    }
}
