namespace Solutions.Y2022;

public class Day1 : AdventOfCodeTests
{
    public Day1(ITestOutputHelper output) : base(output)
    { }

    private IEnumerable<int> CalculateCalories(List<string> lines)
    {
        var current = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                yield return current;
                current = 0;
                continue;
            }

            current += int.Parse(line);
        }

        yield return current;
    }

    public override void PartOne(List<string> lines)
    {
        var elfs = CalculateCalories(lines);
        _output.WriteLine($"Max calories is {elfs.Max(x => x)}");
    }

    public override void PartTwo(List<string> lines)
    {
        var elfs = CalculateCalories(lines);
        _output.WriteLine($"Top three elves with most calories are carrying {elfs.OrderByDescending(x => x).Take(3).Sum(x => x)} calories");
    }
}
