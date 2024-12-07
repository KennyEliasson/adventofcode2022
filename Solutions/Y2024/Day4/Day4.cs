namespace Solutions.Y2024;

public class Day4(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    private int SearchForMAS(char[,] matrix, (int column, int row) fromPos)
    {
        var forward = new List<(int column, int row)>
        {
            (fromPos.column + 1, fromPos.row), (fromPos.column + 2, fromPos.row), (fromPos.column + 3, fromPos.row)
        };

        var backwards = new List<(int column, int row)>
        {
            (fromPos.column - 1, fromPos.row), (fromPos.column - 2, fromPos.row), (fromPos.column - 3, fromPos.row)
        };
        
        var upwards = new List<(int column, int row)>
        {
            (fromPos.column, fromPos.row-1), (fromPos.column, fromPos.row-2), (fromPos.column, fromPos.row-3)
        };
        
        var downwards = new List<(int column, int row)>
        {
            (fromPos.column, fromPos.row+1), (fromPos.column, fromPos.row+2), (fromPos.column, fromPos.row+3)
        };
        
        var upLeft = new List<(int column, int row)>
        {
            (fromPos.column-1, fromPos.row-1), (fromPos.column-2, fromPos.row-2), (fromPos.column-3, fromPos.row-3)
        };
        
        var upRight = new List<(int column, int row)>
        {
            (fromPos.column+1, fromPos.row-1), (fromPos.column+2, fromPos.row-2), (fromPos.column+3, fromPos.row-3)
        };
        
        var downRight = new List<(int column, int row)>
        {
            (fromPos.column+1, fromPos.row+1), (fromPos.column+2, fromPos.row+2), (fromPos.column+3, fromPos.row+3)
        };
        
        var downLeft = new List<(int column, int row)>
        {
            (fromPos.column-1, fromPos.row+1), (fromPos.column-2, fromPos.row+2), (fromPos.column-3, fromPos.row+3)
        };

        var allDirections = new List<List<(int column, int row)>>
        {
            forward, backwards, upwards, downwards, upLeft, downLeft, upRight, downRight
        };

        return allDirections.Count(direction => matrix.IsEqual(direction[0], 'M') && matrix.IsEqual(direction[1], 'A') && matrix.IsEqual(direction[2], 'S'));
    }
    
    private bool SearchForX_MAS(char[,] matrix, (int column, int row) fromPos)
    {
        var a = new List<(int column, int row)>
        {
            (fromPos.column + 1, fromPos.row+1), (fromPos.column - 1, fromPos.row-1)
        };
        
        var b = new List<(int column, int row)>
        {
            (fromPos.column + 1, fromPos.row-1), (fromPos.column - 1, fromPos.row+1)
        };
        
        var allDirections = new List<List<(int column, int row)>>
        {
            a, b
        };

        var hits = allDirections.Count(direction => (matrix.IsEqual(direction[0], 'M') && matrix.IsEqual(direction[1], 'S')) || matrix.IsEqual(direction[0], 'S') && matrix.IsEqual(direction[1], 'M'));
        return hits == 2;
    }
    
    public override void PartOne(List<string> lines)
    {
        var matrix = MatrixExtensions.CreateMatrix(lines);
        
        int xmasHits = 0;
        for (int column = 0; column <= matrix.GetUpperBound(0); column++)
        {
            for (int row = 0; row <= matrix.GetUpperBound(1); row++)
            {
                if (matrix[column, row] == 'X')
                {
                    xmasHits += SearchForMAS(matrix, (column, row));
                }
            }
        }

        _output.WriteLine(xmasHits.ToString());
    }

    public override void PartTwo(List<string> lines)
    {
        var matrix = MatrixExtensions.CreateMatrix(lines);


        int xmasHits = 0;
        for (int column = 0; column <= matrix.GetUpperBound(0); column++)
        {
            for (int row = 0; row <= matrix.GetUpperBound(1); row++)
            {
                if (matrix[column, row] == 'A')
                {
                    if (SearchForX_MAS(matrix, (column, row)))
                    {
                        xmasHits++;
                    }
                }
            }
        }
        
        _output.WriteLine(xmasHits.ToString());
    }
}
