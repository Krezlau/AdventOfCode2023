using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode2023;

public class Day05 : Day
{
    public Day05(string inputFile) : base(inputFile)
    {
    }

    private List<long> seeds = new();
    private ConcurrentBag<long> seeds2 = new();
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
        var threads = new List<Thread>();
        var seedsData = Data[0].Split(": ")[1].Split(" ");
        for (int j = 0; j < seedsData.Length - 1; j += 2)
        {
            int xd = j;
            threads.Add(new Thread(() => part2(xd)));
            threads[^1].Start();
        }
        
        while (threads.Any(t => t.IsAlive))
        {
            Thread.Sleep(100);
        }
    }

    private void part2(int j)
    {
        List<long> seeds = new();
        var seedsData = Data[0].Split(": ")[1].Split(" ");
        var one = long.Parse(seedsData[j]);
        var two = long.Parse(seedsData[j + 1]);
        for (var x = one; x < one + two; x++)
        {
            seeds.Add(x);
        }
        foreach (var filters in _filters)
        {
            seeds = seeds.Select(s => PassThroughFilters(s, filters)).ToList();
        }
        Console.WriteLine(seeds.Min());
    }

    private void ParseData(bool secondPart = false)
    {
        if (!secondPart)
            foreach (string seed in Data[0].Split(": ")[1].Split(" "))
            {
                seeds.Add(long.Parse(seed));
            }
        else
        {
            var seedsData = Data[0].Split(": ")[1].Split(" ");
            var threads = new List<Thread>();
            for (int j = 0; j < seedsData.Length - 1; j+=2)
            {
                //multithread 
                int xd = j;
                threads.Add(new Thread(() => AddToList(seedsData.ToArray(), xd)));
                threads[^1].Start();
            }

            while (threads.Any(t => t.IsAlive))
            {
                Thread.Sleep(100);
            }
            
            Console.WriteLine(seeds2.Count);
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
        var ret = seed;
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
    
    private void AddToList(string[] seedsData, int j)
    {
        Console.WriteLine(j);
        var one = long.Parse(seedsData[j]);
        var two = long.Parse(seedsData[j + 1]);
        for (var x = one; x < one + two; x++)
        {
            seeds2.Add(x);
        }
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

        public (Range? unchanged, Range? changed, Range? unchanged2) ConvertRange(Range seed)
        {
            // split range if it is necessary
            if (seed.Start >= Source && seed.End < Source + Range)
            {
                return (null, new Range(Destination + (seed.Start - Source), Destination + (seed.End - Source)), null);
            }
            if (seed.End < Source || seed.Start >= Source + Range)
            {
                return (seed, null, null);
            }
            if (seed.Start >= Source)
            {
                return (new Range(Source+Range, seed.End), new Range(Destination + (seed.Start - Source), Destination +  Range),null);
            }
            return (new Range(seed.Start, Source - 1), new Range(Source, Destination + Range - 1), new Range(Source + Range, seed.End));
        }
    }

    private class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
        
        public Range(string valueOne, string valueTwo)
        {
            Start = long.Parse(valueOne);
            End = Start + long.Parse(valueTwo);
        }
        
        public Range(long valueOne, long valueTwo)
        {
            Start = valueOne;
            End = valueTwo;
        }
    }
}