using AOC.util;

namespace AOC.days;

internal class Day22 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "22_t1.txt"), 5);
        AddRun("Part 1", () => RunPart(1, "22.txt"));
        AddRun("Test 2", () => RunPart(2, "22_t1.txt"), 7);
        AddRun("Part 2", () => RunPart(2, "22.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        var blocks3d = new HashSet<Coordinate3D>[input.Count];
        foreach (var (v, b) in input.Select(x => x.Split(new[] { ',', '~' }).Select(int.Parse).ToArray()).WithIndex())
        {
            var block = new HashSet<Coordinate3D>();
            for (var x = v[0]; x <= v[3]; x++)
            {
                for (var y = v[1]; y <= v[4]; y++)
                {
                    for (var z = v[2]; z <= v[5]; z++)
                    {
                        block.Add(new Coordinate3D(x, y, z));
                    }
                }
            }
            blocks3d[b] = block;
        }

        blocks3d = blocks3d.OrderBy(x => x.Min(c => c.Z)).ToArray();
        var blocks2d = blocks3d.Select(x => x.Select(c => new Coordinate(c.X, c.Y)).ToHashSet()).ToArray();

        var movement = true;
        while (movement)
        {
            movement = false;
            foreach (var (block, b) in blocks3d.WithIndex())
            {
                var moveDown = block.Min(x => x.Z) - 1;
                if (moveDown <= 0) continue;
                
                var block2d = blocks2d[b];
                var relevant2dBlocks = blocks2d
                    .WithIndex()
                    .Where(x => x.index != b && x.item.Overlaps(block2d))
                    .Select(x => x.index);
                var relevantBlocks = relevant2dBlocks
                    .SelectMany(x => blocks3d[x])
                    .ToHashSet();
                
                foreach (var c2 in block2d)
                {
                    var min = block
                        .Where(x => x.X == c2.X && x.Y == c2.Y)
                        .Select(x => x.Z)
                        .Min();
                    var max = relevantBlocks
                        .Where(x => x.X == c2.X && x.Y == c2.Y && x.Z < min)
                        .Select(x => x.Z).MaxOrDefault(1);
                    moveDown = Math.Min(min - max - 1, moveDown);
                }

                if (moveDown <= 0) continue;
                
                blocks3d[b] = block
                    .Select(c => new Coordinate3D(c.X, c.Y, c.Z - moveDown))
                    .ToHashSet();
                movement = true;
            }
        }

        var isSupportedBy = new HashSet<Tuple<int, int>>(); // a leans on b
        foreach (var (block, b) in blocks3d.WithIndex())
        {
            var newBlock = block
                .Select(c => new Coordinate3D(c.X, c.Y, c.Z - 1))
                .ToHashSet();
            foreach (var (otherBlock, o) in blocks3d.WithIndex())
            {
                if (b == o) continue;
                if (!newBlock.Overlaps(otherBlock)) continue;
                isSupportedBy.Add(new Tuple<int, int>(b, o));
            }
        }

        long result = 0;
        
        if (part == 1) {
            for (var b = 0; b < blocks3d.Length; b++)
            {
                var dependents = isSupportedBy
                    .Where(x => x.Item2 == b)
                    .Select(x => x.Item1)
                    .ToHashSet();
                if (dependents.Count == 0)
                {
                    result += 1;
                    continue;
                }
                var dependentsThatAlsoLeanOnOtherBlocks =
                    isSupportedBy
                        .Where(x => dependents.Contains(x.Item1) && x.Item2 != b)
                        .Select(x => x.Item1)
                        .ToHashSet();
                if (dependents.SetEquals(dependentsThatAlsoLeanOnOtherBlocks))
                {
                    result += 1;
                }
            }
            return result;
        }

        for (var b = 0; b < blocks3d.Length; b++)
        {
            var fallen = new HashSet<int>();
            var falling = new HashSet<int>(){b};
            while (falling.Count > 0)
            {
                fallen.UnionWith(falling);
                falling = isSupportedBy
                    .Where(x => 
                        falling.Contains(x.Item2)
                        && !isSupportedBy.Any(y => y.Item1 == x.Item1 && !fallen.Contains(y.Item2)))
                    .Select(x => x.Item1)
                    .ToHashSet();
                result += falling.Count;
            }
        }

        return result;
    }
}