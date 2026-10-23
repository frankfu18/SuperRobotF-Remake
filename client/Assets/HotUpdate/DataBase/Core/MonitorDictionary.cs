using System;
using System.Collections;
using System.Collections.Generic;
using DataBase.Core;

namespace db.core
{
    /// <summary>
    /// 支持 变更监控的 字典
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MonitorDictionary<K, V> : IDictionary<K, V> , IMonitor<IDictionary<K, V>>
    {
        private readonly IDictionary<K, V> source;


        public Action OnChanged;

        public MonitorDictionary(IDictionary<K, V> source) {
            this.source = source;
        }
        
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        public IDictionary<K, V> GetDataSource() => source;

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();

        public void Add(KeyValuePair<K, V> item) {
            source.Add(item.Key, item.Value);
            OnChanged?.Invoke();
        }

        public void Clear() {
            source.Clear();
            OnChanged?.Invoke();
        }

        public bool Contains(KeyValuePair<K, V> item) => source.Contains(item);
        
        
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<K, V> item) {
            if (!source.Remove(item.Key)) return false;
            OnChanged?.Invoke();
            return true;
        }

        public int Count => source.Count;
        public bool IsReadOnly => false;
        public void Add(K key, V value) {
            source.Add(key, value);
            OnChanged?.Invoke();
        }

        public bool ContainsKey(K key) => source.ContainsKey(key);

        public bool Remove(K key) {
            if (!source.Remove(key)) {
                return false;
            }
            OnChanged?.Invoke();
            return true;
        }

        public bool TryGetValue(K key, out V value) => source.TryGetValue(key, out value);

        public V this[K key] {
            get => source[key];
            set {
                source[key] = value;
                OnChanged?.Invoke();
            }
        }

        public ICollection<K> Keys => source.Keys;
        public ICollection<V> Values => source.Values;
    }
}