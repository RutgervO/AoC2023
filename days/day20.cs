using AOC.util;

namespace AOC.days;

internal class Day20 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "20_t1.txt"), 32000000);
        AddRun("Test 2", () => RunPart(1, "20_t2.txt"), 11687500);
        AddRun("Part 1", () => RunPart(1, "20.txt"));
        AddRun("Part 2", () => RunPart(2, "20.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName).Select(x => x.Split(new [] {' ', '-', '>', ','}, StringSplitOptions.RemoveEmptyEntries).ToArray());
        var modules = new Dictionary<string, Module>();
        // Types:
        // 0 == broadcaster
        // 1 == % flip flop
        // 2 == & conjunction
        foreach (var i in input)
        {
            var type = 0;
            var name = i[0];
            switch (i[0][0])
            {
                case 'b': // broadcaster
                    type = 0;
                    break;
                case '%': // flip flop
                    type = 1;
                    name = i[0][1..];
                    break;
                case '&': // conjunction
                    type = 2;
                    name = i[0][1..];
                    break;
            }
            modules[name] = new Module(name, type, i[1..]);
        }

        var endPoints = modules.SelectMany(x => x.Value.Outputs).ToHashSet().Where(x => !modules.ContainsKey(x))
            .ToHashSet();
        
        // fill sourceMemory where relevant
        foreach (var module in modules.Where(x => x.Value.Type == 2).Select(x => x.Key))
        {
            modules[module].SourceMemory = modules
                .Where(x => x.Value.Outputs.Contains(module))
                .Select(x => x.Key)
                .ToDictionary(x => x, _ => false);
        }

        if (part == 1)
        {
            var low = 0;
            var high = 0;
            for (long i = 0; i < 1000; i++)
            {
                var signals = new Queue<Signal>();
                signals.Enqueue(new Signal("broadcaster", false, "button"));
                while (signals.Count > 0)
                {
                    var signal = signals.Dequeue();
                    low += signal.IsHigh ? 0 : 1;
                    high += signal.IsHigh ? 1 : 0;
                    if (endPoints.Contains(signal.TargetName)) continue;
                    modules[signal.TargetName].Process(signal, signals);
                }
            }
            return low * high;
        }

        // the node before the end-point is (in my input) a conjunction - find all nodes that send to that node and when they first send a high signal
        var conjunction = modules.Where(x => x.Value.Outputs.Contains(endPoints.Single())).Select(x => x.Key).Single();
        var relevantNodes = modules.Where(x => x.Value.Outputs.Contains(conjunction)).Select(x => x.Key).ToHashSet();
        var push = 0;
        long firstHigh = 1;
        while (relevantNodes.Count > 0)
        {
            push++;
            var signals = new Queue<Signal>();
            signals.Enqueue(new Signal("broadcaster", false, "button"));
            while (signals.Count > 0)
            {
                var signal = signals.Dequeue();
                if (signal.IsHigh && relevantNodes.Contains(signal.SourceName))
                {
                    relevantNodes.Remove(signal.SourceName);
                    firstHigh = MathHelpers.LeastCommonMultiple(firstHigh, push);
                }
                if (endPoints.Contains(signal.TargetName)) continue;
                modules[signal.TargetName].Process(signal, signals);
            }
        }
        return firstHigh;
    }
    
    private class Module(string name, int type, string[] outputs)
    {
        private string Name { get; } = name;
        public int Type { get; } = type;
        private bool Memory { get; set; }
        public string[] Outputs { get; } = outputs;
        public Dictionary<string, bool> SourceMemory = new();

        public void Process(Signal signal, Queue<Signal> signals)
        {
            switch (Type)
            {
                case 0:
                    Send(signal.IsHigh, signals);
                    return;
                case 1 when signal.IsHigh:
                    return;
                case 1:
                    Memory = !Memory;
                    Send(Memory, signals);
                    return;
                default:
                    SourceMemory[signal.SourceName] = signal.IsHigh;
                    Send(!SourceMemory.All(x => x.Value), signals);
                    break;
            }
        }

        private void Send(bool signal, Queue<Signal>signals)
        {
            foreach (var output in Outputs)
            {
                signals.Enqueue(new Signal(output, signal, Name));
            }
            
        }
    }

    private record Signal(string TargetName, bool IsHigh, string SourceName)
    {
        public readonly string TargetName = TargetName;
        public readonly bool IsHigh = IsHigh;
        public readonly string SourceName = SourceName;
    }
}