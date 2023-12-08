namespace Solutions.Y2023;

public class Day7 : AdventOfCodeTests
{
    private readonly List<Type> _types = new()
    {
        new FiveOfAKind(),
        new FourOfAKind(),
        new FullHouse(),
        new ThreeOfAKind(),
        new TwoPair(),
        new OnePair(),
        new HighCard()
    };

    public Day7(ITestOutputHelper output) : base(output)
    { }

    public override void PartOne(List<string> lines)
    {
        var hands = CreateHands(lines);

        foreach (var hand in hands)
        {
            foreach (var type in _types)
            {
                if (type.Match(hand))
                {
                    hand.Value = type.Result;
                    break;
                }
            }
        }

        var comparer = new HandComparer();
        var orderedHands = hands.Order(comparer).ToList();

        _output.WriteLine($"Sum is {orderedHands.Select((t, i) => t.Bid * (i + 1)).Sum()}");
    }

    public override void PartTwo(List<string> lines)
    {
        var hands = CreateHands(lines);

        foreach (var hand in hands)
        {
            foreach (var type in _types)
            {
                if (type.MatchJoker(hand))
                {
                    hand.Value = type.Result;
                    break;
                }
            }
        }

        var comparer = new HandComparer(jokerIsLow: true);
        var orderedHands = hands.Order(comparer).ToList();

        _output.WriteLine($"Sum is {orderedHands.Select((t, i) => t.Bid * (i + 1)).Sum()}");
    }

    private static List<Hand> CreateHands(List<string> lines)
    {
        var hands = new List<Hand>();
        foreach (var line in lines)
        {
            var handData = line.Split(' ');

            var hand = new Hand(handData[0], long.Parse(handData[1]));
            hands.Add(hand);
        }

        return hands;
    }

}
