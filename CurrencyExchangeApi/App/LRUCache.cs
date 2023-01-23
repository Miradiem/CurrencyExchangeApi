namespace CurrencyExchangeApi.App
{
    public class LRUCache : ILRUCache
    {
        private readonly int _capacity;
        private readonly Dictionary<string, (LinkedListNode<string> node, object value)> _cache;
        private readonly LinkedList<string> _list;

        public LRUCache(int capacity)
        {
            _cache = new Dictionary<string, (LinkedListNode<string> node, object value)>(_capacity);
            _list = new LinkedList<string>();
            _capacity = capacity;
        }

        public object Get(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                return null;
            } 
                
            var keyNode = _cache[key];
            _list.Remove(keyNode.node);
            _list.AddFirst(keyNode.node);

            return keyNode.value;
        }

        public void Put(string key, object value)
        {
            if (_cache.ContainsKey(key))
            {
                var keyNode = _cache[key];
                _list.Remove(keyNode.node);
                _list.AddFirst(keyNode.node);

                _cache[key] = (keyNode.node, value);
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