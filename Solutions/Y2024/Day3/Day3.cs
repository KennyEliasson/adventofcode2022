using System.Text.RegularExpressions;

namespace Solutions.Y2024;

public class Day3(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    private static readonly Regex Regex = new(@"(mul\((\d{1,3}),(\d{1,3})\))");
    
    public override void PartOne(List<string> lines)
    {
        _output.WriteLine(CalculateMul(lines[0]).ToString());
    }
    
    public override void PartTwo(List<string> lines)
    {
        var negativeSplits = lines[0].Split("don't()");
        var sum = CalculateMul(negativeSplits[0]); 
        foreach (var negativeSplit in negativeSplits.Skip(1))
        {
            sum += negativeSplit.Split("do()").Skip(1).Sum(CalculateMul);
        }
        _output.WriteLine(sum.ToString());
    }

    private static int CalculateMul(string input)
    {
        var collections = Regex.Matches(input);

        var sum = 0;
        foreach (Match match in collections)
        {
            sum += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
        }

        return sum;
    }
}
