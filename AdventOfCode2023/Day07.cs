namespace AdventOfCode2023;

public class Day07 : Day
{
    public Day07(string inputFile) : base(inputFile)
    {
    }

    private static int Part = 1;
    
    private static readonly int FIVE_OF_A_KIND = 6;
    private static readonly int FOUR_OF_A_KIND = 5;
    private static readonly int FULL_HOUSE = 4;
    private static readonly int THREE_OF_A_KIND = 3;
    private static readonly int TWO_PAIR = 2;
    private static readonly int ONE_PAIR = 1;
    private static readonly int HIGH_CARD = 0;

    private static readonly string CARD_ORDER = "23456789TJQKA";
    private static readonly string CARD_ORDER2 = "J23456789TQKA";

    public override void ExecutePartOne()
    {
        List<Hand> hands = Data.Select(line => 
            new Hand(line.Split(" ")[0], int.Parse(line.Split(" ")[1]))).ToList();
        hands = hands.OrderBy(h => h).ToList();
        long sum = 0;
        for (int i = hands.Count; i > 0; i--)
        {
            sum += hands[i-1].Bid * i;
        }
        Console.WriteLine($"Sum of all bids: {sum}");
    }

    public override void ExecutePartTwo()
    {
        Part = 2;
        List<Hand> hands = Data.Select(line => 
            new Hand(line.Split(" ")[0], int.Parse(line.Split(" ")[1]))).ToList();
        hands = hands.OrderBy(h => h).ToList();
        foreach (var hand in hands)
        {
            Console.WriteLine(hand);
        }
        long sum = 0;
        for (int i = hands.Count; i > 0; i--)
        {
            sum += hands[i-1].Bid * i;
        }
        Console.WriteLine($"Sum of all bids: {sum}");
    }

    private class Hand : IComparable, IComparable<Hand>
    {
        public string Cards { get; set; } = string.Empty;
        public int Type { get; set; } = 0;
        public int Bid { get; set; } = 0;

        public Hand(string cards, int bid)
        {
            Cards = cards;
            if (Part == 1) Type = DetermineType(cards);
            if (Part == 2) Type = DetermineType2(cards);
            Bid = bid;
        }

        private int DetermineType(string cards)
        {
            var uniqueCards = cards.Distinct().OrderByDescending(c => cards.Count(cc => cc == c)).ToArray();
            int uniqueCardsCount = cards.Distinct().Count();
            if (uniqueCardsCount == 1) return FIVE_OF_A_KIND;
            if (uniqueCardsCount == 2 && cards.Count(c => c == uniqueCards[0]) == 4) return FOUR_OF_A_KIND;
            if (uniqueCardsCount == 2 && cards.Count(c => c == uniqueCards[0]) == 3) return FULL_HOUSE;
            if (uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) == 3) return THREE_OF_A_KIND;
            if (uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) == 2) return TWO_PAIR;
            if (uniqueCardsCount == 4) return ONE_PAIR;
            return HIGH_CARD;
        }

        private int DetermineType2(string cards)
        {
            var uniqueCards = cards.Where(c => c != 'J').Distinct().OrderByDescending(c => cards.Count(cc => cc == c)).ToArray();
            int uniqueCardsCount = cards.Distinct().Count();
            int JokerCount = cards.Count(c => c == 'J');
            if (uniqueCardsCount == 1 || (uniqueCardsCount == 2 && JokerCount > 0)) return FIVE_OF_A_KIND;
            if ((uniqueCardsCount == 2 && cards.Count(c => c == uniqueCards[0]) == 4) || 
                (uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) + JokerCount == 4 && JokerCount > 0)) return FOUR_OF_A_KIND;
            if ((uniqueCardsCount == 2 && cards.Count(c => c == uniqueCards[0]) == 3) || 
                (uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) + JokerCount == 3 && JokerCount > 0)) return FULL_HOUSE;
            if ((uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) == 3) ||
                (uniqueCardsCount == 4 && cards.Count(c => c == uniqueCards[0]) + JokerCount == 3 && JokerCount > 0)) return THREE_OF_A_KIND;
            if ((uniqueCardsCount == 3 && cards.Count(c => c == uniqueCards[0]) == 2) ||
                (uniqueCardsCount == 4 && cards.Count(c => c == uniqueCards[0]) + JokerCount == 2 && JokerCount > 0)) return TWO_PAIR;
            if (uniqueCardsCount == 4 || (uniqueCardsCount == 5 && JokerCount > 0)) return ONE_PAIR;
            return HIGH_CARD;
        }
        
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj is Hand otherHand) return CompareTo(otherHand);
            throw new ArgumentException("Object is not a Hand");
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;
            if (Type > other.Type) return 1;
            if (Type < other.Type) return -1;
            if (Type == other.Type)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Part == 1)
                    {
                        if (CARD_ORDER.IndexOf(Cards[i]) > CARD_ORDER.IndexOf(other.Cards[i])) return 1;
                        if (CARD_ORDER.IndexOf(Cards[i]) < CARD_ORDER.IndexOf(other.Cards[i])) return -1;
                    }
                    if (Part == 2)
                    {
                        if (CARD_ORDER2.IndexOf(Cards[i]) > CARD_ORDER2.IndexOf(other.Cards[i])) return 1;
                        if (CARD_ORDER2.IndexOf(Cards[i]) < CARD_ORDER2.IndexOf(other.Cards[i])) return -1;
                    }
                }
            }
            return 0;
        }
        
        public static int Compare(Hand left, Hand right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return 0;
            }
            if (left is null)
            {
                return -1;
            }
            return left.CompareTo(right);
        }
        
        public override string ToString()
        {
            return $"{Cards} ({Bid}) - {Type}";
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Hand otherHand) return CompareTo(otherHand) == 0;
            return false;
        }
        
        public override int GetHashCode()
        {
            return Cards.GetHashCode();
        }
        
        public static bool operator ==(Hand left, Hand right)
        {
            if (left is null)
            {
                return right is null;
            }
            return left.Equals(right);
        }
        public static bool operator !=(Hand left, Hand right)
        {
            return !(left == right);
        }
        public static bool operator <(Hand left, Hand right)
        {
            return (Compare(left, right) < 0);
        }
        public static bool operator >(Hand left, Hand right)
        {
            return (Compare(left, right) > 0);
        }
    }
}