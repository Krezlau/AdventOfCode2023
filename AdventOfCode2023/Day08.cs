namespace AdventOfCode2023;

public class Day08 : Day
{
    public Day08(string inputFile) : base(inputFile)
    {
    }
    public override void ExecutePartOne()
    {
        var nodes = ParseInput();
        var instructions = Data[0];
        var current = nodes["AAA"];
        int i = 0;
        int steps = 0;
        while (current.Alias != "ZZZ")
        {
            if (instructions[i] == 'L') current = nodes[current.Left];
            else current = nodes[current.Right];

            if (i++ == instructions.Length - 1) i = 0;
            steps++; Console.WriteLine(current.Alias);
        }
        Console.WriteLine(steps);
    }

    public override void ExecutePartTwo()
    {
        var nodes = ParseInput();
        var startingPoints = nodes.Where(n => n.Key.EndsWith("A")).Select(s => s.Value).ToList();
        var instructions = Data[0];
        var stepsList = new List<long>();
        foreach (var startingPoint in startingPoints)
        {
            var i = 0;
            long steps = 0;
            var current = startingPoint;
            while (!current.Alias.EndsWith("Z"))
            {
                current = instructions[i] == 'L' ? nodes[current.Left] : nodes[current.Right];
                if (i++ == instructions.Length - 1) i = 0;
                steps++;
            }
            stepsList.Add(steps);
        }
        Console.WriteLine(stepsList.Aggregate(LCM));
    }
    
    private Dictionary<string, Node> ParseInput()
    {
        Dictionary<string, Node> nodes = new();
        foreach (string line in Data[2..])
        {
            nodes.Add(line[..3], new Node(line[..3], line[7..10], line[12..15]));
        }
        return nodes;
    }
    
    private long LCM(long a, long b) => (a * b) / GCD(a, b);
    private long GCD(long a, long b)
    {
        long remainder;

        while (b != 0)
        {
            remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }

    private class Node
    {
        public string Alias { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        
        public Node(string alias, string left, string right)
        {
            Alias = alias;
            Left = left;
            Right = right;
        }
        
        public override string ToString()
        {
            return $"{Alias}";
        }
    }
}