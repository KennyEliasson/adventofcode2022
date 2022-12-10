namespace Solutions;

public class Day3 : AdventOfCodeTests
{
    public Day3(ITestOutputHelper output) : base(output)
    { }
    
    public override void PartOne(List<string> lines)
    {
        char FindItemsThatAreInBoth(string line)
        {
            return line[..(line.Length / 2)].Distinct()
                .Intersect(line[(line.Length / 2)..].Distinct())
                .First();
        }
        
        _output.WriteLine($"The sum of items is {lines.Select(FindItemsThatAreInBoth).Sum(CharToPrio)}");
    }

    private static int CharToPrio(char item)
    {
        return char.IsUpper(item) ? item - 65 + 27 : item - 96;
    }

    public override void PartTwo(List<string> lines)
    {
        var groups = lines.Chunk(3);
        var sum = groups.Aggregate(0, (x, y) => x + CharToPrio(y[0].Intersect(y[1]).Intersect(y[2]).First()));

        _output.WriteLine($"The sum of items is {sum}");
    }
}
