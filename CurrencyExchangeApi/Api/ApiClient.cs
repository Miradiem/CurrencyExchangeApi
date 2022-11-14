using Flurl;
using Flurl.Http;

namespace CurrencyExchangeApi.Api
{
    public class ApiClient
    {
        private readonly string _url;

        public ApiClient(string url)
        {
            _url = url;
        }

        public IFlurlClient Create(string baseCurrency)
        {
            var client = new FlurlClient(_url.AppendPathSegment(baseCurrency));

            client.Settings.BeforeCall = (call) =>
            {
                Logs.Log.Info($"Calling {call.HttpRequestMessage.RequestUri}");
            };
            client.Settings.AfterCall = (call) =>
            {
                Logs.Log.Info($"Call status code: {call.Response.StatusCode}");
            };
            
            return client;
        }
    }
}
