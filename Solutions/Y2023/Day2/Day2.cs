using System.Text.RegularExpressions;

namespace Solutions.Y2023;

public partial class Day2 : AdventOfCodeTests
{
    private readonly Regex _regex;

    public Day2(ITestOutputHelper output) : base(output)
    {
        _regex = Day2Regex();
    }
    
    public override void PartOne(List<string> lines)
    {
        var sum = 0;
        for (var i = 1; i <= lines.Count; i++)
        {
            var matches = _regex.Matches(lines[i]);
            if (IsValid(matches))
            {
                sum += i;
            }
        }

        _output.WriteLine($"Sum = {sum}");
    }

    private static bool IsValid(MatchCollection matches)
    {
        foreach (Match? match in matches)
        {
            var count = int.Parse(match.Groups[1].Value);
            var color = match.Groups[2].Value;
            
            switch (color)
            {
                case "r" when count > 12:
                case "g" when count > 13:
                case "b" when count > 14:
                    return false;
            }
        }

        return true;
    }

    public override void PartTwo(List<string> lines)
    {
        var total = 0;
        
        Dictionary<string, List<int>> result = new()
        {
            { "r", new List<int>()},
            { "g", new List<int>()},
            { "b", new List<int>()}
        };
        foreach (var line in lines)
        {
            var replacedLine = line.Remove(0, line.IndexOf(':'));
            var groups = replacedLine.Split(';');

            foreach (var group in groups)
            {
                var matches = _regex.Matches(group);   
                
                foreach (Match match in matches)
                {
                    var count = int.Parse(match.Groups[1].Value);
                    var color = match.Groups[2].Value;
                    
                    result[color].Add(count);
                }
            }

            var sum = result["g"].Max() * result["r"].Max() * result["b"].Max();
            total += sum;
        }
        _output.WriteLine($"Calibration = {total}");
    }

    [GeneratedRegex("(\\d*) ([r|b|g])")]
    private static partial Regex Day2Regex();
}
