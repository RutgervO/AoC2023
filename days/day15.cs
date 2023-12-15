using System.Collections;
using System.Collections.Specialized;

namespace AOC.days;

internal class Day15 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "15_t1.txt"), 1320);
        AddRun("Part 1", () => RunPart(1, "15.txt"));
        AddRun("Test 2", () => RunPart(2, "15_t1.txt"), 145);
        AddRun("Part 2", () => RunPart(2, "15.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var rules = GetListOfLines(inputName).Single().Split(',');
        if (part == 1)
        {
            return rules.Aggregate<string?, long>(0, (current, step) => current + GetHash(step!));
        }

        var boxes = Enumerable.Range(0, 256).Select(x => new OrderedDictionary()).ToArray();
        foreach (var rule in rules)
        {
            var parts = rule.Split(new char[]{'=', '-'});
            var label = parts[0];
            var op = rule[label.Length];
            var focal = rule[(label.Length + 1)..];

            var boxNo = GetHash(label);
            var box = boxes[boxNo];
            if (op == '-')
            {
                box.Remove(label);
            }
            else
            {
                var value = int.Parse(focal);
                if (box.Contains(label))
                {
                    box[label] = value;
                }
                else
                {
                    box.Add(label, value);
                }
            }
        }

        long result = 0;
        long boxCount = 0;
        foreach (var box in boxes)
        {
            boxCount++;
            long slotCount = 0;
            foreach (DictionaryEntry slot in box)
            {
                slotCount++;
                result += boxCount * slotCount * (int)slot.Value!;
            }

        }

        return result;



    }

    private static int GetHash(string label)
    {
        var value = 0;
        foreach (var c in label)
        {
            value = ((value + c) * 17) % 256;
        }
        return value;
    }
}