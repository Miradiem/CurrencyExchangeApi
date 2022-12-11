using CurrencyExchangeApi.Api;

namespace CurrencyExchangeApi.CurrencyExchange
{
    public class Conversion
    {
        public Exchange GetExchange(ExchangeRates rates, string quoteCurrency, int baseAmount) =>
            new Exchange()
            {
                ExchangeRate = rates.Rates[quoteCurrency],
                QuoteAmount = (int)Math.Round(rates.Rates[quoteCurrency] * baseAmount,
                   MidpointRounding.AwayFromZero)
            };
    }
}
