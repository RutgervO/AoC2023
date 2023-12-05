namespace AOC.days;

internal class Day05 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "05_t1.txt"), 35);
        AddRun("Part 1", () => RunPart(1, "05.txt"));
        AddRun("Test 2", () => RunPart(2, "05_t1.txt"), 46);
        AddRun("Part 2", () => RunPart(2, "05.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var lines = GetListOfLines(inputName);

        if (part == 1)
        {
            var source = lines.First().Split(':').Skip(1).Single()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)
                .ToList();

            var target = new List<long>();

            foreach (var line in lines.Skip(1))
            {
                if (line.Length == 0 || line.Contains(':'))
                {
                    source.AddRange(target);
                    target.Clear();
                    continue;
                }

                var rule = line.Split(' ').Select(long.Parse).ToArray();
                var ruleTarget = rule[0];
                var ruleSource = rule[1];
                var ruleSize = rule[2];

                foreach (var seed in source.ToArray())
                {
                    var offset = seed - ruleSource;
                    if (offset >= 0 && offset < ruleSize)
                    {
                        target.Add(ruleTarget + offset);
                        source.Remove(seed);
                    }
                }
            }

            source.AddRange(target);
            target.Clear();
            return source.Min();
        }
        
        var startValues = lines.First().Split(':').Skip(1).Single()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)
            .ToList();
        
        var sourceT = startValues.Chunk(2).Select(x => Tuple.Create(x[0], x[1])).ToList();
        var targetT = new List<Tuple<long, long>>();
        
        foreach (var line in lines.Skip(1))
        {
            if (line.Length == 0 || line.Contains(':'))
            {
                sourceT.AddRange(targetT);
                targetT.Clear();
                continue;
            }

            var rule = line.Split(' ').Select(long.Parse).ToArray();
            var ruleTarget = rule[0];
            var ruleStart = rule[1];
            var ruleSize = rule[2];
            var ruleEnd = ruleStart + ruleSize - 1;
            
            foreach (var seed in sourceT.ToArray())
            {
                var seedStart = seed.Item1;
                var seedSize = seed.Item2;
                var seedEnd = seedStart + seedSize - 1;
                
                if ((seedStart <= ruleStart && seedEnd >= ruleStart)
                    || (seedStart <= ruleEnd && seedEnd >= ruleEnd)
                    || (seedStart >= ruleStart && seedEnd <= ruleEnd))
                {
                    sourceT.Remove(seed);
                    if (seedStart < ruleStart)
                    {
                        var size = Math.Min(ruleStart - seedStart, seedSize);
                        if (size > 0)
                        {
                            sourceT.Add(Tuple.Create(seedStart, size));
                            seedStart = ruleStart;
                            seedSize -= size;
                        }
                    }

                    if (seedEnd > ruleEnd)
                    {
                        var size = Math.Min(seedEnd - ruleEnd, seedSize);
                        if (size > 0) {
                            sourceT.Add(Tuple.Create(ruleEnd + 1, size));
                            // seedEnd = ruleEnd;
                            seedSize -= size;
                        }
                    }

                    var offset = seedStart - ruleStart;
                    targetT.Add(Tuple.Create(ruleTarget + offset, seedSize));
                }
            }
        }

        sourceT.AddRange(targetT);
        targetT.Clear();
        return sourceT.Select(x => x.Item1).Min();
    }
}