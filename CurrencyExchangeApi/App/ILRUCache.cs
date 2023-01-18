namespace CurrencyExchangeApi.App
{
    public interface ILRUCache
    {
        public object Get(string key);

        public void Put(string key, object value);
    }
}
