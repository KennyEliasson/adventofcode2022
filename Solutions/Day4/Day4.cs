namespace Solutions;

public class Day4 {

    private readonly ITestOutputHelper _output;

    public Day4(ITestOutputHelper output)
    {
        _output = output;
    }

    private IEnumerable<(HashSet<int> firstRange, HashSet<int> secondRange)> CreatePairs(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var pairs = line.Split(',');
            var firstPairRange = pairs[0].Split('-').Select(int.Parse).ToList();
            var secondPairRange = pairs[1].Split('-').Select(int.Parse).ToList();
            
            var firstRange = Enumerable.Range(firstPairRange[0], firstPairRange[1] - firstPairRange[0]+1).ToHashSet();
            var secondRange = Enumerable.Range(secondPairRange[0], secondPairRange[1] - secondPairRange[0]+1).ToHashSet();

            yield return (firstRange, secondRange);
        }
    }

    [Fact]
    public void PartOne()
    {
        var lines = File.ReadAllLines("Day4/input.txt");

        var pairs = CreatePairs(lines);
        var fullyContains = pairs.Count(pair => pair.firstRange.IsSubsetOf(pair.secondRange) || pair.firstRange.IsSubsetOf(pair.firstRange));

        _output.WriteLine($"Fully contains count is {fullyContains}");
    }

    [Fact]
    public void PartTwo()
    {
        var lines = File.ReadAllLines("Day4/input.txt");
        var pairs = CreatePairs(lines);

        var overlaps = pairs.Count(pair => pair.firstRange.Overlaps(pair.secondRange) || pair.firstRange.Overlaps(pair.firstRange));

        _output.WriteLine($"Overlap count is {overlaps}");
    }
}

