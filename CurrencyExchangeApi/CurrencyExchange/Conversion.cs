using CurrencyExchangeApi.Api;

namespace CurrencyExchangeApi.CurrencyExchange
{
    public class Conversion
    {
        public Exchange GetExchange(ExchangeRates rates, string quoteCurrency, int baseAmount)
        {
            var exchange = new Exchange()
            {
                ExchangeRate = rates.Rates[quoteCurrency],
                QuoteAmount = (int)Math.Round(rates.Rates[quoteCurrency] * baseAmount,
                  MidpointRounding.AwayFromZero)
            };

            if (exchange.ExchangeRate == 0 || exchange.QuoteAmount == 0)
            {
                throw new NotImplementedException("Exchange not found.");
            }

            return exchange;
        }  
    }
}
