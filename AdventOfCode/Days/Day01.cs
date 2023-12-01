using System.Text;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly List<string> _input;
    private readonly Dictionary<string, string> _wordToInt = new()
    {
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "th3ee" },
        { "four", "fo4r" },
        { "five", "fi5e" },
        { "six", "s6x" },
        { "seven", "se7en" },
        { "eight", "ei8ht" },
        { "nine", "ni9e" }
    };

    public Day01()
    {
        _input = [.. File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine)];
    }

    public override ValueTask<string> Solve_1() => new(_input.Select(x => x.Where(x => char.IsDigit(x)).ToList())
                                                            .Select(x => string.Concat(x[0], x[^1]))
                                                            .Select(int.Parse)
                                                            .Sum()
                                                            .ToString());

    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        foreach (var calibrationValue in _input)
        {
            var stringBuilder = new StringBuilder(calibrationValue);

            foreach (var value in _wordToInt)
            {
                stringBuilder.Replace(value.Key, value.Value);
            }

            var values = stringBuilder.ToString()
                .Where(char.IsDigit).ToList();

            result += int.Parse(string.Concat(values[0], values[^1]));
        }

        return new(result.ToString());
    }
}
