
using System.Numerics;

namespace AdventOfCode2023;

public class Day03 : Day {

    public Day03(string inputFile) : base(inputFile)
    {
    }

    public override void ExecutePartOne() {
        int sum = 0;
        for (int i = 0; i < Data.Length; i++) {
            for (int j = 0; j < Data[i].Length; j++) {
                if (char.IsDigit(Data[i][j]))
                {
                    int startIndex = j;
                    int endIndex = j;
                    int number = ComposeNumber(ref startIndex, ref endIndex, i);
                    if (CheckIfValid(startIndex, endIndex, i))
                    {
                        sum += number;
                        Console.WriteLine($"Valid number: {number}");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid number: {number}");
                    }
                    j = endIndex + 1;
                }
            }
        }
        Console.WriteLine(sum);
    }

    public override void ExecutePartTwo() {
        List<Element> elements = new List<Element>();
        List<(int i, int j)> gears = new List<(int i, int j)>();
        for (int i = 0; i < Data.Length; i++) {
            for (int j = 0; j < Data[i].Length; j++) {
                if (char.IsDigit(Data[i][j]))
                {
                    int startIndex = j;
                    int endIndex = j;
                    int number = ComposeNumber(ref startIndex, ref endIndex, i);
                    elements.Add(new Element(number, i, (startIndex, endIndex)));
                    j = endIndex;
                }
                else if (Data[i][j] == '*')
                {
                    gears.Add((i, j));
                }
            }
        }
        
        long sum = 0;
        foreach (var cords in gears)
        {
            var numbers = elements.Where(e => Math.Abs(e.i - cords.i) <= 1 && (Math.Abs(e.Cords.start - cords.j) <= 1 || Math.Abs(e.Cords.end - cords.j) <= 1)).Select(e => e.Number).ToArray();
            if (numbers.Length == 2)
            {
                sum+= numbers[0] * numbers[1];
            }
        }
        Console.WriteLine(sum);

    }

    private int ComposeNumber(ref int startIndex, ref int endIndex, int i) {
        while (endIndex < Data[i].Length - 1 && char.IsDigit(Data[i][endIndex + 1])) endIndex++;
        while (startIndex > 0 && char.IsDigit(Data[i][startIndex - 1])) startIndex--;
        //Console.WriteLine($"Found number: {Data[i].Substring(startIndex, endIndex - startIndex + 1)}");
        return int.Parse(Data[i].Substring(startIndex, endIndex - startIndex + 1));
     }
    
    private bool CheckIfValid(int startIndex, int endIndex, int i)
    {
        return (startIndex < Data[i].Length - 1 && i > 0 && Data[i - 1][startIndex + 1] != '.' &&
                !char.IsDigit(Data[i - 1][startIndex + 1]))
               || (i > 0 && Data[i - 1][startIndex] != '.' && !char.IsDigit(Data[i - 1][startIndex]))
               || (i > 0 && startIndex > 0 && Data[i - 1][startIndex - 1] != '.' &&
                   !char.IsDigit(Data[i - 1][startIndex - 1]))
               || (startIndex > 0 && Data[i][startIndex - 1] != '.' && !char.IsDigit(Data[i][startIndex - 1]))
               || (startIndex > 0 && i < Data.Length - 1 && Data[i + 1][startIndex - 1] != '.' &&
                   !char.IsDigit(Data[i + 1][startIndex - 1]))
               || (i < Data.Length - 1 && Data[i + 1][startIndex] != '.' && !char.IsDigit(Data[i + 1][startIndex]))
               || (i < Data.Length - 1 && startIndex < Data[i].Length - 1 && Data[i + 1][startIndex + 1] != '.' &&
                   !char.IsDigit(Data[i + 1][startIndex + 1]))
               || (endIndex < Data[i].Length - 1 && i > 0 && Data[i - 1][endIndex + 1] != '.' &&
                   !char.IsDigit(Data[i - 1][endIndex + 1]))
               || (i > 0 && Data[i - 1][endIndex] != '.' && !char.IsDigit(Data[i - 1][endIndex]))
               || (i > 0 && endIndex > 0 && Data[i - 1][endIndex - 1] != '.' &&
                   !char.IsDigit(Data[i - 1][endIndex - 1]))
               || (endIndex < Data[i].Length - 1 && Data[i][endIndex + 1] != '.' && !char.IsDigit(Data[i][endIndex + 1]))
               || (endIndex > 0 && i < Data.Length - 1 && Data[i + 1][endIndex - 1] != '.' &&
                   !char.IsDigit(Data[i + 1][endIndex - 1]))
               || (i < Data.Length - 1 && Data[i + 1][endIndex] != '.' && !char.IsDigit(Data[i + 1][endIndex]))
               || (i < Data.Length - 1 && endIndex < Data[i].Length - 1 && Data[i + 1][endIndex + 1] != '.' &&
                   !char.IsDigit(Data[i + 1][endIndex + 1]));
    }

    private (int i, int j) CheckIfGearNearby(int startIndex, int endIndex, int i)
    {
        if (startIndex < Data[i].Length - 1 && i > 0 && Data[i - 1][startIndex + 1] == '*' )
        {
            return (i - 1, startIndex + 1);
        }

        if (i > 0 && Data[i - 1][startIndex] == '*')
        {
            return (i - 1, startIndex);
        }

        if (i > 0 && startIndex > 0 && Data[i - 1][startIndex - 1] == '*')
        {
            return (i - 1, startIndex - 1);
        }

        if (startIndex > 0 && Data[i][startIndex - 1] == '*')
        {
            return (i, startIndex - 1);
        }

        if (startIndex > 0 && i < Data.Length - 1 && Data[i + 1][startIndex - 1] == '*')
        {
            return (i + 1, startIndex - 1);
        }

        if (i < Data.Length - 1 && Data[i + 1][startIndex] == '*')
        {
            return (i + 1, startIndex);
        }

        if (i < Data.Length - 1 && startIndex < Data[i].Length - 1 && Data[i + 1][startIndex + 1] == '*')
        {
            return (i + 1, startIndex + 1);
        }

        if (endIndex < Data[i].Length - 1 && i > 0 && Data[i - 1][endIndex + 1] == '&')
        {
            return (i - 1, endIndex + 1);
        }

        if (i > 0 && Data[i - 1][endIndex] == '*')
        {
            return (i - 1, endIndex);
        }

        if (i > 0 && endIndex > 0 && Data[i - 1][endIndex - 1] == '*')
        {
            return (i - 1, endIndex - 1);
        }

        if (endIndex < Data[i].Length - 1 && Data[i][endIndex + 1] == '*')
        {
            return (i, endIndex + 1);
        }

        if (endIndex > 0 && i < Data.Length - 1 && Data[i + 1][endIndex - 1] == '*')
        {
            return (i + 1, endIndex - 1);
        }

        if (i < Data.Length - 1 && Data[i + 1][endIndex] == '*')
        {
            return (i + 1, endIndex);
        }

        if (i < Data.Length - 1 && endIndex < Data[i].Length - 1 && Data[i + 1][endIndex + 1] == '*')
        {
            return (i + 1, endIndex + 1);
        }

        return (-1, -1);
    }

    private class Element
    {
        public int Number { get; set; }
        public (int start, int end) Cords { get; set; }
        public int i { get; set; }

        public Element(int number, int i, (int start, int end) cords)
        {
            this.i = i;
            Number = number;
            Cords = cords;
        }
    }
}
