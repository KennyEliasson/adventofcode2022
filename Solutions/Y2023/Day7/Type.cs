namespace Solutions.Y2023;

public abstract class Type
{
    public abstract bool Match(Hand hand);
    public abstract int Result { get; }

    public bool MatchJoker(Hand hand)
    {
        var (hasJokers, jokers) = hand.Jokers();
        return hasJokers ? MatchJoker(hand, jokers) : Match(hand);
    }

    protected abstract bool MatchJoker(Hand hand, int jokers);
}

public class FiveOfAKind : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels.Count == 1;
        public override int Result => 7;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            if (jokers == 5)
                return true;

            return hand.GroupedLabels.Count == 2;
        }
    }

    public class FourOfAKind : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels[0].Count() == 4;

        public override int Result => 6;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            if (jokers == 1)
            {
                return hand.GroupedLabels[0].Count() == 3;
            }

            return hand.GroupedLabels.Count == 3;
        }
    }

    public class FullHouse : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels.Count == 2 && hand.GroupedLabels[0].Count() == 3;

        public override int Result => 5;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            return hand.GroupedLabels.Count == 3;
        }
    }

    public class ThreeOfAKind : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels[0].Count() == 3;

        public override int Result => 4;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            return hand.GroupedLabels.Count == 4;
        }
    }

    public class TwoPair : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels.Count == 3 && hand.GroupedLabels[0].Count() == 2 && hand.GroupedLabels[1].Count() == 2;

        public override int Result => 3;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            return false;
        }
    }

    public class OnePair : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels.Count == 4;

        public override int Result => 2;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            return true;
        }
    }

    public class HighCard : Type
    {
        public override bool Match(Hand hand) => hand.GroupedLabels.Count == 5;

        public override int Result => 1;

        protected override bool MatchJoker(Hand hand, int jokers)
        {
            return false;
        }
    }
