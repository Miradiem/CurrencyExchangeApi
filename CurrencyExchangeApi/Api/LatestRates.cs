namespace CurrencyExchangeApi.Api
{
    public class LatestRates : IRates
    {
        private const string _url = "https://api.exchangerate-api.com/v4/latest";

        public async Task<ExchangeRates> GetRates(string baseCurrency) =>
            await new RatesApi(
              new ApiClient(_url)
              .Create(baseCurrency))
              .Rates();
    }
}
