namespace Solutions.Y2024;

public class Day7(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public record MathematicalOperations(long Sum, List<Operator> Operators, List<int> Numbers)
    {
        private readonly List<MathematicalOperations> _children = [];

        public void Execute()
        {
            if (Numbers.Count == 0)
            {
                return;
            }
            
            var nextNumber = Numbers.First();
            foreach (var op in Operators)
            {
                if (op == Operator.Addition)
                    _children.Add(new MathematicalOperations(Sum + nextNumber, Operators, Numbers.Skip(1).ToList()));
                else if (op == Operator.Multiplication) 
                    _children.Add(new MathematicalOperations(Sum * nextNumber, Operators, Numbers.Skip(1).ToList()));
                else if (op == Operator.Concatenate)
                    _children.Add(new MathematicalOperations(long.Parse(Sum.ToString() + nextNumber), Operators, Numbers.Skip(1).ToList()));
                
            }

            foreach (var child in _children)
            {
                child.Execute();
            }
        }
        
        public IEnumerable<long> GetAllSums()
        {
            foreach (var child in _children)
            {
                if (child.Numbers.Count == 0)
                {
                    yield return child.Sum;
                }
                else
                {
                    foreach (var i in child.GetAllSums())
                    {
                        yield return i;
                    }
                }
            }
        }
    }
    
    public class Equation(long testValue, List<int> numbers, List<Operator> operators)
    {
        public long TestValue { get; } = testValue;
        private readonly MathematicalOperations _mathematicalOperations = new(numbers.First(), operators, numbers.Skip(1).ToList());
        
        public void Execute()
        {
            _mathematicalOperations.Execute();
        }

        public bool HasCorrectTestValue()
        {
            var sums = _mathematicalOperations.GetAllSums().ToList();
            return sums.Any(x => x == TestValue);
        }
    }
    
    public override void PartOne(List<string> lines)
    {
        var equations = new List<Equation>();
        foreach (var line in lines)
        {
            var splitOne = line.Split(":", StringSplitOptions.TrimEntries);
            var sum = long.Parse(splitOne[0]);
            var numbers = splitOne[1].Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            equations.Add(new Equation(sum, numbers, [Operator.Addition, Operator.Multiplication]));
        }

        foreach (var equation in equations)
        {
            equation.Execute();
        }

        var result = equations.Where(x => x.HasCorrectTestValue()).Sum(x => x.TestValue);
        
        _output.WriteLine(result.ToString());
    }

    public override void PartTwo(List<string> lines)
    {
        var equations = new List<Equation>();
        foreach (var line in lines)
        {
            var splitOne = line.Split(":", StringSplitOptions.TrimEntries);
            var sum = long.Parse(splitOne[0]);
            var numbers = splitOne[1].Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            equations.Add(new Equation(sum, numbers, [Operator.Addition, Operator.Multiplication, Operator.Concatenate]));
        }

        foreach (var equation in equations)
        {
            equation.Execute();
        }

        var result = equations.Where(x => x.HasCorrectTestValue()).Sum(x => x.TestValue);
        
        _output.WriteLine(result.ToString());
    }
}

public enum Operator
{
    Addition,
    Multiplication,
    Concatenate
}
