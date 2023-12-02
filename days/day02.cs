using AOC.util;

namespace AOC.days;

internal class Day02 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "02_t1.txt"), 8);
        AddRun("Part 1", () => RunPart(1, "02.txt"));
        AddRun("Test 2", () => RunPart(2, "02_t1.txt"), 2286);
        AddRun("Part 2", () => RunPart(2, "02.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);
        var settings = new Dictionary<string, int>() { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
        var game = 1;
        long result = 0;
        foreach (var line in inputValues)
        {
            var valid = true;
            var maxValues = new DefaultDictionary<string, int>();
            var moves = line.Split(new[] { ':' }).Skip(1).Single().Split(new[] {',', ';' });
            foreach (var move in moves)
            {
                var parts = move.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var count = int.Parse(parts[0]);
                var color = parts[1];
                maxValues[color] = int.Max(maxValues[color], count);
                if (maxValues[color] > settings[color])
                {
                    valid = false;
                }
            }

            if (part == 1)
            {
                result += valid ? game : 0;
            }
            else
            {
                result += maxValues["red"] * maxValues["green"] * maxValues["blue"];
            }
            game++;
        }
        return result;
    }
}
