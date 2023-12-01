namespace AOC.util;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new() where TKey : notnull
{
    public DefaultDictionary() : base()
    {
    }

    public DefaultDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
    {
    }

    public DefaultDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
    {
    }
    
    public new TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var val)) return val;
            val = new();
            Add(key, val);
            return val;
        }
        set => base[key] = value;
    }
}