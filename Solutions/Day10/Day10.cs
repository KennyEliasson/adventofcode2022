namespace Solutions;

public class Day10 : AdventOfCodeTests
{
    public Day10(ITestOutputHelper output) : base(output)
    { }
    
    public override void PartOne(List<string> lines)
    {
        var instructions = ParseInstructions(lines);

        int cycle = 0;
        int x = 1;
        var cycles = new List<int>();
        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Cycles; i++)
            {
                cycles.Add(x);
                cycle++;
            }

            x = instruction.Execute(x);
        }

        cycles.Add(x);

        var data = new List<(int cycle, int x, int result)>();
        for (int i = 20; i < cycles.Count; i += 40)
        {
            data.Add((i, cycles[i - 1], i * cycles[i - 1]));
        }
        
        _output.WriteLine(data.Sum(x => x.result).ToString());
    }

    private IEnumerable<Instruction> ParseInstructions(List<string> lines)
    {
        return lines.Select(line => (Instruction)(line.Split(' ') switch
        {
            ["noop", ..] => new Noop(),
            ["addx", var val] => new Add(int.Parse(val)),
        }));
    }

    public override void PartTwo(List<string> lines)
    {
        var instructions = ParseInstructions(lines);

        int cycle = 0;
        int x = 1;
        var cycles = new List<int>();

        var screen = Enumerable.Repeat('.', 240).ToList();

        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Cycles; i++)
            {
                var compare = cycle - (Math.Floor((decimal)cycle/40) * 40);
                screen[cycle] = (x == compare || x - 1 == compare || x + 1 == compare) ? '#' : '.';
                cycles.Add(x);
                cycle++;
            }
            
            x = instruction.Execute(x);
        }

        cycles.Add(x);

        var screenChunks = screen.Chunk(40);
        foreach (var chunk in screenChunks)
        {
            _output.WriteLine(string.Join(null, chunk));
        }
    }
}

public abstract record Instruction(int Cycles)
{
    public virtual int Execute(int x) => x;
}

public record Noop() : Instruction(1);

public record Add(int Value) : Instruction(2)
{
    public override int Execute(int x)
    {
        return x + Value;
    }
}
