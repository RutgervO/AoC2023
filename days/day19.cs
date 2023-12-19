using AOC.util;

namespace AOC.days;

internal class Day19 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "19_t1.txt"), 19114);
        AddRun("Part 1", () => RunPart(1, "19.txt"));
        AddRun("Test 2", () => RunPart(2, "19_t1.txt"), 167409079868000);
        AddRun("Part 2", () => RunPart(2, "19.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName).Split(x => x.Length == 0).ToArray();
        
        var workflows = new Dictionary<string, List<Tuple<long[], string, long[]>>>();

        foreach (var line in input[0])
        {
            var lineParts = line.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var workflowName = lineParts[0];
            var rules = new List<Tuple<long[], string, long[]>>();
            foreach (var s in lineParts[1].Split(','))
            {
                var rule = new long[] { 0, 4001, 0, 4001, 0, 4001, 0, 4001 };
                var negativeRule = new long[] { 0, 4001, 0, 4001, 0, 4001, 0, 4001 };
                var t = s.Split(':').ToArray();
                var destination = t.Last();
                if (t.Length > 1)
                {
                    var pos = "xmas".IndexOf(t[0][0]) * 2;
                    var negOffset = 1;
                    
                    if (t[0][1] == '<')
                    {
                        pos += 1;
                        negOffset = -1;
                    }
                    rule[pos] = long.Parse(t[0][2..]);
                    negativeRule[pos + negOffset] = rule[pos] + negOffset;
                }

                rules.Add(new Tuple<long[], string, long[]>(rule, destination, negativeRule));
            }
            workflows[workflowName] = rules;
        }

        long result = 0;
        
        if (part == 1)
        {
            var items = input[1]
                .Select(line => line
                    .Split(new[] { '{', ',', '=', 'x', 'm', 'a', 's', '}' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray())
                .ToList();

            foreach (var item in items)
            {
                var flow = "in";
                while (flow != "A" && flow != "R")
                {
                    foreach (var rule in workflows[flow])
                    {
                        if (item[0] <= rule.Item1[0] || item[0] >= rule.Item1[1] ||
                            item[1] <= rule.Item1[2] || item[1] >= rule.Item1[3] ||
                            item[2] <= rule.Item1[4] || item[2] >= rule.Item1[5] ||
                            item[3] <= rule.Item1[6] || item[3] >= rule.Item1[7]) continue;
                        flow = rule.Item2;
                        if (flow == "A")
                        {
                            result += item.Sum();
                        }

                        break;
                    }
                }
            }
            return result;
        }

        return SolvePart2("in", new long[] { 1, 4000, 1, 4000, 1, 4000, 1, 4000 });

        long SolvePart2(string name, long[] validRange)
        {
            long result = 0;
            switch (name)
            {
                case "R":
                    return 0;
                case "A":
                    return (validRange[1] - validRange[0] + 1) *
                           (validRange[3] - validRange[2] + 1) *
                           (validRange[5] - validRange[4] + 1) *
                           (validRange[7] - validRange[6] + 1);
            }

            foreach (var rule in workflows[name])
            {
                var newRange = new[]
                {
                    Math.Max(validRange[0], rule.Item1[0] + 1), Math.Min(validRange[1], rule.Item1[1] - 1),
                    Math.Max(validRange[2], rule.Item1[2] + 1), Math.Min(validRange[3], rule.Item1[3] - 1),
                    Math.Max(validRange[4], rule.Item1[4] + 1), Math.Min(validRange[5], rule.Item1[5] - 1),
                    Math.Max(validRange[6], rule.Item1[6] + 1), Math.Min(validRange[7], rule.Item1[7] - 1)
                };
                result += SolvePart2(rule.Item2, newRange);
                validRange = new[]
                {
                    Math.Max(validRange[0], rule.Item3[0] + 1), Math.Min(validRange[1], rule.Item3[1] - 1),
                    Math.Max(validRange[2], rule.Item3[2] + 1), Math.Min(validRange[3], rule.Item3[3] - 1),
                    Math.Max(validRange[4], rule.Item3[4] + 1), Math.Min(validRange[5], rule.Item3[5] - 1),
                    Math.Max(validRange[6], rule.Item3[6] + 1), Math.Min(validRange[7], rule.Item3[7] - 1)
                };
            }
            return result;
        }
    }
}