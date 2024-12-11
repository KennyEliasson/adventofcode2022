using System.Globalization;

namespace Solutions.Y2024;

public class Day11(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public class FirstRule : IRule
    {
        public (bool, List<long>?) Execute(long input)
        {
            if (input == 0)
            {
                return (true, [1]);
            }

            return (false, null);
        }
    }
    
    public class SecondRule : IRule
    {
        public (bool, List<long>?) Execute(long input)
        {
            var inputAsString = input.ToString();
            if (inputAsString.Length % 2 == 0)
            {
                var left = inputAsString[..(inputAsString.Length / 2)];
                var right = inputAsString[(inputAsString.Length / 2)..];
                return (true, [long.Parse(left), long.Parse(right)]);
            }

            return (false, null);
        }
    }

    public class Abc
    {
        
        public static IEnumerable<long> Execute(long input)
        {
            if (input == 0)
            {
                yield return 1;
                yield break;
            }
            var length = (long)Math.Log10(input) + 1;
            if (length % 2 == 0)
            {
                var divisor = (long)Math.Pow(10, length / 2); // Divisor to split the number

                // Split the number into two halves
                long leftHalf = input / divisor;
                long rightHalf = input % divisor;

                yield return leftHalf;
                yield return rightHalf;
                yield break;
            }

            yield return input * 2024;
        }
    }
    
    public class SecondRuleMemory : IRule
    {
        
        public (bool, List<long>?) Execute(long input)
        {
            var length = (long)Math.Log10(input) + 1;
            if (length % 2 == 0)
            {
                var divisor = (long)Math.Pow(10, length / 2); // Divisor to split the number

                // Split the number into two halves
                long leftHalf = input / divisor;
                long rightHalf = input % divisor;
                
                return (true, [leftHalf, rightHalf]);
            }
            
            return (false, null);
        }
    }
    
    public class ThirdRule : IRule
    {
        public (bool, List<long>?) Execute(long input)
        {
            return (true, [input * 2024]);
            
        }
    }
 
    public override void PartOne(List<string> lines)
    {
        var rules = new List<IRule>
        {
            new FirstRule(), new SecondRuleMemory(), new ThirdRule()
        };

        //var input = new List<long>() { 0, 1, 10, 99, 999 };
        //var input = new List<long>() { 125, 17 };
        var input = lines[0].Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList();  
        for (int i = 0; i < 26; i++)
        {
            _output.WriteLine($"{i} has {input.Count}");
            var oldList = new List<long>(input);
            input.Clear();
            foreach (var l in oldList)
            {
                foreach (var rule in rules)
                {
                    var (success, data) = rule.Execute(l);
                    if (success)
                    {
                        input.AddRange(data!);
                        break;
                    }
                }
            }
        }
    }

    public static long Counter = 0;
    public static List<long> Data = [];
   
    static void ProcessEnumerable(IEnumerable<long> enumerable, int iterations)
    {
        if (iterations == 40)
        {
            Counter += enumerable.Count();  
           // Data.AddRange(enumerable);
            return;
        }
        
        foreach (var item in enumerable)
        {
            var enumerator = Abc.Execute(item);
            ProcessEnumerable(enumerator, iterations+1);
        }
    }
    
    public override void PartTwo(List<string> lines)
    {
       
        var input = lines[0].Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList();

       // input = [253, 0, 2024, 14168];
        
        foreach (var l in input)
        {
            ProcessEnumerable(new List<long>() {l}.AsEnumerable(), 0);
        }
        _output.WriteLine($"{Counter}");
        
        _output.WriteLine(string.Join(',', Data));
        
    }
}

public interface IRule
{
    (bool, List<long>?) Execute(long input);
}
