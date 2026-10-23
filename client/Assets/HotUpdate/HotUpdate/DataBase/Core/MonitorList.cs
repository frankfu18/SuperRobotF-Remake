using System;
using System.Collections;
using System.Collections.Generic;
using DataBase.Core;

namespace db.core
{
    public class MonitorList<T> : IList<T> , IMonitor<IList<T>>
    {

        private readonly IList<T> source;

        public Action OnChanged;

        public MonitorList(IList<T> source) {
            this.source = source;
        }

        public IEnumerator<T> GetEnumerator() => source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();

        public void Add(T item) {
            source.Add(item);
            OnChanged?.Invoke();
        }

        public void Clear() {
            source.Clear();
            OnChanged?.Invoke();
        }

        public bool Contains(T item) => source.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) {
            source.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) {
            if (!source.Remove(item)) {
                return false;
            }
            OnChanged?.Invoke();
            return true;
        }

        public int Count => source.Count;
        public bool IsReadOnly => source.IsReadOnly;
        public int IndexOf(T item) => source.IndexOf(item);

        public void Insert(int index, T item) {
            source.Insert(index, item);
            OnChanged?.Invoke();
        }

        public void RemoveAt(int index) {
            source.RemoveAt(index);
            OnChanged?.Invoke();
        }

        public T this[int index] {
            get => source[index];
            set {
                source[index] = value;
                OnChanged?.Invoke();
            }
        }

        public IList<T> GetDataSource() => source;
    }
}