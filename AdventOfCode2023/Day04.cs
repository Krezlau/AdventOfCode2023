namespace AdventOfCode2023;

public class Day04 : Day
{
    public Day04(string inputFile) : base(inputFile)
    {
    }

    public override void ExecutePartOne()
    {
        var scratchcards = Data.Select(l => new Scratchcard(l)).ToList();
        var counts = scratchcards.Select(s => s.WinningNumbers.Count(wn => s.Numbers.Contains(wn)));
        var winnings = counts.Sum(c => c == 0 ? 0 : Math.Pow(2, c-1));
        Console.WriteLine(winnings);
    }

    public override void ExecutePartTwo()
    {
        var scratchcards = Data.Select(l => new Scratchcard(l)).ToList();
        var counts = scratchcards.Select(s => s.WinningNumbers.Count(wn => s.Numbers.Contains(wn))).ToList();

        for (int i = 0; i < scratchcards.Count - 2; i++)
        {
            for (int j = i + 1; j < i + counts[i] + 1 && j < scratchcards.Count ; j++)
            {
                scratchcards[j].Count += scratchcards[i].Count;
            }
        }
        
        Console.WriteLine(scratchcards.Sum(s => s.Count));
    }

    private class Scratchcard
    {
        public int Id { get; set; }
        public List<int> WinningNumbers { get; set; } = new();
        public List<int> Numbers { get; set; } = new();

        public int Count { get; set; } = 1;

        public Scratchcard(string line)
        {
            var split = line.Split(":");
            Id = int.Parse(split[0].Split(" ")[^1]);
            var numbersPart = split[1].Split("|");
            foreach (var number in numbersPart[0].Trim().Split(" "))
            {
                if (number.Length > 0 && char.IsDigit(number[0])) WinningNumbers.Add(int.Parse(number));
            }
            foreach (var number in numbersPart[1].Trim().Split(" "))
            {
                if (number.Length > 0 && char.IsDigit(number[0])) Numbers.Add(int.Parse(number));
            }
        }
        
        public override string ToString()
        {
            return $"{Id}: {Count}";
        }
    }
}