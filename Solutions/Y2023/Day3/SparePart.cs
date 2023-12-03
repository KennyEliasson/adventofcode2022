using System.Diagnostics;

namespace Solutions.Y2023;

[DebuggerDisplay("{Number()} on Row {Row}.")]
public class SparePart
{
    public List<char> Chars { get; set; } = new();

    public int Number()
    {
        return int.Parse(string.Join("", Chars));
    }
        
    public int Row { get; set; }
    public List<int> Columns { get; set; } = new();

    public bool Match(int row, int column)
    {
        return (Columns.Contains(column) && Row == row - 1)
               ||
               (Columns.Contains(column + 1) && Row == row - 1)
               ||
               (Columns.Contains(column + 1) && Row == row)
               ||
               (Columns.Contains(column + 1) && Row == row + 1)
               ||
               (Columns.Contains(column) && Row == row + 1)
               ||
               (Columns.Contains(column - 1) && Row == row + 1)
               ||
               (Columns.Contains(column - 1) && Row == row)
               ||
               (Columns.Contains(column - 1) && Row == row - 1);
    }

    public void Add(char c, int row, int column)
    {
        Chars.Add(c);
        Row = row;
        Columns.Add(column);
    }
}