namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly List<string[]> _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath)
                    .Trim()
                    .Split(Environment.NewLine)
                    .Select(x => x.Split(": "))
                    .Select(x => x[1])
                    .Select(x => x.Split("; "))
                    .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;

        for (int i = 0; i < _input.Count; i++)
        {
            var invalidSet = false;
            var game = _input[i];

            foreach (var set in game)
            {
                foreach (var cubeCount in set.Split(", "))
                {
                    var cube = cubeCount.Split();
                    var count = int.Parse(cube[0]);
                    var colour = cube[1];

                    if ((colour != "red" || count > 12) && (colour != "green" || count > 13) && (colour != "blue" || count > 14))
                    {
                        invalidSet = true;
                        break;
                    }
                }

                if (invalidSet)
                    break;
            }

            if (!invalidSet)
                result += i + 1;
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        for (int i = 0; i < _input.Count; i++)
        {
            var game = _input[i];
            var minimumCubeNumber = new Dictionary<string, int>
                {
                    {"red", 0},
                    {"blue", 0},
                    {"green", 0}
                };

            foreach (var set in game)
            {
                foreach (var cubeCount in set.Split(", "))
                {
                    var cube = cubeCount.Split();
                    var count = int.Parse(cube[0]);
                    var colour = cube[1];

                    if (count > minimumCubeNumber[colour])
                        minimumCubeNumber[colour] = count;
                }
            }

            result += minimumCubeNumber.Values.Aggregate((a, b) => a * b); ;
        }

        return new(result.ToString());
    }
}
