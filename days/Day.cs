using System.Diagnostics;

namespace AOC.days;

internal abstract class Day<TResult> where TResult:IComparable?
{
    public int DayNumber { get; set; }
    private List<(string Title, Func<TResult> Action, TResult? TestResult)> Sequence { get; }

    public abstract TResult RunPart(int part, string inputName);

    protected abstract void SetSequence();

    protected Day()
    {
        Sequence = new List<(string Title, Func<TResult> Action, TResult? TestResult)>();
        Initialize();
    }

    private void Initialize()
    {
        SetSequence();
    }

    protected void AddRun(string title, Func<TResult> action, TResult? testResult=default)
    {
        Sequence.Add((title, action, testResult));
    }

    public void Run()
    {
        foreach (var (title, action, testResult) in Sequence)
        {
            Out($"Day {DayNumber} {title}: ");
            var watch = Stopwatch.StartNew();
            var result = action();
            watch.Stop();
            if (title.Contains("est") || !Equals(testResult, default(TResult))) {
                if (Equals(result, testResult))
                {
                    Out($"{result} ✓", ConsoleColor.Green);
                } else {
                    Out($"{result} ❌ Expected: {testResult}\n", ConsoleColor.Red);
                    return;
                }
            }
            else
            {
                Out($"{result} ");
            }

            if (watch.ElapsedMilliseconds < 1000)
            {
                Out("  \u23f1 <1s", ConsoleColor.Green);
            }
            else
            {
                Out($"  \u23f1 {watch.Elapsed}", ConsoleColor.Red);
            }
            Out("\n");
        }
    }

    private static void Out(string output, ConsoleColor? color = null)
    {
        if (color != null)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write(output);
            Console.ResetColor();
            return;
        }
        Console.Write(output);
    }

    protected static List<string> GetListOfLines(string fileName)
    {
        var inputLines = File.ReadLines($"input/{fileName}").ToList();
        return inputLines;
    }

    protected static List<int> GetListOfIntegers(string fileName)
    {
        var inputLines = GetListOfLines(fileName);
        return inputLines[0].Split(',').ToList().ConvertAll(int.Parse);
    }
    
    protected static List<int> GetListOfLinesAsInt(string fileName)
    {
        var inputLines = GetListOfLines(fileName);
        return inputLines.ConvertAll(int.Parse);
    }
}