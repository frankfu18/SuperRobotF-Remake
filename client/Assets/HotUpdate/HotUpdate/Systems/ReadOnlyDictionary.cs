using System.Collections;
using System.Collections.Generic;

public class ReadOnlyDictionary<T,K> :IDictionary<T,K>
{
    private IDictionary<T, K> dictionary;

    public ReadOnlyDictionary(IDictionary<T,K> _dictionary, bool _copy = true) {
        this.dictionary = _copy ? new Dictionary<T, K>(_dictionary) : _dictionary;
    }
    
    public ReadOnlyDictionary() {
        this.dictionary = new Dictionary<T, K>();
    }
    

    public IEnumerator<KeyValuePair<T, K>> GetEnumerator() {
        return dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return dictionary.GetEnumerator();
    }

    public void Add(KeyValuePair<T, K> item) {
        throw new System.NotImplementedException();
    }

    public void Clear() {
        throw new System.NotImplementedException();
    }

    public bool Contains(KeyValuePair<T, K> item) {
        return dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<T, K>[] array, int arrayIndex) {
        dictionary.CopyTo(array, arrayIndex); 
    }

    public bool Remove(KeyValuePair<T, K> item) {
        throw new System.NotImplementedException();
    }

    public void NSet(T key, K value) {
        dictionary[key] = value;
    }
    public void NAdd(T key, K value) {
        dictionary.Add(key, value);
    }

    public int Count => dictionary.Count;
    public bool IsReadOnly => true;
    public void Add(T key, K value) {
        throw new System.NotImplementedException();
    }

    public bool ContainsKey(T key) {
        return dictionary.ContainsKey(key);
    }

    public bool Remove(T key) {
        throw new System.NotImplementedException();
    }

    public bool TryGetValue(T key, out K value) {
        return dictionary.TryGetValue(key, out value);
    }

    public K this[T key] {
        get => dictionary[key];
        set => throw new System.NotImplementedException();
    }

    public ICollection<T> Keys => dictionary.Keys;
    public ICollection<K> Values => dictionary.Values;
}