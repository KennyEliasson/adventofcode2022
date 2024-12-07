namespace Solutions.Y2024;

public class Day5(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public record Rule(int Left, int Right);

    public record Update(List<int> Pages)
    {
        public int GetMiddleNumber()
        {
            return Pages.Count % 2 != 0 ? Pages[Pages.Count / 2] : Pages[Pages.Count / 2 - 1];
        }
        
        public bool IsValidToRules(List<Rule> rules)
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                var leftHandRules = rules.Where(x => x.Left == Pages[i]);
                var rightHandRules = rules.Where(x => x.Right == Pages[i]);
                
                foreach (var rule in leftHandRules)
                {
                    bool match = Pages.Take(i).Any(x => x == rule.Right);
                    if (match) return false;
                }

                foreach (var rule in rightHandRules)
                {
                    bool match = Pages.Skip(i).Any(x => x == rule.Left);
                    if (match) return false;
                }
                
            }
            
            return true;
        }
        
    }
    
    public static int CompareNumbers(int x, int y, List<Rule> rules)
    {
        bool xBeforeY = rules.Exists(rule => rule.Left == x && rule.Right == y);
        bool yBeforeX = rules.Exists(rule => rule.Left == y && rule.Right == x);
        
        if (xBeforeY)
            return -1;
        
        if (yBeforeX)
            return 1;
        
        return 0;
    }
    
    private static (List<Rule>, List<Update>) CreateRulesAndUpdates(List<string> lines)
    {
        var rules = new List<Rule>();
        var updates = new List<Update>();
        bool isRule = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRule = false;
                continue;
            }
            
            if (isRule)
            {
                var split = line.Split('|');
                rules.Add(new Rule(int.Parse(split[0]), int.Parse(split[1])));
            }
            else
            {
                var updateRow = line.Split(',');
                updates.Add(new Update(updateRow.Select(int.Parse).ToList()));
            }
        }

        return (rules, updates);
    }
    
    public override void PartOne(List<string> lines)
    {
        var (rules, updates) = CreateRulesAndUpdates(lines);

        var validUpdates = updates.Where(x => x.IsValidToRules(rules));
        _output.WriteLine(validUpdates.Sum(x => x.GetMiddleNumber()).ToString());
        
        _output.WriteLine("");
    }


    public override void PartTwo(List<string> lines)
    {
        var (rules, updates) = CreateRulesAndUpdates(lines);
        var invalidUpdates = updates.Where(x => !x.IsValidToRules(rules)).ToList();
        foreach (var update in invalidUpdates)
        {
            update.Pages.Sort((x, y) => CompareNumbers(x, y, rules));
        }
        _output.WriteLine(invalidUpdates.Sum(x => x.GetMiddleNumber()).ToString());
    }
}
