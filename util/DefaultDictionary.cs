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
            val = new TValue();
            Add(key, val);
            return val;
        }
        set => base[key] = value;
    }
}

public class DefaultValueDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new() where TKey : notnull
{
    private TValue DefaultValue { get; set; }

    public DefaultValueDictionary(TValue defaultValue) : base()
    {
        DefaultValue = defaultValue;
    }

    public DefaultValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, TValue defaultValue) : base(collection)
    {
        DefaultValue = defaultValue;
    }

    public DefaultValueDictionary(IDictionary<TKey, TValue> dictionary, TValue defaultValue) : base(dictionary)
    {
        DefaultValue = defaultValue;
    }
    
    public new TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var val)) return val;
            val = DefaultValue;
            Add(key, val);
            return val;
        }
        set => base[key] = value;
    }
}
