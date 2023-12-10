using AOC.util;

namespace AOC.days;

internal class Day10 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "10_t1.txt"), 4);
        AddRun("Test 2", () => RunPart(1, "10_t2.txt"), 8);
        AddRun("Part 1", () => RunPart(1, "10.txt"));
        AddRun("Test 3", () => RunPart(2, "10_t3.txt"), 4);
        AddRun("Test 4", () => RunPart(2, "10_t4.txt"), 8);
        AddRun("Test 5", () => RunPart(2, "10_t5.txt"), 10);
        AddRun("Part 2", () => RunPart(2, "10.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);
        var board = new Board2D<char>(inputValues, x => x[0], false);
        var notDirt = board.Board.Where(x => x.Value != '.').ToDictionary(x => x.Key, x => x.Value);

        var start = notDirt
            .Where(x => x.Value == 'S')
            .Select(x => x.Key)
            .Single();
        
        var validDeltaChars = new Dictionary<Coordinate, string>()
        {
            { new(1, 0), "-J7S" },
            { new(-1, 0), "-FLS" },
            { new(0, 1), "|LJS" },
            { new(0, -1), "|F7S" },
        };
        var directions = new Dictionary<char, Coordinate[]>()
        {
            { '-', new[] { new Coordinate(-1,  0), new Coordinate( 1,  0) } },
            { '|', new[] { new Coordinate( 0, -1), new Coordinate( 0,  1) } },
            { '7', new[] { new Coordinate(-1,  0), new Coordinate( 0,  1) } },
            { 'F', new[] { new Coordinate( 1,  0), new Coordinate( 0,  1) } },
            { 'L', new[] { new Coordinate( 1,  0), new Coordinate( 0, -1) } },
            { 'J', new[] { new Coordinate(-1,  0), new Coordinate( 0, -1) } },
            { 'S', new[] { new Coordinate(-1,  0), new Coordinate( 0, -1), 
                           new Coordinate( 1,  0), new Coordinate( 0,  1) } },
        };

        var pathNodes = new HashSet<Coordinate>() { start };
        long result = 0;
        var node = new Coordinate(start);
        var potentialDirections = validDeltaChars.Select(x => x.Key).ToArray();
        while (true)
        {
            foreach (var delta in validDeltaChars.Where(x => potentialDirections.Contains(x.Key)))
            {
                var newNode = node.Add(delta.Key);
                var piece = board.Board[newNode];
                if (delta.Value.Contains(piece))
                {
                    result += 1;
                    potentialDirections = directions[piece].Where(x => !x.IsInverse(delta.Key)).ToArray();
                    node = new Coordinate(newNode);
                    if (node.Equals(start))
                    {
                        if (part == 1)
                        {
                            return result / 2;
                        }
                        
                        result = 0;
                        for (var y = 0; y < board.Height; y++)
                        {
                            var inside = 0;
                            var onLine = false;
                            var lineStartChar = '.';
                                
                            for (var x = 0; x < board.Width; x++)
                            {
                                var pos = new Coordinate(x, y);
                                var item = board.Board[pos];
                                var isOnPath = pathNodes.Contains(pos);
                                if (!isOnPath)
                                {   // not on path, so count result!
                                    result += inside;
                                }
                                else if (item == '|')
                                {   // passing a vertical - flip
                                    inside = 1 - inside;
                                }
                                else if (!onLine)
                                {   // not already in a line - start one
                                    lineStartChar = item;
                                    onLine = true;
                                }
                                else
                                {   // on a line, see if it ends
                                    if ("7J".Contains(item))
                                    {
                                        onLine = false;
                                        if ((lineStartChar == 'F' && item == 'J') || (lineStartChar == 'L' && item == '7'))
                                        {   // if directions are opposite then we passed a vertical - flip
                                            inside = 1 - inside;
                                        }
                                    }
                                }
                            }
                        }
                        return result;
                    }
                    pathNodes.Add(node);
                } 
            }
        }
    }
}