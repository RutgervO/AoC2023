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
            var turns = line.Split(new[] { ';', ':' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            foreach (var turn in turns)
            {
                var moves = turn.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var move in moves)
                {
                    var bla = move.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var count = int.Parse(bla[0]);
                    var color = bla[1];
                    maxValues[color] = int.Max(maxValues[color], count);
                    if (maxValues[color] > settings[color])
                    {
                        valid = false;
                    }
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
