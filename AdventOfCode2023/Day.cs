namespace AdventOfCode2023;

public abstract class Day
{
    protected readonly string[] Data;
    protected Day(string inputFile)
    {
        Data = ReadData(inputFile);
    }
    protected string[] ReadData(string inputFile)
    {
        return File.ReadAllLines($"Data/{inputFile}");
    }
    public abstract void ExecutePartOne();
    public abstract void ExecutePartTwo();
}