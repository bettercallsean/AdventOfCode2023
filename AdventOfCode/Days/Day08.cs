namespace AdventOfCode.Days;

public class Day08 : BaseDay
{
    private readonly string _directions;
    private readonly Dictionary<string, List<string>> _input;

    public Day08()
    {
        var input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine);

        _directions = input[0];

        _input = input[2..].ToDictionary(x => x[..3], x => new List<string> { x[7..10], x[12..15] });
    }

    public override ValueTask<string> Solve_1()
    {
        var count = 1;
        var currentNode = "AAA";

        for (var i = 0; i < _directions.Length; i++)
        {
            var direction = _directions[i] == 'L' ? 0 : 1;

            currentNode = _input[currentNode][direction];

            if (currentNode == "ZZZ")
                break;
            else
                count++;

            if (i == _directions.Length - 1)
                i = -1;
        }

        return new(count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var startingNodes = _input.Where(x => x.Key[2] == 'A').Select(x => x.Key).ToList();
        var stepsCount = new List<long>();

        foreach (var node in startingNodes)
        {
            var currentNode = node;
            var count = 1;

            for (var i = 0; i < _directions.Length; i++)
            {
                var direction = _directions[i] == 'L' ? 0 : 1;

                currentNode = _input[currentNode][direction];

                if (currentNode[2] == 'Z')
                {
                    stepsCount.Add(count);
                    break;
                }
                else
                    count++;

                if (i == _directions.Length - 1)
                    i = -1;
            }
        }

        var result = FindLowestCommonMultiple(stepsCount);
        return new(result.ToString());
    }

    private static long FindLowestCommonMultiple(IEnumerable<long> numbers) => numbers.Aggregate((a, b) => a / FindGreatestCommonDivisor(a, b) * b);

    private static long FindGreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
