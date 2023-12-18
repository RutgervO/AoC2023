using AOC.util;

namespace AOC.days;

internal class Day18 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "18_t1.txt"), 62);
        AddRun("Part 1", () => RunPart(1, "18.txt"));
        AddRun("Test 2", () => RunPart(2, "18_t1.txt"), 952408144115);
        AddRun("Part 2", () => RunPart(2, "18.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var instructions = GetListOfLines(inputName)
            .Select(x => x.Split(new[] { ' ', '(', '#', ')' }, StringSplitOptions.RemoveEmptyEntries));
        var position = new Coordinate(0, 0);
        var trench = new List<Coordinate> { position };
        var directionMap = new Dictionary<char, string>() { { '0', "R" }, { '1', "D" }, { '2', "L" }, { '3', "U" } };
        long edgeBlocks = 0;
        foreach (var instruction in instructions)
        {
            var steps = long.Parse(instruction[1]);
            var direction = new Direction(instruction[0]);
            if (part == 2)
            {
                steps = long.Parse(instruction[2][0..5], System.Globalization.NumberStyles.HexNumber);
                direction = new Direction(directionMap[instruction[2][5]]);
            }
            for (var i = 0; i < steps; i++)
            {
                position = position.Add(direction);
            }
            trench.Add(position);
            edgeBlocks += steps;
        }

        var area = Math.Abs(trench.Take(trench.Count - 1)
            .Select((p, i) => (long)(trench[i + 1].X - p.X) * (trench[i + 1].Y + p.Y))
            .Sum() / 2);
        
        var internalBlocks = area + 1 - edgeBlocks/2;

        return internalBlocks + edgeBlocks;
    }
}