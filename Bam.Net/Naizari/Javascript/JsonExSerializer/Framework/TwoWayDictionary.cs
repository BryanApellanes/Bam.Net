/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization.Framework
{
    /// <summary>
    /// A Bidirectional dictionary used by the context to store types.  items can be looked based on
    /// key or value.  A lookup on the value will return the key.
    /// </summary>
    /// <typeparam name="K">the type of the key</typeparam>
    /// <typeparam name="V">the type of the value</typeparam>
    public class TwoWayDictionary<K, V> : IDictionary<K, V> 
    {
        private IDictionary<K, V> _impl;
        private IDictionary<V, K> _reverseImpl;

        public TwoWayDictionary()
        {
            _impl = new Dictionary<K, V>();
            _reverseImpl = new Dictionary<V, K>();
        }

        #region IDictionary<K,V> Members

        public void Add(K key, V value)
        {
            _impl.Add(key, value);
            _reverseImpl.Add(value, key);
        }

        public bool ContainsKey(K key)
        {
            return _impl.ContainsKey(key);
        }

        public ICollection<K> Keys
        {
            get { return _impl.Keys; }
        }

        public bool Remove(K key)
        {
            V value;
            if (_impl.TryGetValue(key, out value))
            {
                _impl.Remove(key);
                _reverseImpl.Remove(value);
                return true;
            }
            return false;
        }

        public bool TryGetValue(K key, out V value)
        {
            return _impl.TryGetValue(key, out value);
        }

        public bool TryGetKey(V value, out K key)
        {
            return _reverseImpl.TryGetValue(value, out key);
        }

        public ICollection<V> Values
        {
            get { return _impl.Values; }
        }

        public V this[K key]
        {
            get
            {
                return _impl[key];
            }
            set
            {
                _impl[key] = value;
                _reverseImpl[value] = key;
            }
        }

        public K this[V val]
        {
            get
            {
                return _reverseImpl[val];
            }
            set
            {
                this[value] = val;
            }
        }
        #endregion

        #region ICollection<KeyValuePair<K,V>> Members

        public void Add(KeyValuePair<K, V> item)
        {
            _impl.Add(item);
            _reverseImpl.Add(new KeyValuePair<V, K>(item.Value, item.Key));
        }

        public void Clear()
        {
            _impl.Clear();
            _reverseImpl.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return _impl.Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            _impl.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _impl.Count; }
        }

        public bool IsReadOnly
        {
            get { return _impl.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            bool success = _impl.Remove(item);
            _reverseImpl.Remove(new KeyValuePair<V, K>(item.Value, item.Key));
            return success;
        }

        #endregion

        #region IEnumerable<KeyValuePair<K,V>> Members

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_impl).GetEnumerator();
        }

        #endregion
    }
}
