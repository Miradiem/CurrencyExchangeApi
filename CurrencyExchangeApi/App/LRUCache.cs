using CurrencyExchangeApi.Api;

namespace CurrencyExchangeApi.App
{
    public class LRUCache : ILRUCache
    {
        private int _capacity = 4;
        private Dictionary<string, (LinkedListNode<string> node, ExchangeRates value)> _cache;
        private LinkedList<string> _list;

        public LRUCache()
        {
            _cache = new Dictionary<string, (LinkedListNode<string> node, ExchangeRates value)>(_capacity);
            _list = new LinkedList<string>();
        }

        public ExchangeRates Get(string key)
        {
            if (!_cache.ContainsKey(key)) return null;
                
            var node = _cache[key];
            _list.Remove(node.node);
            _list.AddFirst(node.node);

            return node.value;
        }

        public void Put(string key, ExchangeRates value)
        {
            if (_cache.ContainsKey(key))
            {
                var node = _cache[key];
                _list.Remove(node.node);
                _list.AddFirst(node.node);

                _cache[key] = (node.node, value);
            }
            else
            {
                if (_cache.Count >= _capacity)
                {
                    var removeKey = _list.Last!.Value;
                    _cache.Remove(removeKey);
                    _list.RemoveLast();
                }

                _cache.Add(key, (_list.AddFirst(key), value));
            }
        }      
    }
}