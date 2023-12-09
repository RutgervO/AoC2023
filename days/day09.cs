using System.Reflection;
using AOC.util;

namespace AOC.days;

internal class Day09 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "09_t1.txt"), 114); 
        AddRun("Part 1", () => RunPart(1, "09.txt"));
        AddRun("Test 2", () => RunPart(2, "09_t1.txt"), 2);
        AddRun("Part 2", () => RunPart(2, "09.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var lines = GetListOfLines(inputName);
        long result = 0;
        foreach (var line in lines)
        {
            var sequence = line.Split(' ').Select(long.Parse);
            if (part == 1)
            {
                result += GetNextNumber(sequence);
            }
            else
            {
                result += GetPreviousNumber(sequence);
            }
        }

        return result;
    }

    private long GetNextNumber(IEnumerable<long> sequence)
    {
        var diffs = sequence.SelectWithPrevious((prev, cur) => cur - prev).ToArray();
        if (diffs.ToHashSet().Count == 1)
        {
            return sequence.Last() + diffs.First();
        }

        return sequence.Last() + GetNextNumber(diffs);
    }
    
    private long GetPreviousNumber(IEnumerable<long> sequence)
    {
        var diffs = sequence.SelectWithPrevious((prev, cur) => cur - prev).ToArray();
        if (diffs.ToHashSet().Count == 1)
        {
            return sequence.First() - diffs.First();
        }

        return sequence.First() - GetPreviousNumber(diffs);
    }
}