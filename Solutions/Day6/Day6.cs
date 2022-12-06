namespace Solutions;

public class Day6 {

    private readonly ITestOutputHelper _output;

    public Day6(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void PartOne()
    {
        var input = File.ReadAllText("Day6/input.txt");
        
        var position = FindFirstDistinctCharInRange(input, 4);
            
        _output.WriteLine($"Start of data is from position {position}");
    }

    [Fact]
    public void PartTwo()
    {
        var input = File.ReadAllText("Day6/input.txt");
        var position = FindFirstDistinctCharInRange(input, 14);

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

