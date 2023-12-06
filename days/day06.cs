namespace AOC.days;

internal class Day06 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "06_t1.txt"), 288);
        AddRun("Part 1", () => RunPart(1, "06.txt"));
        AddRun("Test 2", () => RunPart(2, "06_t1.txt"), 71503);
        AddRun("Part 2", () => RunPart(2, "06.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var lines = GetListOfLines(inputName);
        if (part == 2)
        {
            lines = lines.Select(x => x.Replace(" ", "")).ToList();
        }
        var races = lines.Select(x => x.Split(new []{' ', ':'}, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(long.Parse)
                .ToArray())
            .ToArray();
        
        long result = 1;

        foreach (var (time, goal) in races[0].Zip(races[1]))
        {
            long count = 0;
            for (var buttonTime = 0; buttonTime < time; buttonTime++)
            {
                if (buttonTime * (time - buttonTime) > goal)
                {
                    count += 1;
                }
            }
            result *= count;
        }
        
        return result;
    }
}