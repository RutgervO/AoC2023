using AOC.util;

namespace AOC.days;

internal class Day04 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "04_t1.txt"), 13);
        AddRun("Part 1", () => RunPart(1, "04.txt"));
        AddRun("Test 2", () => RunPart(2, "04_t1.txt"), 30);
        AddRun("Part 2", () => RunPart(2, "04.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var lines = GetListOfLines(inputName);
        long result = 0;
        var copies = new DefaultDictionary<int, int>();
        int card;
        for (card = 0; card < lines.Count; card++)
        {
            var line = lines[card];
            var halves = line.Split(':', '|').ToArray();
            var haves = halves.Skip(1).First().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
            var winning = halves.Skip(2).Single().Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
            haves.IntersectWith(winning);
            if (part == 1)
            {
                if (haves.Any())
                {
                    result += (long)Math.Pow(2, haves.Count - 1);
                }
            }
            else
            {
                copies[card] += 1;
                result += copies[card];
                if (haves.Any())
                {
                    var dupes = card + 1;
                    foreach (var _ in haves)
                    {
                        copies[dupes++] += copies[card];
                    }
                }
            }
        }

        return result;
    }
}