using AOC.util;

namespace AOC.days;

internal class Day16 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "16_t1.txt"), 46);
        AddRun("Part 1", () => RunPart(1, "16.txt"));
        AddRun("Test 2", () => RunPart(2, "16_t1.txt"), 51);
        AddRun("Part 2", () => RunPart(2, "16.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var board = new Board2D<char>(input, x => x[0], false);
        var starts = new List<Tuple<Coordinate, Direction>>();
        
        if (part == 1)
        {
            starts.Add(new Tuple<Coordinate, Direction>(new Coordinate(-1, 0), new Direction("E")));
        }

        if (part == 2)
        {
            for (var i = 0; i < Math.Max(board.Width, board.Height); i ++)
            {
                if (i < board.Width)
                {
                    starts.Add(new Tuple<Coordinate, Direction>(new Coordinate(-1, i), new Direction("E")));
                    starts.Add(new Tuple<Coordinate, Direction>(new Coordinate(board.Width, i), new Direction("W")));
                }
                if (i < board.Height)
                {
                    starts.Add(new Tuple<Coordinate, Direction>(new Coordinate(i, -1), new Direction("S")));
                    starts.Add(new Tuple<Coordinate, Direction>(new Coordinate(i, 1), new Direction("N")));
                }
            }
        }

        return starts.Select(Solve).Max();

        long Solve(Tuple<Coordinate, Direction> startBeam)
        {
            var energized = new HashSet<Coordinate>();
            var directions = new HashSet<Tuple<Coordinate, Direction>>();
            var beams = new List<Tuple<Coordinate, Direction>> { startBeam };

            while (true)
            {
                if (beams.Count == 0)
                {
                    return energized.Count;
                }
                
                var beamsToProcess = beams.ToList();
                beams.Clear();
                foreach (var (coordinate, direction) in beamsToProcess)
                {
                    var location = coordinate.Add(direction);
                    if (!board.IsOnBoard(location)) continue;

                    energized.Add(location);
                    
                    var tile = board.Board[location];
                    if (tile == '.' || (tile == '-' && direction.Y == 0) || (tile == '|' && direction.X == 0))
                    {
                        // direction does not change
                        AddDirection(direction);
                    }
                    else
                    {
                        // direction changes
                        switch (tile)
                        {
                            case '/':
                                AddDirection(new Direction(-direction.Y, -direction.X));
                                break;
                            case '\\':
                                AddDirection(new Direction(direction.Y, direction.X));
                                break;
                            case '|':
                                AddDirection(new Direction("N"));
                                AddDirection(new Direction("S"));
                                break;
                            case '-':
                                AddDirection(new Direction("W"));
                                AddDirection(new Direction("E"));
                                break;
                        }
                    }

                    void AddDirection(Direction d)
                    {
                        var newBeam = new Tuple<Coordinate, Direction>(location, d);
                        if (directions.Add(newBeam))
                        {
                            beams.Add(newBeam);
                        }
                    }
                }
            }
        }
    }
}