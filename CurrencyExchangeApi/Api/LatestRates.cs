namespace CurrencyExchangeApi.Api
{
    public class LatestRates : IRates
    {
        public async Task<ExchangeRates> GetRates(string baseCurrency) =>
            await new RatesApi(
              new ApiClient("https://api.exchangerate-api.com/v4/latest")
              .Create(baseCurrency))
              .Rates();
    }
}
