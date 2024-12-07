namespace Solutions.Y2024;

public class Day6(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public enum Direction { Up, Down, Left, Right };
    
    public override void PartOne(List<string> lines)
    {
        var grid = MatrixExtensions.CreateMatrix(lines);

        var coord = grid.Find('^');
        if (coord == null)
            throw new Exception();
        
        var direction = Direction.Up;

        bool walking = true;
        (int, int)? turnCoord = coord.Value;
        while (walking)
        {
            turnCoord = Walk(grid, turnCoord.Value, direction);
            if (turnCoord == null)
                break;

            direction = direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
            };
        }
        
        _output.WriteLine(grid.Count('X').ToString());
    }

    private (int, int)? Walk(char[,] grid, (int, int) coord, Direction direction)
    {
        Func<int, int, (int, int row)> walker = direction switch
        {
            Direction.Up => (col, row) => (col, row-1),
            Direction.Down => (col, row) => (col, row+1),
            Direction.Left => (col, row) => (col-1, row),
            Direction.Right => (col, row) => (col+1, row)
        };
        
        var lastCoord = coord;
        while (grid[coord.Item1, coord.Item2] != '#')
        {
            grid[coord.Item1, coord.Item2] = 'X';
            lastCoord = coord;
            coord = walker(coord.Item1, coord.Item2);
            if (grid.OutOfBounds(coord.Item1, coord.Item2))
            {
                return null;
            }
        }

        return lastCoord;
    }

    public override void PartTwo(List<string> lines)
    {
     
    }
}
