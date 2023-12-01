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
}