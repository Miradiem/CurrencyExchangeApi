using CurrencyExchangeApi.CurrencyExchange;
using Newtonsoft.Json;
using System.Text;

namespace CurrencyExchangeApi.Storring
{
    public class ExchangeFile
    {
        private SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private readonly string _file;

        public ExchangeFile(string file)
        {
            _file = file;
        }

        public async Task Save(Exchange exchange)
        {
            await _mutex.WaitAsync();
            try
            {
                await WriteTextAsync(_file, Serialize(exchange));
            }
            finally
            {
                _mutex.Release();
            }
        }

        async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using var destination =
                new FileStream(
                    filePath,
                    FileMode.Append, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true);

            await destination.WriteAsync(encodedText, 0, encodedText.Length);
        }

        private string Serialize(Exchange exchange)
        {
            return JsonConvert.SerializeObject(exchange);
        }
    }
}
