using System.Text;

namespace Solutions;

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
    
    public static bool OutOfBounds<T>(this T[,] matrix, int column, int row)
    {
        if (column < 0 || row < 0)
            return true;

        if (column > matrix.GetUpperBound(0) || row > matrix.GetUpperBound(1))
            return true;

        return false;
    }
    
    public static int Count<T>(this T[,] grid, T target)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int hits = 0;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j].Equals(target))
                {
                    hits++;
                }
            }
        }

        return hits;
    }
    
    public static (bool, T? val) TryGet<T>(this T[,] matrix, int column, int row)
    {
        if (column < 0 || row < 0)
            return (false, default);

        if (column > matrix.GetUpperBound(0) || row > matrix.GetUpperBound(1))
            return (false, default);

        var value = matrix[column, row];
        return (true, value);
    }
    
    public static (int, int)? Find<T>(this T[,] grid, T target)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j].Equals(target))
                {
                    return (i, j);
                }
            }
        }
        
        return null;
    }
    
    public static bool IsEqual<T>(this T[,] matrix, (int column, int row) pos, T lookFor)
    {
        return matrix.IsEqual(pos.column, pos.row, lookFor);
    }

    public static bool IsEqual<T>(this T[,] matrix, int column, int row, T lookFor)
    {
        if (matrix.TryGet(column, row, out var value))
        {
            return value!.Equals(lookFor);
        }

        return false;
    }
    
    public static void TryDo<T>(this T[,] matrix, int column, int row, Action<T> action)
    {
        if (matrix.TryGet(column, row, out var sign))
        {
            action(sign);
        }
    }
    
    public static char[,] CreateMatrix(List<string> lines)
    {
        var matrix = new char[lines[0].Length, lines.Count];
        for (var index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            for (int i = 0; i < line.Length; i++)
            {
                matrix[i, index] = line[i];
            }
        }

        return matrix;
    }

    public static T[,] Create<T>(int rows, int cols, T defaultVal)
    {
        var grid = new T[rows, cols];
        for (int row = 0; row < grid.GetUpperBound(0); row++)
        {
            for (int column = 0; column < grid.GetUpperBound(1); column++)
            {
                grid[row, column] = defaultVal;
            }
        }

        return grid;
    }
    
    public static IEnumerable<StringBuilder> WriteCol<T>(this T[,] matrix)
    {
        for (int row = 0; row < matrix.GetUpperBound(1); row++)
        {
            var sb = new StringBuilder();

            for (int column = 0; column < matrix.GetUpperBound(0); column++)
            {
                sb.Append(matrix[row, column]);
            }

            yield return sb;
        }
    }

    public static IEnumerable<StringBuilder> Write<T>(this T[,] matrix)
    {
        for (int row = 0; row <= matrix.GetUpperBound(0); row++)
        {
            var sb = new StringBuilder();

            for (int column = 0; column <= matrix.GetUpperBound(1); column++)
            {
                sb.Append(matrix[column, row]);
            }

            yield return sb;
        }
    }
}
