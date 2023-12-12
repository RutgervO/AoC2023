namespace AOC.days;

internal class Day12 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "12_t1.txt"), 21);
        AddRun("Part 1", () => RunPart(1, "12.txt"));
        AddRun("Test 2", () => RunPart(2, "12_t1.txt"), 525152);
        AddRun("Part 2", () => RunPart(2, "12.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var inputValues = GetListOfLines(inputName);
        var unfolding = part * 4 - 3;
        long result = 0;

        foreach (var parts in inputValues.Select(line => line.Split(' ').ToArray()))
        {
            var condition = string.Join('?', Enumerable.Repeat(parts[0], unfolding));
            var rules = string.Join(',', Enumerable.Repeat(parts[1], unfolding)).Split(',').Select(int.Parse).ToArray();
            var pattern = string.Join('.', condition.Split('.', StringSplitOptions.RemoveEmptyEntries));
            var cache = new Dictionary<string, long>();
            
            result += Solve(0, 0, 0);
            continue;

            long Solve(int pos, int count, int rule)
            {
                while (true)
                {
                    if (cache.TryGetValue(GetKey(), out var solve))
                    {
                        return solve;
                    }
                    
                    if (rule == rules.Length)
                    {
                        return count > 0 ?
                            // no rule for current grouping - impossible
                            SaveResult(0) :
                            // Can only fit in the rules if there are no '#'
                            SaveResult(pattern[pos..].Contains('#') ? 0 : 1);
                    }

                    if (count > rules[rule])
                    {
                        // No way to solve it
                        return SaveResult(0);
                    }

                    if (pos == pattern.Length)
                    {
                        // ran out of input
                        if ((count == 0 && rules.Length == rule)
                            || (rules.Length - rule == 1 && rules[rule] == count))
                        {
                            // either remaining rules is empty and we are not in a grouping,
                            // or current grouping matches the last remaining rule
                            return SaveResult(1);
                        }

                        return SaveResult(0);
                    }

                    if (pattern[pos] == '#')
                    {
                        // has to be added to current grouping
                        pos += 1;
                        count += 1;
                        continue;
                    }

                    if (pattern[pos] == '.')
                    {
                        if (count > 0)
                        {
                            // current grouping must end and be correct
                            if (rules.Length == rule || count != rules[rule])
                            {
                                // incorrect
                                return SaveResult(0);
                            }

                            rule += 1;
                        }

                        pos += 1;
                        count = 0;
                        continue;
                    }

                    // pattern[pos] == '?'
                    // solve all possibilities
                    long total = 0;
                    
                    if (rules.Length > rule && count < rules[rule])
                    {   // try '#' if there are still rules and we're not exceeding the current rule
                        total += Solve(pos + 1, count + 1, rule);
                    }

                    if (count == 0)
                    {   // '.' while not in grouping
                        total += Solve(pos + 1, count, rule);
                    }
                    else
                    {
                        if (rules.Length > rule && count == rules[rule])
                        {   // '.' ending grouping
                            total += Solve(pos + 1, 0, rule + 1);
                        }
                    }

                    return SaveResult(total);

                    string GetKey() => $"{pos},{count},{rule}";
                    long SaveResult(long value) => cache[GetKey()] = value;
                }
            }
        }
        return result;
    }
}