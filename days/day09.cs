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

        return lines
            .Select(line => line.Split(' ').Select(long.Parse))
            .Select(sequence => GetNextNumber((part == 1) ? sequence : sequence.Reverse()))
            .Sum();
    }

    private long GetNextNumber(IEnumerable<long> sequence)
    {
        var diffs = sequence.SelectWithPrevious((prev, cur) => cur - prev).ToArray();
        if (diffs.All(x => x == diffs[0]))
        {
            return sequence.Last() + diffs[0];
        }

        return sequence.Last() + GetNextNumber(diffs);
    }
}