namespace AOC.days;

internal class Day99 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "99_t1.txt"), 1001); // ToDo
        AddRun("Part 1", () => RunPart(1, "99.txt"));
        // AddRun("Test 2", () => RunPart(2, "99_t2.txt"), 1002); // ToDo
        // AddRun("Part 2", () => RunPart(2, "99.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLinesAsInt(inputName);
        if (part == 1)
        {
            return 1; // ToDo
        }

        return 2; // ToDo
    }
}
