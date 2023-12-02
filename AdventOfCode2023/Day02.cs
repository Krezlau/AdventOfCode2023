using System.Security.AccessControl;

namespace AdventOfCode2023;

public class Day02 : Day
{
    public Day02(string inputFile) : base(inputFile)
    {
    }

    public override void ExecutePartOne()
    {
        int redBalls = 12;
        int greenBalls = 13;
        int blueBalls = 14;
        var games = TransformData();
        games = games.Where(g => g.Rounds.All(r => r.Balls.All(b =>
        {
            if (b.color == "green") return b.count <= greenBalls;
            if (b.color == "red") return b.count <= redBalls;
            if (b.color == "blue") return b.count <= blueBalls;
            return true;
        }))).ToList();
        Console.WriteLine(games.Sum(g => g.Id));
    }

    public override void ExecutePartTwo()
    {
        var games = TransformData();
        int sum = 0;
        foreach (var game in games)
        {
            var maxRed = 0;
            var maxBlue = 0;
            var maxGreen = 0;
            foreach (var round in game.Rounds)
            {
                var red = round.Balls.Where(b => b.color == "red").Select(b => b.count).ToArray();
                var blue = round.Balls.Where(b => b.color == "blue").Select(b => b.count).ToArray(); 
                var green = round.Balls.Where(b => b.color == "green").Select(b => b.count).ToArray();
                if (red.Length > 0 && red.Max() > maxRed) maxRed = red.Max();
                if (blue.Length > 0 && blue.Max() > maxBlue) maxBlue = blue.Max();
                if (green.Length > 0 && green.Max() > maxGreen) maxGreen = green.Max();
            }
            sum += maxRed * maxBlue * maxGreen;
        }
        Console.WriteLine(sum);
    }
    private List<Game> TransformData()
    {
        return Data.Select(line => new Game(line)).ToList();
    }

    private class Game
    {
        public int Id { get; set; }
        public List<Round> Rounds { get; set; }

        public Game(string line)
        {
            this.Id = int.Parse(line.Split(":")[0].Split(" ")[1]);
            var roundInfo = line.Split(":")[1].Split(";");
            this.Rounds = roundInfo.Select(x => new Round(x)).ToList();
        }
    }

    private class Round
    {
        public List<(int count, string color)> Balls { get; set; }

        public Round(string info)
        {
            var balls = info.Split(",");
            this.Balls = balls.Select(x => (int.Parse(x.TrimStart().Split(" ")[0]), x.TrimStart().Split(" ")[1])).ToList();
        }
    }
}
