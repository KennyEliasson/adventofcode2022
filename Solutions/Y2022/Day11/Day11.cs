namespace Solutions.Y2022;

public class Day11
{
    private readonly ITestOutputHelper _output;

    public Day11(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void PartOne()
    {
        var monkeys = MyInputMonkeys();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryDequeue(out var item))
                {
                    item = monkey.InspectItem(item);
                    item = Convert.ToInt32(Math.Floor((decimal) item / 3));
                    var monkeyIndex = monkey.ShouldThrowTo[monkey.Test(item)];
                    monkeys[monkeyIndex].Items.Enqueue(item);
                }
            }
        }

        _output.WriteLine(monkeys.OrderByDescending(x => x.InspectCount).Take(2).Select(x => x.InspectCount).Aggregate((x, y) => x * y).ToString());
    }

    [Fact]
    public void PartTwo()
    {
        var monkeys = MyInputMonkeys();

        // This is the super modulo that I've been looking for all day.
        // Ive been looking for square roots, using BigIntegers (too big number anyway!)
        // Tried different modulo like Sum all items at the start, at each round.
        // Summing DividBy etc etc, what works in the end was to Aggregate DivisibleBy
        var superModulo3 = monkeys.Select(x => x.DividBy).Aggregate((x, y) => x * y);
        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryDequeue(out var item))
                {
                    item = monkey.InspectItem(item);
                    item %= superModulo3;
                    var monkeyIndex = monkey.ShouldThrowTo[monkey.Test(item)];
                    monkeys[monkeyIndex].Items.Enqueue(item);
                }
            }
        }

        _output.WriteLine(monkeys.OrderByDescending(x => x.InspectCount).Take(2).Select(x => x.InspectCount).Aggregate((x, y) => x * y).ToString());
    }

    private static List<Monkey> ExampleMonkeys()
    {
        return new List<Monkey>()
        {
            new Monkey(23, old => old * 19, new long[] {79, 98}, 2, 3),
            new Monkey(19, old => old + 6, new long[] {54, 65, 75, 74}, 2, 0),
            new Monkey(13, old => old * old, new long[] {79, 60, 97}, 1, 3),
            new Monkey(17, old => old + 3, new long[] {74}, 0, 1)
        };
    }

    private static List<Monkey> MyInputMonkeys()
    {
        return new List<Monkey>
        {
            new Monkey(7, old => old * 13, new long[]{ 91, 58, 52, 69, 95, 54 }, 1, 5),
            new Monkey(3, old => old * old, new long[]{ 80, 80, 97, 84 }, 3, 5),
            new Monkey(2, old => old + 7, new long[]{ 86, 92, 71 }, 0, 4),
            new Monkey(11, old => old + 4, new long[]{ 96, 90, 99, 76, 79, 85, 98, 61 }, 7, 6),
            new Monkey(17, old => old * 19, new long[]{ 60, 83, 68, 64, 73 }, 1, 0),
            new Monkey(5, old => old + 3, new long[]{ 96, 52, 52, 94, 76, 51, 57 }, 7, 3),
            new Monkey(13, old => old + 5, new long[]{ 75 }, 4, 2),
            new Monkey(19, old => old + 1, new long[]{ 83, 75 }, 2, 6)

        };
    }
}

public class Monkey
{
    public Monkey(int dividBy, Func<long, long> inspect, long[] items, int ifTrue, int ifFalse)
    {
        DividBy = dividBy;
        Inspect = inspect;
        Items = new Queue<long>(items);
        StartingVal = items.Sum(x => x);
        ShouldThrowTo = new Dictionary<bool, int>()
        {
            { true, ifTrue},
            { false, ifFalse}
        };
    }

    public long StartingVal { get; set; }

    public bool Test(long val)
    {
        var (_, remainder) = long.DivRem(val, DividBy);
        return remainder == 0;
    }

    public int DividBy { get; set; }

    public long InspectItem(long val)
    {
        InspectCount++;
        return Inspect(val);
    }

    public long InspectCount { get; set; }
    public Func<long, long> Inspect { get; set; }
    public Queue<long> Items { get; set; }
    public Dictionary<bool, int> ShouldThrowTo { get; set; }
}
