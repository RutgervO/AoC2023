namespace AOC.util;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> self)
    {
        return self.SelectMany(inner => inner.Select((item, index) => new { item, index }))
            .GroupBy(i => i.index, i => i.item)
            .Select(g => g.ToList());
    }
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)       
        => self.Select((item, index) => (item, index));
    
    public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> self,
        Func<TSource, TResult> selector, Func<TSource, bool> predicate)
    {
        return self.Where(predicate).Select(selector);
    }

    public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> self,
        Func<TSource, int, TResult> selector, Func<TSource, int, bool> predicate)
    {
        foreach (var (item, index) in self.WithIndex())
        {
            if (predicate(item, index))
            {
                yield return selector(item, index);
            }
        }
    }
    public static IEnumerable<T> RepeatIndefinitely<T>(this IEnumerable<T> source)
    {
        var list = source.ToList();
        while (true)
        {
            foreach (var item in list)
            {
                yield return item;
            }
        }
    }
    
    public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>
    (this IEnumerable<TSource> source,
        Func<TSource, TSource, TResult> projection)
    {
        using (var iterator = source.GetEnumerator())
        {
            if (!iterator.MoveNext())
            {
                yield break;
            }
            TSource previous = iterator.Current;
            while (iterator.MoveNext())
            {
                yield return projection(previous, iterator.Current);
                previous = iterator.Current;
            }
        }
    }
}