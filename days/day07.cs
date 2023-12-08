using AOC.util;

namespace AOC.days;

internal class Day07 : Day<long>
{
    protected override void SetSequence()
    {
        AddRun("Test 1", () => RunPart(1, "07_t1.txt"), 6440);
        AddRun("Part 1", () => RunPart(1, "07.txt"));
        AddRun("Test 2", () => RunPart(2, "07_t1.txt"), 5905);
        AddRun("Part 2", () => RunPart(2, "07.txt"));
    }

    public override long RunPart(int part, string inputName)
    {
        var input = GetListOfLines(inputName);
        return input
            .Select(x => new Hand(x, part))
            .Order()
            .Select((x, i) => x.Bid * (i + 1))
            .Sum();
    }

    public class Hand : IComparable<Hand>, IComparable
    {
        private readonly int _strength;
        private readonly long _value;
        public readonly long Bid;

        public Hand(string input, int part)
        {
            var labels = (part == 1) ? "23456789TJQKA" : "J23456789TQKA";
            var parts = input.Split(' ');
            var cards = parts[0];
            Bid = long.Parse(parts[1]);
            _value = 0;

            var counts = new DefaultDictionary<char, int>();
            var sets = cards.Select(x => x).ToHashSet();
            foreach (var card in cards)
            {
                counts[card] += 1;
                _value = _value * 20 + labels.IndexOf(card);
            }

            var highestCount = counts.Select(x => x.Value).Max();

            if (part == 2)
            {
                var jokers = counts['J'];
                if (jokers is > 0 and < 5)
                {
                    var card = counts
                        .OrderByDescending(x => x.Value)
                        .First(x => x.Key != 'J');
                    counts[card.Key] += jokers;
                    counts['J'] = 0;
                    highestCount = counts.Select(x => x.Value).Max();
                    sets = counts.Where(x => x.Value > 0).Select(x => x.Key).ToHashSet();
                }
            }

            if (highestCount == 5)
            {
                _strength = 6; // Five of a kind
            }
            else if (highestCount == 4)
            {
                _strength = 5; // Four of a kind
            }
            else if (sets.Count == 2)
            {
                _strength = 4; // Full house
            }
            else if (highestCount == 3)
            {
                _strength = 3; // Three of a kind
            }
            else
            {
                _strength = counts.Count(x => x.Value == 2); // 2, 1, 0 pair
            }
        }
        
        public int CompareTo(Hand? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var strengthComparison = _strength.CompareTo(other._strength);
            if (strengthComparison != 0) return strengthComparison;
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Hand other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Hand)}");
        }

        public static bool operator <(Hand? left, Hand? right)
        {
            return Comparer<Hand>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(Hand? left, Hand? right)
        {
            return Comparer<Hand>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(Hand? left, Hand? right)
        {
            return Comparer<Hand>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(Hand? left, Hand? right)
        {
            return Comparer<Hand>.Default.Compare(left, right) >= 0;
        }
    }
}