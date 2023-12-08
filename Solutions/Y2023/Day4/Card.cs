namespace Solutions.Y2023;

public record Card(int Index, List<int> WinningNumbers, List<int> DrawnNumbers)
{
    public int Points()
    {
        var matches = Matches();
        if (!matches.Any())
            return 0;

        return(int) Math.Pow(2, matches.Count() - 1);
    }

    public IEnumerable<int> Matches()
    {
        return DrawnNumbers.Intersect(WinningNumbers);
    }
}
