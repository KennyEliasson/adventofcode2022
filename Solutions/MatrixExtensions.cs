﻿using System.Text;

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