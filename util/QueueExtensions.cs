namespace AOC.util;

public static class QueueExtensions
{
    public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, int chunkSize) 
    {
        for (int i = 0; i < chunkSize && queue.Count > 0; i++)
        {
            yield return queue.Dequeue();
        }
    }
    public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, long chunkSize) 
    {
        for (long i = 0; i < chunkSize && queue.Count > 0; i++)
        {
            yield return queue.Dequeue();
        }
    }
}