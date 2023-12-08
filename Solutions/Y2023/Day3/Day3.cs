namespace Solutions.Y2023;

public class Day3 : AdventOfCodeTests
{
    public Day3(ITestOutputHelper output) : base(output)
    {
    }

    public override void PartOne(List<string> lines)
    {
        var spareParts = CreateSpareParts(lines);
        var matrix = CreateMatrix(lines);

        int sum = 0;
        foreach (var sparePart in spareParts)
        {
            var adjacents = new List<char>();
            foreach (var column in sparePart.Columns)
            {
                matrix.TryDo(column, sparePart.Row+1, c => adjacents.Add(c));
                matrix.TryDo(column, sparePart.Row-1, c => adjacents.Add(c));
                matrix.TryDo(column+1, sparePart.Row, c => adjacents.Add(c));
                matrix.TryDo(column-1, sparePart.Row, c => adjacents.Add(c));
                matrix.TryDo(column-1, sparePart.Row-1, c => adjacents.Add(c));
                matrix.TryDo(column-1, sparePart.Row+1, c => adjacents.Add(c));
                matrix.TryDo(column+1, sparePart.Row+1, c => adjacents.Add(c));
                matrix.TryDo(column+1, sparePart.Row-1, c => adjacents.Add(c));
            }

            var hit = adjacents.Any(x => x != '.' && !char.IsNumber(x));

            if (hit)
            {
                sum += sparePart.Number();
            }
        }

        _output.WriteLine($"Sum = {sum}");
    }

    private static char[,] CreateMatrix(List<string> lines)
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

    private static List<SparePart> CreateSpareParts(IReadOnlyList<string> lines)
    {
        var spareParts = new List<SparePart>();
        for (var index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            SparePart? current = null;
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    current ??= new SparePart();
                    current.Add(line[i], index, i);
                }
                else
                {
                    if (current != null)
                    {
                        spareParts.Add(current);
                    }

                    current = null;
                }
            }

            if (current != null)
                spareParts.Add(current);
        }

        return spareParts;
    }

    public override void PartTwo(List<string> lines)
    {
        var spareParts = CreateSpareParts(lines);
        var matrix = CreateMatrix(lines);

        int sum = 0;
        for (int column = 0; column < matrix.GetUpperBound(0); column++)
        {
            for (int row = 0; row < matrix.GetUpperBound(1); row++)
            {
                if (matrix[column, row] == '*')
                {
                    var parts = spareParts.Where(x => x.Match(row, column)).ToList();

                    if (parts.Count == 2)
                    {
                        sum += parts[0].Number() * parts[1].Number();
                    }
                }
            }
        }

        _output.WriteLine($"Sum = {sum}");
    }
}
