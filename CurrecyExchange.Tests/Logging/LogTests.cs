using CurrencyExchangeApi.Logging;

namespace CurrecyExchange.Tests.Logging
{
    public class LogTests
    {
        public void ShouldLog()
        {
            Log.Info("test logging");
        }
    }
}
