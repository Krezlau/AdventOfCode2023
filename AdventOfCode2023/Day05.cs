namespace AdventOfCode2023;

public class Day05 : Day
{
    public Day05(string inputFile) : base(inputFile)
    {
    }

    private List<long> seeds = new();
    private List<List<Mapping>> _filters = new();
    public override void ExecutePartOne()
    {
        ParseData();
        foreach (var filters in _filters)
        {
            seeds = seeds.Select(s => PassThroughFilters(s, filters)).ToList();
            Console.WriteLine(string.Join(" ", seeds));
        }
        
        Console.WriteLine(seeds.Min());
    }

    public override void ExecutePartTwo()
    {
        throw new NotImplementedException();
    }

    private void ParseData()
    {
        foreach (string seed in Data[0].Split(": ")[1].Split(" "))
        {
            seeds.Add(long.Parse(seed));
        }
        
        int i = -1;
        foreach (var line in Data[1..])
        {
            if (line == "")
            {
                _filters.Add(new List<Mapping>());
                i++;
                continue;
            }
            if (char.IsDigit(line[0]))
            {
                _filters[i].Add(new Mapping(line));
            }
        }
    }
    
    private long PassThroughFilters(long seed, List<Mapping> filters)
    {
        long ret = seed;
        foreach (var filter in filters)
        {
            ret = filter.ConvertSeed(seed);
            if (ret != seed)
            {
                return ret;
            }
        }
        return seed;
    }

    private class Mapping
    {
        public long Source { get; set; }
        public long Destination { get; set; }
        public long Range { get; set; }
        
        public Mapping(string line)
        {
            var x = line.Split(" ");
            Destination = long.Parse(x[0]);
            Source = long.Parse(x[1]);
            Range = long.Parse(x[2]);
        }
        
        public long ConvertSeed(long seed)
        {
            if (seed >= Source && seed < Source + Range)
            {
                return Destination + (seed - Source);
            }
            return seed;
        }
    }
}