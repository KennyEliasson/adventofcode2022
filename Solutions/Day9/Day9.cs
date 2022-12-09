namespace Solutions;

public class Day9
{
    private readonly ITestOutputHelper _output;

    public Day9(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void PartOne()
    {
        var lines = File.ReadLines("Day9/input.txt").ToList();
        
        var actions = ParseMoveCommands(lines);

        var head = new Knot(0, 0);
        var tail = new Knot(head.X, head.Y);

        foreach (var action in actions)
        {
            action.Move(head, tail);
        }
        
        _output.WriteLine(tail.VisitedPositions.Count.ToString());
        
    }
    
    [Fact]
    public void PartTwo()
    {
        var lines = File.ReadLines("Day9/input.txt").ToList();
        
        var actions = ParseMoveCommands(lines);

        var knots = Enumerable.Range(0, 10).Select(_ => new Knot(0, 0)).ToList();

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

    public Knot(int x, int y)
    {
        X = x;
        Y = y;
        VisitedPositions.Add((X, Y));
    }

    public void Follow(Knot knot)
    {
        var diffX = Math.Abs(knot.X - X);
        var diffY = Math.Abs(knot.Y - Y);

        if (knot.X == X && diffY > 1)
        {
            var steps = knot.Y - Y;
            Y = steps > 0 ? Y + 1 : Y - 1;
        } 
        else if (knot.Y == Y && diffX > 1)
        {
            var steps = knot.X - X;
            X = steps > 0 ? X + 1 : X - 1;
        }
        else
        {
            if (Math.Abs(diffX + diffY) < 3)
            {
                return;
            }

            var stepsX = knot.X - X;
            var stepsY = knot.Y - Y;

            X = stepsX > 0 ? X + 1 : X - 1;
            Y = stepsY > 0 ? Y + 1 : Y - 1;
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
