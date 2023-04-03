using System.Linq;
using UnityEngine;

namespace dbrewka.Collections
{
    [System.Serializable]
    public class KeyValuePair<Key,Value> {
        public Key key;
        public Value value;
    }


    [System.Serializable]
    public class KeyValueList<K,V> {
        
        [SerializeField] private KeyValuePair<K,V>[] _items;
        public int Count => _items.Count();

        public KeyValueList(int i) {
            _items = new KeyValuePair<K,V>[i];
        }

        public V FindValueWithKey(K key) {
            V match = default(V);

            for (int i = 0; i < Count; i++)
            {
                if (_items[i].key.Equals(key)) { match = _items[i].value; break; }
            }

            return match;
        }

    }

}