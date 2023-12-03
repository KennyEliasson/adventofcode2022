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
    
    public static void TryDo<T>(this T[,] matrix, int column, int row, Action<T> action)
    {
        if (matrix.TryGet(column, row, out var sign))
        {
            action(sign);
        }
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

    public static IEnumerable<StringBuilder> Write<T>(this T[,] matrix)
    {
        for (int row = 0; row < matrix.GetUpperBound(0); row++)
        {
            var sb = new StringBuilder();

            for (int column = 0; column < matrix.GetUpperBound(1); column++)
            {
                sb.Append(matrix[row, column]);
            }

            yield return sb;
        }
    }
}
