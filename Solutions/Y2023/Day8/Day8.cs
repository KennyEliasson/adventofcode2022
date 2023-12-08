namespace Solutions.Y2023;

public class Day8 : AdventOfCodeTests
{

    public Day8(ITestOutputHelper output) : base(output)
    {

    }

    public override void PartOne(List<string> lines)
    {
        var instructions = lines[0].Select(x => x).ToList();
        var elementInput = lines.Skip(2).Select(x => (x[..3], x[7..10], x[12..15])).ToDictionary(x => x.Item1);

        var steps = GoToZZZ(elementInput, instructions);

        _output.WriteLine($"Steps = {steps}");
    }

    private static int GoToZZZ(Dictionary<string, (string, string, string)> elementInput, List<char> instructions)
    {
        var input = elementInput["AAA"];
        int steps = 0;
        while (true)
        {
            foreach (var instruction in instructions)
            {
                input = instruction == 'L' ? elementInput[input.Item2] : elementInput[input.Item3];
                steps++;

                if (input.Item1 == "ZZZ")
                    return steps;
            }
        }
    }

    public override void PartTwo(List<string> lines)
    {
        var instructions = lines[0].Select(x => x).ToList();
        var elementInput = lines.Skip(2).Select(x => (x[..3], x[7..10], x[12..15])).ToDictionary(x => x.Item1);


        var steps = GoToZZZAsGhost(elementInput, instructions);

        _output.WriteLine($"Steps = {steps}");
    }

    private long GoToZZZAsGhost(Dictionary<string,(string, string, string)> elementInput, List<char> instructions)
    {
        var inputs = elementInput.Where(x => x.Key.EndsWith('A')).Select(x => x.Value).ToList();
        int steps = 0;
        while (true)
        {
            foreach (var instruction in instructions)
            {
                var newInput = new List<(string, string, string)>();

                foreach (var input in inputs)
                {
                    var match = instruction == 'L' ? elementInput[input.Item2] : elementInput[input.Item3];
                    newInput.Add(match);
                }

                steps++;

                if (newInput.All(x => x.Item1.EndsWith('Z')))
                {
                    return steps;
                }

                inputs = newInput;
            }
        }
    }
}
