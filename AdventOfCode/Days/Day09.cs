namespace AdventOfCode.Days;

public class Day09 : BaseDay
{
    private List<List<long>> _input;

    public Day09()
    {
        SetupInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var score = CalculateScore(false);

        return new(score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        SetupInput();

        var score = CalculateScore(true);

        return new(score.ToString());
    }

    private long CalculateScore(bool reverse)
    {
        var score = 0L;

        foreach (var item in _input.ToList())
        {
            var dataHistory = CreateDatahistory(item);

            dataHistory[^1].Insert(reverse ? 0 : dataHistory[^1].Count, 0);

            for (var i = dataHistory.Count - 2; i >= 0; i--)
            {
                var valueToInsert = reverse
                    ? dataHistory[i][0] - dataHistory[i + 1][0]
                    : dataHistory[i][^1] + dataHistory[i + 1][^1];

                dataHistory[i].Insert(reverse ? 0 : dataHistory[i].Count, valueToInsert);
            }

            score += dataHistory[0][reverse ? 0 : ^1];
        }

        return score;
    }

    private static List<List<long>> CreateDatahistory(List<long> item)
    {
        var dataHistory = new List<List<long>>
            {
                item
            };

        var currentLine = item;

        while (!currentLine.All(x => x == 0))
        {
            var newLine = new List<long>();

            for (int i = 0; i < currentLine.Count - 1; i++)
            {
                newLine.Add(currentLine[i + 1] - currentLine[i]);
            }

            dataHistory.Add(newLine);
            currentLine = newLine;
        }

        return dataHistory;
    }

    private void SetupInput()
    {
        _input = [.. File.ReadAllText(InputFilePath)
            .Trim()
            .Split(Environment.NewLine)
            .Select(x => x.Split(' ').Select(long.Parse).ToList())];
    }
}
