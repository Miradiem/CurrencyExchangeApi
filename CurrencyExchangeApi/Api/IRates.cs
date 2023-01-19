namespace CurrencyExchangeApi.Api
{
    public interface IRates
    {
        public Task<ExchangeRates> GetRates(string baseCurrency);
    }
}