namespace AdventOfCode2023;

public class Day06 : Day
{
    public Day06(string inputFile) : base(inputFile)
    {
    }

    private List<Race> _races = new();
    public override void ExecutePartOne()
    {
        ParseData();
        var results = _races.Select(r => r.FindWaysToWin());
        // multiply all results together
        long ret = 1;
        foreach (var result in results)
        {
            ret *= result;
        }
        Console.WriteLine(ret);
    }

    public override void ExecutePartTwo()
    {
        ParseData2();
        Console.WriteLine(_races[0].FindWaysToWin());
    }

    private void ParseData()
    {
        var timesLine = Data[0].Replace("     ", " ").Replace("   ", " ").Replace("  ", " ").Replace("        ", " ").Split(" ")[1..];
        var distancesLine = Data[1].Replace("     ", " ").Replace("   ", " ").Replace("  ", " ").Replace("        ", " ").Split(" ")[1..];
        for (int i = 0; i < timesLine.Length; i++)
        {
            _races.Add(new Race(int.Parse(distancesLine[i]), int.Parse(timesLine[i])));
        }
    }

    private void ParseData2()
    {
        _races = new List<Race>();
        var timesLine = Data[0].Replace(" ", "").Split(":")[1..];
        var distancesLine = Data[1].Replace(" ", "").Split(":")[1..];
        _races.Add(new Race(long.Parse(distancesLine[0]), long.Parse(timesLine[0])));
    }

    private class Race
    {
        long Distance { get; set; }
        long Duration { get; set; }

        public Race(long distance, long duration)
        {
            Distance = distance;
            Duration = duration;
        }
        
        public long FindWaysToWin()
        {
            long break1 = 0;
            for (long speed = 1; speed < Duration; speed++)
            {
                Console.WriteLine($"{speed}/{Duration}");
                long distanceTravelled = (Duration - speed) * speed;
                if (distanceTravelled > Distance)
                {
                    break1 = speed;
                    break;
                } 
            }
            long break2 = 0;
            for (long speed = Duration; speed > break1; speed--)
            {
                Console.WriteLine($"{speed}/{Duration}");
                long distanceTravelled = (Duration - speed) * speed;
                if (distanceTravelled > Distance)
                {
                    break2 = speed;
                    break;
                } 
            }

            return break2 - break1 + 1;
        }
    }
}