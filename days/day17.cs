using AOC.util;

namespace AOC.days;

internal class Day17 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "17_t1.txt"), 102);
        AddRun("Part 1", () => RunPart(1, "17.txt"));
        AddRun("Test 2", () => RunPart(2, "17_t1.txt"), 94);
        AddRun("Part 2", () => RunPart(2, "17.txt"));
    }

    private record Pass(Coordinate Location, Direction Direction, int Count);
    
    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var board = new Board2D<int>(input, int.Parse, false);
        var minCost = new DefaultValueDictionary<Pass, long>(long.MaxValue);
        var finish = new Coordinate(board.Width - 1, board.Height - 1);
        var allDirections = Direction.AllDirections().ToArray();
        var startPass = new Pass(new Coordinate(0, 0), new Direction(0, 0), 0);
        var finishCost = long.MaxValue;
        var maxCount = (part == 1) ? 3 : 10;
        var minCount = (part == 1) ? 0 : 4;
        
        Solve(startPass, 0);
        
        return finishCost;
        
        void Solve(Pass pass, long cost)
        {
            var inverseDirection = pass.Direction.Inverse();

            foreach (var direction in allDirections)
            {
                if ((pass.Count > 0 && pass.Count < minCount && direction != pass.Direction)
                    || (pass.Count >= maxCount && direction == pass.Direction)
                    || (direction == inverseDirection))
                {
                    continue;
                }
                
                var location = pass.Location.Add(direction);
                if (!board.IsOnBoard(location)) continue;
                
                var newCost = cost + board.Board[location];
                var count = (direction == pass.Direction) ? pass.Count + 1 : 1;
                
                var newPass = new Pass(location, direction, count);
                if (newCost >= minCost[newPass] || newCost >= finishCost) continue;
                
                minCost[newPass] = newCost;
                if (location == finish)
                {
                    finishCost = Math.Min(finishCost, newCost);
                    continue;
                }
                Solve(newPass, newCost);
            }
        }
    }
}