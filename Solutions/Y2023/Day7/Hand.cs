using System.Diagnostics;

namespace Solutions.Y2023;

[DebuggerDisplay("Labels = {Labels} Value = {Value}")]
public class Hand
{
    public Hand(string labels, long bid)
    {
        Labels = labels;
        Bid = bid;

        GroupedLabels = Labels.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();
    }

    public List<IGrouping<char,char>> GroupedLabels { get; }

    public string Labels { get; init; }
    public int Value { get; set; }
    public long Bid { get; }

    public (bool, int) Jokers()
    {
        var jokers = Labels.Count(x => x == 'J');
        return (jokers > 0, jokers);
    }
}
