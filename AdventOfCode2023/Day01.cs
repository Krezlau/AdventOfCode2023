namespace AdventOfCode2023;

public class Day01 : Day
{
    public Day01(string inputFile) : base(inputFile)
    {
    }

    public override void ExecutePartOne()
    {
        Console.WriteLine(
            Data
                .Select(line => 
                    string.Concat(line.Where(character => char.IsDigit(character))))
                .Select(line => 
                    int.Parse(line[0].ToString()) * 10 + int.Parse(line[^1].ToString()))
                .Sum());
    }

    public override void ExecutePartTwo()
    {
        Console.WriteLine(
            Data
                .Select(line => string.Concat(GetDigitsFromLine(line)
                        .Where(character => char.IsDigit(character))))
                .Select(line => 
                        int.Parse(line[0].ToString()) * 10 + int.Parse(line[^1].ToString()))
                .Sum());
    }
    
    private string GetDigitsFromLine(string line)
    {
        string[] digits = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        for (int i = 0; i < digits.Length; i++)
            line = line.Replace(digits[i], digits[i].Substring(0, 2) + (i + 1) + digits[i].Substring(2));
        return line;
    }
}