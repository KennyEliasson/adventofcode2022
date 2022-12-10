namespace Solutions;

public class Day9 : AdventOfCodeTests
{
    public Day9(ITestOutputHelper output) : base(output)
    { }
    
    public override void PartOne(List<string> lines)
    {
        var actions = ParseMoveCommands(lines);

        var head = new Knot();
        var tail = new Knot();

        foreach (var action in actions)
        {
            action.Move(head, tail);
        }
        
        _output.WriteLine(tail.VisitedPositions.Count.ToString());
        
    }
    
    public override void PartTwo(List<string> lines)
    {
        var actions = ParseMoveCommands(lines);

        var knots = Enumerable.Range(0, 10).Select(_ => new Knot()).ToList();

        foreach (var action in actions)
        {
            action.Move(knots);
        }
        
        _output.WriteLine($"{knots.Last().VisitedPositions.Count}");
    }

    private static List<MoveCommand> ParseMoveCommands(List<string> lines)
    {
        var actions = new List<MoveCommand>();

        foreach (var line in lines)
        {
            actions.Add(line.Split(' ') switch
            {
                ["U", var steps] => new MoveCommand(int.Parse(steps), knot => knot.Y -= 1),
                ["D", var steps] => new MoveCommand(int.Parse(steps), knot => knot.Y += 1),
                ["R", var steps] => new MoveCommand(int.Parse(steps), knot => knot.X += 1),
                ["L", var steps] => new MoveCommand(int.Parse(steps), knot => knot.X -= 1),
            });
        }

        return actions;
    }
}

public class Knot
{
    public int X { get; set; }
    public int Y { get; set; }
    public HashSet<(int x, int y)> VisitedPositions { get; } = new();

    public Knot()
    {
        VisitedPositions.Add((X, Y));
    }

    public void Follow(Knot knot)
    {
        if (knot.X == X)
        {
            var steps = knot.Y - Y;
            Y = steps > 1 ? Y + 1 : steps < -1 ? Y - 1 : Y;
        } 
        else if (knot.Y == Y)
        {
            var steps = knot.X - X;
            X = steps > 1 ? X + 1 : steps < -1 ? X - 1 : X;
        }
        else if(Math.Abs(knot.X - X) + Math.Abs(knot.Y - Y) > 2)
        {
            X += Math.Sign(knot.X - X);
            Y += Math.Sign(knot.Y- Y);
        }
        
        VisitedPositions.Add((X, Y));
    }
}

public class MoveCommand
{
    public MoveCommand(int steps, Action<Knot> movement)
    {
        Steps = steps;
        Movement = movement;
    }

    public void Move(Knot head, Knot tail)
    {
        for (var i = 0; i < Steps; i++)
        {
            Movement(head);
            tail.Follow(head);
        }
    }
    
    public void Move(List<Knot> knots)
    {
        for (int step = 0; step < Steps; step++)
        {
            Movement(knots[0]); // Move the head first
            for (int index = 0; index < knots.Count-1; index++)
            {
                // Move all tails
                var currentHead = knots[index];
                var tail = knots[index+1];
                
                tail.Follow(currentHead);
            }
        }
    }
    
    public Action<Knot> Movement { get; }
    public int Steps { get; }
}
