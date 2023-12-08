namespace Solutions.Y2023;

public class Day4 : AdventOfCodeTests
{
    public Day4(ITestOutputHelper output) : base(output)
    {
    }

    public override void PartOne(List<string> lines)
    {
        var cards = ParseCard(lines);

        _output.WriteLine($"Sum = {cards.Sum(x => x.Points())}");
    }

    public override void PartTwo(List<string> lines)
    {
        var originalCards = ParseCard(lines);
        var cards = new List<Card>(originalCards);

        var sum = 0;
        var loop = true;
        int index = 1;
        while (loop)
        {
            var matchingCards = cards.Where(x => x.Index == index).ToList();

            foreach (var card in matchingCards)
            {
                var matches = card.Matches().Count();
                if (matches == 0)
                {
                    if (!cards.Any(x => x.Index == index + 1))
                    {
                        loop = false;
                        break;
                    }

                }
                for (int i = 1; i <= matches; i++)
                {
                    var copy = originalCards.FirstOrDefault(x => x.Index == index + i);
                    cards.Add(copy);
                }
            }

            sum += cards.RemoveAll(x => x.Index == index);
            index++;
        }


        _output.WriteLine($"Sum = {sum}");
    }

    private static List<Card> ParseCard(List<string> lines)
    {
        var cards = new List<Card>();
        for (var index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            var removedCardLine = line.Remove(0, line.IndexOf(":") + 2);
            var numbers = removedCardLine.Split('|');

            var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var drawnNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            cards.Add(new Card(index+1, winningNumbers, drawnNumbers));
        }

        return cards;
    }
}
