using AOC.util;

namespace AOC.days;

internal class Day03 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "03_t1.txt"), 4361);
        AddRun("Part 1", () => RunPart(1, "03.txt"));
        AddRun("Test 2", () => RunPart(2, "03_t1.txt"), 467835);
        AddRun("Part 2", () => RunPart(2, "03.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);

        var board = new Board2D<char>(inputValues, x=> x[0]);
        var symbols = part == 1
            ? board.CoordinatesThatMatch(x => !char.IsDigit(x) && x != '.').ToList()
            : board.CoordinatesThatMatch(x => x == '*').ToList();
        var gearNumbers = new Dictionary<Coordinate, long>();
        long result = 0;
        
        foreach (var y in Enumerable.Range(0, board.Height))
        {
            var c = new Coordinate(0, y);
            while (c.X < board.Width)
            {
                if (char.IsDigit(board.Get(c)))
                {
                    var number = 0;
                    var startX = c.X;
                    var endX = c.X;
                    while (board.IsOnBoard(c) && char.IsDigit(board.Get(c)))
                    {
                        number = 10 * number + board.Get(c) - '0';
                        c = new Coordinate(++endX, y);
                    }

                    var hits = symbols.Where(x =>
                        x.X >= startX - 1 &&
                        x.X <= endX &&
                        x.Y >= y - 1 &&
                        x.Y <= y + 1).ToArray();
                    if (hits.Any())
                    {
                        if (part == 1)
                        {
                            result += number;
                        }
                        else
                        {
                            foreach (var hit in hits)
                            {
                                if (gearNumbers.TryGetValue(hit, out var gearNumber))
                                {
                                    result += gearNumber * number;
                                }
                                else
                                {
                                    gearNumbers[hit] = number;
                                }
                            }
                        }
                    }
                }
                c = new Coordinate(c.X + 1, y);
            }
        }
        return result;
    }
}