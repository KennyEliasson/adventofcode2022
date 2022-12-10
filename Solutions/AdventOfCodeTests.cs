namespace Solutions;

public abstract class AdventOfCodeTests
{
    protected readonly ITestOutputHelper _output;

    protected AdventOfCodeTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [AdventOfCodeData("example")]
    [AdventOfCodeData("input")]
    public abstract void PartOne(List<string> lines);
    
    [Theory]
    [AdventOfCodeData("example")]
    [AdventOfCodeData("input")]
    public abstract void PartTwo(List<string> lines);
}