using AOC.util;

namespace AOC.days;

internal class Day01 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "01_t1.txt"), 142);
        AddRun("Part 1", () => RunPart(1, "01.txt"));
        AddRun("Test 2", () => RunPart(2, "01_t2.txt"), 281);
        AddRun("Part 2", () => RunPart(2, "01.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);
        if (part == 2)
        {
            inputValues = inputValues.Select(GetDigits).ToList();
        }
        
        return inputValues
            .Select(x => (long)10 * (x.First(char.IsDigit) - '0') + x.Last(char.IsDigit) - '0')
            .Sum();
    }

    private readonly string[] _words = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    
    private string GetDigit(string line)
    {
        if (char.IsDigit(line[0])) return "" + line[0];
        return _words
            .SelectWhere((_, i) => $"{i + 1}", (x, _) => line.StartsWith(x))
            .FirstOrDefault("");
    }

    private string GetDigits(string line)
    {
        var result = "";
        for (var i = 0; i < line.Length; i++)
        {
            result += GetDigit(line[i..]);
        }
        return result;
    }
}
