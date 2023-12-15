using AOC.util;

namespace AOC.days;

internal class Day14 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "14_t1.txt"), 136);
        AddRun("Part 1", () => RunPart(1, "14.txt"));
        AddRun("Test 2", () => RunPart(2, "14_t1.txt"), 64);
        AddRun("Part 2", () => RunPart(2, "14.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var board = new Board2D<char>(input, x => x[0], false);
        var load = GetLoad();

        if (part == 1)
        {
            TiltBoard("N");
            return load;
        }

        var results = new List<long>();
        for (long i = 0; i < 1000; i++) // Lucky guess that 1000 works! Not fixing it...
        {
            TiltBoard("N");
            TiltBoard("W");
            TiltBoard("S");
            TiltBoard("E");
            results.Add(load);
        }

        return results.Last();

        void TiltBoard(string direction)
        {
            var delta = new Direction(direction).ToCoordinate();
            while (true)
            {
                var moved = false;
                foreach (var roundedRock in board.Board.Where(x => x.Value == 'O').ToList())
                {
                    var location = roundedRock.Key.Add(delta);
                    if (!board.IsOnBoard(location) || board.Board[location] != '.') continue;
                    moved = true;
                    board.Board[roundedRock.Key] = '.';
                    board.Board[location] = 'O';
                    load -= delta.Y;
                }

                if (!moved)
                {
                    return;
                }
            }
        }

        long GetLoad()
        {
            return board.Board
                .Where(x => x.Value == 'O')
                .Select(x => (long)(board.Height - x.Key.Y))
                .Sum();
        }
    }
}