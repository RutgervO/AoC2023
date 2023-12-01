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
            var newValues = new List<string>();
            foreach (var line in inputValues)
            {
                var newLine = GetDigits(line);
                newValues.Add(newLine);
            }
            inputValues = newValues;
        }
        
        long result = 0;
        foreach (var line in inputValues)
        {
            result += 10 * (line.First(char.IsDigit) - '0');
            result += line.Last(char.IsDigit) - '0';
        }
        return result;
    }

    private string GetDigit(string line)
    {
        if (char.IsDigit(line[0])) return "" + line[0];
        var words = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        var digit = '1';
        foreach (var word in words)
        {
            if (line.StartsWith(word))
            {
                return "" + digit;
            }
            digit++;
        }
        return "";
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
