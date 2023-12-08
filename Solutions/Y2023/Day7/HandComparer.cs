namespace Solutions.Y2023;

public class HandComparer : IComparer<Hand>
{
    private Dictionary<char, int> Cards {get; }
    public HandComparer(bool jokerIsLow = false)
    {
        Cards = new()
        {
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'T', 10},
            {'Q', 12},
            {'K', 13},
            {'A', 14},
            {'J', jokerIsLow ? 1 : 11}
        };
    }

    public int Compare(Hand x, Hand y)
    {
        var compare = x.Value.CompareTo(y.Value);
        if (compare == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                var compareCard = Cards[x.Labels[i]].CompareTo(Cards[y.Labels[i]]);
                if (compareCard != 0)
                {
                    return compareCard;
                }
            }
        }

        return compare;
    }

}
