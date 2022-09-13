using Serilog;
using Serilog.Core;

namespace CurrencyExchangeApi.Logging
{
    public static class Log
    {
        private static readonly Logger _logger = new LoggerConfiguration()
                .WriteTo.File("Logging/log.txt")
                .CreateLogger();

        public static void Info(string message) =>
            _logger.Information(message);

        public static void Error(string message) =>
            _logger.Error(message);
    }
}

