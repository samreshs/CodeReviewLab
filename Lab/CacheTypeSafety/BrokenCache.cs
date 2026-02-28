using System.Collections.Concurrent;

namespace Lab.CacheTypeSafety;

public sealed class BrokenTupleCache
{
    // key -> (value, type)
    private readonly ConcurrentDictionary<string, (object Value, Type Type)> _cache = new();

    public void Set<T>(string key, T value)
    {
        _cache[key] = (value!, typeof(T));
    }

    public T Get<T>(string key)
    {
        if (!_cache.TryGetValue(key, out var entry))
            throw new KeyNotFoundException(key);

        // Runtime check only — still unsafe design
        if (entry.Type != typeof(T))
            throw new InvalidCastException(
                $"Stored type is {entry.Type.Name}, requested {typeof(T).Name}");

        return (T)entry.Value;
    }
}