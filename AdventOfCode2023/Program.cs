// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("Data/input.txt");

Console.WriteLine(
    input
        .Select(line => 
            string.Concat(line.Where(character => char.IsDigit(character))))
        .Select(line => 
            int.Parse(line[0].ToString()) * 10 + int.Parse(line[^1].ToString()))
        .Sum());

// part two
//input = "two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen".Split("\n");

Console.WriteLine(
    input
        .Select(line => string.Concat(GetDigitsFromLine(line)
                .Where(character => char.IsDigit(character))))
        .Select(line => 
                int.Parse(line[0].ToString()) * 10 + int.Parse(line[^1].ToString()))
        .Sum());

string GetDigitsFromLine(string line)
{
    string[] digits = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    for (int i = 0; i < digits.Length; i++)
        line = line.Replace(digits[i], digits[i].Substring(0, 2) + (i + 1) + digits[i].Substring(2));
    return line;
}
