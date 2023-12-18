using System.Diagnostics;
using AOC.days;

namespace AOC;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine("AOC 2023 runner");
        
        var skip = 0;
        var limit = 25;
        if (DateTime.Now.Year == 2023 && DateTime.Now.Month == 12 && DateTime.Now.Day <= 25)
        {
            skip = DateTime.Now.Day - 1;
            limit = 1;
        }

        var baseName = new string(typeof(Day<>).FullName!.TakeWhile(c => c != '`').ToArray());
        var days = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.FullName!.StartsWith(baseName))
            .Where(p => p.Name is [_, _, _, >= '0' and < '3', _])
            .Select(p => new Tuple<Type,int> ( p, int.Parse(p.Name[3..])))
            .ToArray();
        skip = Math.Min(days.Length - 1, skip); // Don't skip past the last day
        
        Console.WriteLine($"Running the last {(limit == 1 ? "" : $"{limit} ")}day{(limit is 0 or > 1 ? "s" : "")} out of {days.Length}:");
        Console.WriteLine();
        
        var watch = Stopwatch.StartNew();
        foreach (var (classToRun, dayNumber) in days.Skip(skip).Take(limit))
        {
            switch (Activator.CreateInstance(classToRun))
            {
                case Day<long> day:
                    day.DayNumber = dayNumber;
                    day.Run();
                    break;
                case Day<string> day:
                    day.DayNumber = dayNumber;
                    day.Run();
                    break;
            }
        }
        watch.Stop();
        
        Console.WriteLine();
        Console.WriteLine($"Execution took {watch.Elapsed}.",
            watch.ElapsedMilliseconds < 1000 ? ConsoleColor.Green : ConsoleColor.Red);
    }
}
