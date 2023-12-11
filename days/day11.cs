using AOC.util;

namespace AOC.days;

internal class Day11 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "11_t1.txt"), 374);
        AddRun("Part 1", () => RunPart(1, "11.txt"));
        AddRun("Test 2", () => RunPart(2, "11_t1.txt"), 1030);
        AddRun("Test 3", () => RunPart(3, "11_t1.txt"), 8410);
        AddRun("Part 2", () => RunPart(4, "11.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var expansion = new[]{ 0, 1, 9, 99, 999999 }[part];
        var lines = GetListOfLines(inputName);
        var board = new Board2D<int>(lines, x => x == "#" ? 1 : 0, false);
        var coordinates = board.Board.Where(x => x.Value == 1).Select(x => x.Key).ToHashSet();
        var newCoordinates = new HashSet<Coordinate>();

        var offset = 0;
        for (var y = 0; y < board.Height; y++)
        {
            newCoordinates.UnionWith(coordinates.Where(c => c.Y == y).Select(c => new Coordinate(c.X, c.Y + offset)));
            if (coordinates.All(x => x.Y != y))
            {
                offset += expansion;
            }
        }

        coordinates.Clear();
        offset = 0;
        for (var x = 0; x < board.Width; x++)
        {
            coordinates.UnionWith(newCoordinates.Where(c => c.X == x).Select(c => new Coordinate(c.X + offset, c.Y)));
            if (newCoordinates.All(c => c.X != x))
            {
                offset += expansion;
            }
        }

        return coordinates
            .SelectMany(a => coordinates.Select(b => (a, b)))
            .Where(x => x.a < x.b)
            .Aggregate<(Coordinate a, Coordinate b), long>(0, (result, c)
                => result + (Math.Abs(c.a.X - c.b.X) + Math.Abs(c.a.Y - c.b.Y)));
    }
}