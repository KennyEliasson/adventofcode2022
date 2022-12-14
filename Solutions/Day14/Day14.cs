using System.Text;

public class Day14 : AdventOfCodeTests
{
    public Day14(ITestOutputHelper output) : base(output)
    {
        
    }

    public override void PartOne(List<string> lines)
    {
        var grid = InitializeCave();

        var lowestPositionOfRock = CreateRocksInCave(lines, grid);
        
        var sandList = new List<Sand>();
        var canSpawnSand = true;
        while (true)
        {
            if (canSpawnSand)
            {
                sandList.Add(new Sand(0, 500));
                canSpawnSand = false;
            }

            var currentSand = sandList.Last();

            if (currentSand.Row > lowestPositionOfRock)
            {
                break;
            }

            if (!currentSand.TryMove(grid))
            {
                canSpawnSand = true;
                grid[currentSand.Row, currentSand.Col] = 'o';
            }
        }
        
        _output.WriteLine($"A total of {sandList.Count-1 } units of sand ");

        foreach (var builder in grid.Write())
        {
            _output.WriteLine(builder.ToString());            
        }
        
    }
    
    public override void PartTwo(List<string> lines)
    {
        var grid = InitializeCave();

        var lowestPositionOfRock = CreateRocksInCave(lines, grid);

        for (int i = 0; i < grid.GetUpperBound(1); i++)
        {
            grid[lowestPositionOfRock + 2, i] = '#';
        }
        
        var sandList = new List<Sand>();
        var canSpawnSand = true;
        while (true)
        {
            if (canSpawnSand)
            {
                if (grid[0, 500] == 'o')
                    break;
                
                sandList.Add(new Sand(0, 500));
                canSpawnSand = false;
            }

            var currentSand = sandList.Last();

            if (!currentSand.TryMove(grid))
            {
                canSpawnSand = true;
                grid[currentSand.Row, currentSand.Col] = 'o';
            }
        }
        
        _output.WriteLine($"A total of {sandList.Count} units of sand ");

        foreach (var builder in grid.Write())
        {
            _output.WriteLine(builder.ToString());            
        }
    }

    private int CreateRocksInCave(List<string> lines, char[,] grid)
    {
        var lowestRow = 0;

        foreach (var line in lines)
        {
            var positions = line.Split(" -> ");

            for (int i = 0; i < positions.Length - 1; i++)
            {
                var start = GetCoord(positions[i]);
                var end = GetCoord(positions[i + 1]);

                if (start.Column == end.Column)
                {
                    var min = Math.Min(start.Row, end.Row);
                    var max = Math.Max(start.Row, end.Row);

                    for (int j = min; j <= max; j++)
                    {
                        lowestRow = Math.Max(lowestRow, j);
                        grid[j, start.Column] = '#';
                    }
                }
                else
                {
                    lowestRow = Math.Max(lowestRow, start.Row);
                    var min = Math.Min(start.Column, end.Column);
                    var max = Math.Max(start.Column, end.Column);

                    for (int j = min; j <= max; j++)
                    {
                        grid[start.Row, j] = '#';
                    }
                }
            }
        }

        return lowestRow;
    }

    private static char[,] InitializeCave()
    {
        var grid = new char[1000, 1000];
        for (int row = 0; row < grid.GetUpperBound(0); row++)
        {
            for (int column = 0; column < grid.GetUpperBound(1); column++)
            {
                grid[row, column] = '.';
            }
        }

        return grid;
    }

    private (int Column, int Row) GetCoord(string position)
    {
        var split = position.Split(',');
        return (int.Parse(split[0]), int.Parse(split[1]));
    }
    
   

    public class Sand
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Sand(int row, int col)
        {
            Row = row;
            Col = col;
        }
        
        public bool TryMove(char[,] grid)
        {
            if (grid[Row + 1, Col] == '.')
            {
                Row += 1;
                return true;
            }

            if (grid[Row + 1, Col - 1] == '.')
            {
                Row += 1;
                Col -= 1;
                return true;
            }
            
            if (grid[Row + 1, Col + 1] == '.')
            {
                Row += 1;
                Col += 1;
                return true;
            }

            return false;
        }
    }

}
