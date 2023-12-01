namespace Solutions.Y2023;

public class Day1 : AdventOfCodeTests
{
    private readonly Dictionary<string, char> _numberWords;

    public Day1(ITestOutputHelper output) : base(output)
    {
        _numberWords = new Dictionary<string, char>
        {
            {"one", '1'},
            {"two", '2'},
            {"three", '3'},
            {"four", '4'},
            {"five", '5'},
            {"six", '6'},
            {"seven", '7'},
            {"eight", '8'},
            {"nine", '9'}
        };
    }

    private int CalculateCalibrationPartOne(List<string> lines)
    {
        var current = 0;
        foreach (var line in lines)
        {
            var first = line.First(char.IsDigit);
            var last = line.Last(char.IsDigit);
            var number = int.Parse($"{first}{last}");

            current += number;
        }

        return current;
    }

    private int CalculateCalibrationPartTwo(List<string> lines)
    {
        var current = 0;
        foreach (var line in lines)
        {
            var firstDigit = line.FirstOrDefault(char.IsDigit);
            var firstIndex = line.IndexOf(firstDigit);
            foreach (var word in _numberWords)
            {
                var index = line.IndexOf(word.Key);
                if (index < firstIndex || firstIndex == -1)
                {
                    firstIndex = index;
                    firstDigit = word.Value;
                }
            }

            var lastDigit = line.LastOrDefault(char.IsDigit);
            var lastIndex = line.LastIndexOf(lastDigit);
            foreach (var word in _numberWords)
            {
                var index = line.LastIndexOf(word.Key);
                if (index > lastIndex)
                {
                    lastIndex = index;
                    lastDigit = word.Value;
                }
            }

            var number = int.Parse($"{firstDigit}{lastDigit}");

            current += number;
        }

        return current;
    }

    public override void PartOne(List<string> lines)
    {
        var calibration = CalculateCalibrationPartOne(lines);
        _output.WriteLine($"Calibration = {calibration}");
    }

    public override void PartTwo(List<string> lines)
    {
        var calibration = CalculateCalibrationPartTwo(lines);
        _output.WriteLine($"Calibration = {calibration}");
    }
}
