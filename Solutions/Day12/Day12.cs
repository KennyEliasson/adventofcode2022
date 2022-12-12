using Solutions;

public class Day12 : AdventOfCodeTests
{
    public Day12(ITestOutputHelper output) : base(output)
    { }

    public override void PartOne(List<string> lines)
    {
        var (grid, positions) = CreateMaps(lines);
        var start = positions.First(x => x.Height == 'S');
        var queue = new Queue<Position>();
        queue.Enqueue(start);

        var itemCovered = new HashSet<Position>();

        var shortestPath = Traverse(queue, itemCovered, grid);
        
        _output.WriteLine(shortestPath.ToString());
    }

    public override void PartTwo(List<string> lines)
    {
        var (grid, positions) = CreateMaps(lines);
        var start = positions.Where(x => x.Height is 'S' or 'a');

        var shortestPaths = new List<int>();
        foreach (var position in start)
        {
            var queue = new Queue<Position>();
            queue.Enqueue(position);

            var itemCovered = new HashSet<Position>();

            var steps = Traverse(queue, itemCovered, grid, shortestPaths.Any() ? shortestPaths.Min() : null);
            if(steps.HasValue)
                shortestPaths.Add(steps.Value);    
        }
        
        _output.WriteLine(shortestPaths.Min().ToString());
    }

    private (Position[,] grid, List<Position> positions) CreateMaps(List<string> lines)
    {
        var grid = new Position[lines.Count, lines[0].Length];
        var positions = new List<Position>();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                positions.Add(new Position(lines[i][j], j, i));
                grid[i, j] = new Position(lines[i][j], j, i);
            }
        }

        return (grid, positions);
    }

    private static int? Traverse(Queue<Position> queue, HashSet<Position> itemCovered, Position[,] grid, int? currentShortestPath = null)
    {
        while (queue.Count > 0)
        {
            var element = queue.Dequeue();
            
            if(element.Steps > currentShortestPath.GetValueOrDefault(int.MaxValue))
                continue;
            
            if (element.Height == 'E')
                return element.Steps;

            if (itemCovered.Contains(element))
                continue;

            itemCovered.Add(element);

            var neighbours = new List<Position>();

            if (grid.TryGet(element.Row, element.Col + 1, out var right))
            {
                neighbours.Add(right);
            }

            if (grid.TryGet(element.Row, element.Col - 1, out var left))
            {
                neighbours.Add(left);
            }

            if (grid.TryGet(element.Row + 1, element.Col, out var up))
            {
                neighbours.Add(up);
            }

            if (grid.TryGet(element.Row - 1, element.Col, out var down))
            {
                neighbours.Add(down);
            }

            foreach (var neighbour in neighbours)
            {
                var elementHeight = Convert.ToInt16(element.Height == 'S' ? 'a' : element.Height);
                var neighbourHeight = Convert.ToInt16(neighbour.Height == 'E' ? 'z' : neighbour.Height);
                if (neighbourHeight > elementHeight)
                {
                    if (neighbourHeight - elementHeight == 1)
                    {
                        queue.Enqueue(neighbour with {Steps = element.Steps + 1});
                    }
                }
                else
                {
                    queue.Enqueue(neighbour with {Steps = element.Steps + 1});
                }
            }
        }

        return null;
    }
}

public record Position(char Height, int Col, int Row, int Steps = 0);

public static class MatrixExtensions
{
    public static bool TryGet<T>(this T[,] matrix, int column, int row, out T? value)
    {
        value = default;
        if (column < 0 || row < 0)
            return false;

        if (column > matrix.GetUpperBound(0) || row > matrix.GetUpperBound(1))
            return false;

        value = matrix[column, row];
        return true;
    }
}
