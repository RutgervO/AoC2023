using AOC.util;

namespace AOC.days;

internal class Day12 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "12_t1.txt"), 21);
        AddRun("Part 1", () => RunPart(1, "12.txt"));
        AddRun("Test 2", () => RunPart(2, "12_t1.txt"), 525152);
        AddRun("Part 2", () => RunPart(2, "12.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);
        var unfolding = part * 4 - 3;
        var mapping = new[] { '#', '.' };
        long result = 0;

        foreach (var parts in inputValues.Select(line => line.Split(' ').ToArray()))
        {
            var condition = string.Join('?', Enumerable.Repeat(parts[0], unfolding));
            var rules = string.Join(',', Enumerable.Repeat(parts[1], unfolding)).Split(',').Select(int.Parse).ToArray();

            var groupings = condition.Split('.', StringSplitOptions.RemoveEmptyEntries);
            var groupingsString = string.Join('.', groupings);
            var unknowns = groupingsString.Count(x => x == '?');
            var options = Math.Pow(2, unknowns);
            var positions = groupingsString.SelectWhere((_, i) => i, (c, _) => c == '?').ToArray();
            for (long option = 0; option < options; option++)
            {
                var test = groupingsString.ToCharArray();
                long bit = 1;
                for (var pos = 0; pos < unknowns; pos++)
                {
                    test[positions[pos]] = mapping[(option & bit) / bit];
                    bit <<= 1;
                }

                if (new string(test)
                    .Split('.', StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => x[0] == '#')
                    .Select(x => x.Length)
                    .ToArray()
                    .SequenceEqual(rules))
                {
                    result += 1;
                }
            }
        }

        return result;

    }
}
