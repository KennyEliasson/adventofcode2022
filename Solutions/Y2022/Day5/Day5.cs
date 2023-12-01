using System.Text.RegularExpressions;

namespace Solutions.Y2022;

public class Day5 : AdventOfCodeTests
{
    public Day5(ITestOutputHelper output) : base(output)
    { }

    private IEnumerable<Stack<char>> CreateStacks(IEnumerable<string> stackLines)
    {
        for (var i = 1; i <= 36; i += 4)
        {
            yield return new Stack<char>(stackLines.Select(x => x[i]).Where(char.IsUpper).Reverse());
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

    public override void PartOne(List<string> lines)
    {
        var stacks = CreateStacks(lines.ToArray()[..9]).ToList();
        var instructions = CreateInstructions(lines.ToArray()[10..]);

        foreach (var (move, from, to) in instructions)
        {
            for (var i = 0; i < move; i++)
            {
                stacks[to-1].Push(stacks[from - 1].Pop());
            }
        }

        _output.WriteLine($"The order is {string.Join("", stacks.Select(x => x.Pop()))}");
    }

    public override void PartTwo(List<string> lines)
    {
        var stacks = CreateStacks(lines.ToArray()[..9]).ToList();
        var instructions = CreateInstructions(lines.ToArray()[10..]);

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

