// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("Data/input.txt");

input = input.Select(line => string.Concat(line.Where(character => char.IsDigit(character)))).ToArray();
int[] values = input.Select(line => int.Parse(line[0].ToString()) * 10 + int.Parse(line[^1].ToString())).ToArray();

Console.WriteLine(values.Sum());