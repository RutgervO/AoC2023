using System.Numerics;
using AOC.util;

namespace AOC.days;

internal class Day08 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "08_t1.txt"), 2);
        AddRun("Test 2", () => RunPart(1, "08_t2.txt"), 6);
        AddRun("Part 1", () => RunPart(1, "08.txt"));
        AddRun("Test 3", () => RunPart(2, "08_t3.txt"), 6);
        AddRun("Part 2", () => RunPart(2, "08.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);

        var instructions = inputValues[0].Select(x => (x - 76) / 6).RepeatIndefinitely();
        var network = inputValues
            .Skip(2)
            .ToDictionary(x => x[0..3], x => new string[] { x[7..10], x[12..15] });

        if (part == 1)
        {
            var node = "AAA";
            long result = 0;

            foreach (var instruction in instructions)
            {
                node = network[node][instruction];
                result += 1;
                if (node == "ZZZ")
                {
                    return result;
                }
            }
        }
        else
        {
            var nodes = network.Keys.Where(x => x[2] == 'A').ToArray();
            var firstZ = nodes.Select(x => (long)0).ToArray();
            var runners = nodes.Length;
            long result = 0;
            foreach (var instruction in instructions)
            {
                result += 1;
                for (int i = 0; i < runners; i++)
                {
                    nodes[i] = network[nodes[i]][instruction];
                    if (nodes[i][2] == 'Z')
                    {
                        if (firstZ[i] == 0)
                        {
                            firstZ[i] = result;
                        }
                    }
                }
                if (nodes.Count(x => x[2] == 'Z') == runners)
                {
                    return result;
                }

                if (firstZ.All(x => x > 0))
                {
                    return firstZ.Aggregate((long)1, MathHelpers.LeastCommonMultiple);
                }
            }
        }

        return 0;
    }
    
}
public static class MathHelpers
{
    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        => a / GreatestCommonDivisor(a, b) * b;
    
    public static T LeastCommonMultiple<T>(this IEnumerable<T> values) where T : INumber<T>
        => values.Aggregate(LeastCommonMultiple);
}