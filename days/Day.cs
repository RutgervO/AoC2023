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
            var result = action();
            Out($"{result} ");
            if (title.Contains("est") || !Equals(testResult, default(TResult))) {
                if (Equals(result, testResult)) {
                    Out("✓");
                } else {
                    Out($"❌ Expected: {testResult}\n");
                    return;
                }
            }
            Out("\n");
        }
    }

    private static void Out(string output)
    {
        Console.Write(output);
    }

    protected static List<string> GetListOfLines(string fileName)
    {
        var inputLines = File.ReadLines($@"input/{fileName}").ToList();
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