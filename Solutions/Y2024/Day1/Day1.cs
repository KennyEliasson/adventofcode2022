namespace Solutions.Y2024;

public class Day1(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public override void PartOne(List<string> lines)
    {
        var (left, right) = CreateNumberLists(lines);

        left = left.OrderBy(x => x).ToList();
        right = right.OrderBy(x => x).ToList();

        var count = left.Select((t, i) => Math.Abs(t - right[i])).Sum();

        _output.WriteLine(count.ToString());
    }

    private static (List<int>, List<int>) CreateNumberLists(List<string> lines)
    {
        var left = new List<int>(lines.Count);
        var right = new List<int>(lines.Count);
        foreach (var numbers in lines.Select(line => line.Split("   ")))
        {
            left.Add(Convert.ToInt32(numbers[0]));
            right.Add(Convert.ToInt32(numbers[1]));
        }

        return (left, right);
    }

    public override void PartTwo(List<string> lines)
    {
        var (left, right) = CreateNumberLists(lines);
        var count = left.Sum(t => t * right.Count(x => x == t));

        _output.WriteLine(count.ToString());
    }
}
