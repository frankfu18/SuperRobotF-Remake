using System;
using System.Collections.Concurrent;

/// <summary>
/// 简单的本地缓存工具，可以指定失效时间
/// </summary>
public class CacheUtil
{
    
    private static readonly ConcurrentDictionary<string, CacheItem> _cache = new();

    private class CacheItem
    {
        public object Value { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
    
    public static void Set(string key, object value, int expireSeconds)
    {
        _cache[key] = new CacheItem
        {
            Value = value,
            ExpiryTime = DateTime.Now.AddSeconds(expireSeconds)
        };
    }

    public static T Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var item))
        {
            if (DateTime.Now < item.ExpiryTime)
            {
                return (T)item.Value;
            }
            _cache.TryRemove(key, out _); // 过期自动移除
        }
        return default;
    }

    public static bool TryGet<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out var item))
        {
            if (DateTime.Now < item.ExpiryTime)
            {
                value = (T)item.Value;
                return true;
            }
            _cache.TryRemove(key, out _); // 过期自动移除
        }
        value = default;
        return false;
    }

    public static void Remove(string key)
    {
        _cache.TryRemove(key, out _);
    }

    public static void ClearAll()
    {
        _cache.Clear();
    }
    
}