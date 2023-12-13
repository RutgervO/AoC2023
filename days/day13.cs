namespace AOC.days;

using static util.StringUtils;

internal class Day13 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "13_t1.txt"), 405);
        AddRun("Part 1", () => RunPart(1, "13.txt"));
        AddRun("Test 2", () => RunPart(2, "13_t1.txt"), 400);
        AddRun("Part 2", () => RunPart(2, "13.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var puzzles = GetListOfLines(inputName)
            .Aggregate(new List<List<string>> { new() },
                (list, value) =>
                {
                    if (value == "")
                    {
                        list.Add(new List<string>());
                    }
                    else
                    {
                        list.Last().Add(value);
                    }

                    return list;
                });

        long result = 0;
        foreach (var puzzle in puzzles)
        {
            // find vertical
            var lines = puzzle.ToArray();
            result += Solve(lines, 100, part - 1);

            // find horizontal
            lines = TransposeStrings(lines);
            result += Solve(lines, 1, part - 1);
        }

        return result;
    }

    private static long Solve(string[] lines, long multiplier, int targetDistance)
    {
        for (var y = 0; y < lines.Length - 1; y++)
        {
            var distance = 0;
            for (var dy = 0; y - dy >= 0 && y + 1 + dy < lines.Length; dy++)
            {
                distance += StringDistance(lines[y - dy], lines[y + 1 + dy]);
                if (distance <= targetDistance) continue;
                break;
            }

            if (distance == targetDistance)
            {
                return multiplier * (y + 1);
            }
        }

        return 0;
    }
}