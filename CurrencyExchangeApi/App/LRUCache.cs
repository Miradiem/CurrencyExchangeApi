namespace CurrencyExchangeApi.App
{
    public class LRUCache : ILRUCache
    {
        private const int _capacity = 4;
        private readonly Dictionary<string, (LinkedListNode<string> node, object value)> _cache;
        private readonly LinkedList<string> _list;

        public LRUCache()
        {
            _cache = new Dictionary<string, (LinkedListNode<string> node, object value)>(_capacity);
            _list = new LinkedList<string>();
        }

        public object Get(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                return null;
            } 
                
            var node = _cache[key];
            _list.Remove(node.node);
            _list.AddFirst(node.node);

            return node.value;
        }

        public void Put(string key, object value)
        {
            if (_cache.ContainsKey(key))
            {
                var node = _cache[key];
                _list.Remove(node.node);
                _list.AddFirst(node.node);

                _cache[key] = (node.node, value);
            }

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