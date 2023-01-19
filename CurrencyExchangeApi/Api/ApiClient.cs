using Flurl;
using Flurl.Http;
using Serilog;

namespace CurrencyExchangeApi.Api
{
    public class ApiClient
    {
        private readonly string _url;

        public ApiClient(string url)
        {
            _url = url;
        }

        public IFlurlClient Create(string baseCurrency) =>
            new FlurlClient(_url.AppendPathSegment(baseCurrency));
    }
}
