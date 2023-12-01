namespace Solutions.Y2022;

public class Day6 : AdventOfCodeTests
{
    public Day6(ITestOutputHelper output) : base(output)
    { }

    public override void PartOne(List<string> input)
    {
        var position = FindFirstDistinctCharInRange(input[0], 4);
        _output.WriteLine($"Start of data is from position {position}");
    }

    public override void PartTwo(List<string> input)
    {
        var position = FindFirstDistinctCharInRange(input[0], 14);

        _output.WriteLine($"Start of data is from position {position}");
    }

    private static int FindFirstDistinctCharInRange(string input, int howManyUniqueLetters)
    {
        var range = new Range(Index.Start, howManyUniqueLetters);
        while (input[range].Distinct().Count() != howManyUniqueLetters)
        {
            range = new Range(range.Start.Value + 1, range.End.Value + 1);
        }

        return range.End.Value;
    }
}

