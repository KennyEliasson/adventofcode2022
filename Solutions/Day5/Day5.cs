using System.Text.RegularExpressions;

namespace Solutions;

public class Day5 {

    private readonly ITestOutputHelper _output;

    public Day5(ITestOutputHelper output)
    {
        _output = output;
    }

    private IEnumerable<Stack<char>> CreateStacks(IEnumerable<string> stackLines)
    {
        for (var i = 1; i <= 36; i += 4)
        {
            IEnumerable<char> g = stackLines.Select(x => x[i]).Where(char.IsUpper).ToList();
            yield return new Stack<char>(g.Reverse());
        }
    }
    
    private IEnumerable<(int amout, int from, int to)> CreateInstructions(IEnumerable<string> instructionLines)
    {
        var regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
        foreach (var line in instructionLines)
        {
            var matches = regex.Match(line);
            yield return (int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value));
        }
    }

    [Fact]
    public void PartOne()
    {
        var lines = File.ReadAllLines("Day5/input.txt");
        
        var stacks = CreateStacks(lines[..9]).ToList();
        var instructions = CreateInstructions(lines[10..]);

        foreach (var (move, from, to) in instructions)
        {
            for (var i = 0; i < move; i++)
            {   
                stacks[to-1].Push(stacks[from - 1].Pop());
            }
        }

        _output.WriteLine($"The order is {string.Join("", stacks.Select(x => x.Pop()))}");
    }

    [Fact]
    public void PartTwo()
    {
        var lines = File.ReadAllLines("Day5/input.txt");
        
        var stacks = CreateStacks(lines[..9]).ToList();
        var instructions = CreateInstructions(lines[10..]);

        foreach (var (move, from, to) in instructions)
        {
            var crates = new List<char>();
            for (var i = 0; i < move; i++)
            {   
                crates.Add(stacks[from - 1].Pop());
            }

            crates.Reverse();

            foreach (var crate in crates)
            {
                stacks[to-1].Push(crate);
            }
        }

        _output.WriteLine($"The order is {string.Join("", stacks.Select(x => x.Pop()))}");
    }
}

