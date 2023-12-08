namespace Solutions.Y2023;

public class Day6 : AdventOfCodeTests
{

    public Day6(ITestOutputHelper output) : base(output)
    {

    }



    public override void PartOne(List<string> lines)
    {
        /*var races = new List<(int time, int distance)>()
        {
            (7, 9), (15,40), (30, 200)
        };*/

        var races = new List<(int time, long distance)>()
        {
            (42899189, 308117012911467)
        };

        var raceWins = new List<int>();
        foreach (var race in races)
        {
            var wins = 0;

            for (long i = 0; i < race.time; i++)
            {
                var speed = i;
                var left = race.time - i;


                long travel = speed * left;
                if (travel > race.distance)
                    wins++;
            }

            raceWins.Add(wins);

        }

        _output.WriteLine(string.Join(", ", raceWins));
    }

    public override void PartTwo(List<string> lines)
    {
    }
}
