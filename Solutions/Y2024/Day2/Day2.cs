namespace Solutions.Y2024;

public class Day2(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public record Report(List<int> Levels)
    {
        public bool IsSafe() => IsSafe(Levels);
        
        private bool IsSafe(List<int> levels)
        {
            bool? increasing = null;
            for (var i = 0; i < levels.Count-1; i++)
            {
                var diff = levels[i] - levels[i + 1];

                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }

                if (diff > 0 && increasing.GetValueOrDefault(true))
                    increasing = true;
                else if (diff < 0 && increasing.GetValueOrDefault(false) == false)
                    increasing = false;
                else
                    return false;
            }

            return true;
        }
        
        public bool IsSafeDampifier()
        {
            var isSafe = IsSafe();
            if (isSafe)
                return true;
            
            for (var i = 0; i < Levels.Count; i++)
            {
                var copy = new List<int>(Levels);
                copy.RemoveAt(i);
                var safe = IsSafe(copy);
                if (safe)
                    return true;
            }

            return false;
        }
    }
    
    public override void PartOne(List<string> lines)
    {
        var reports = lines.Select(line => new Report(line.Split(' ').Select(int.Parse).ToList())).ToList();
        _output.WriteLine(reports.Count(x => x.IsSafe()).ToString());
    }
    
    public override void PartTwo(List<string> lines)
    {
        var reports = lines.Select(line => new Report(line.Split(' ').Select(int.Parse).ToList())).ToList();
        _output.WriteLine(reports.Count(x => x.IsSafeDampifier()).ToString());
    }
}
