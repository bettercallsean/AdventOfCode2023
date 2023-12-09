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
        var score = 0L;

        foreach (var item in _input)
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

            dataHistory[^1].Add(0);

            for (var i = dataHistory.Count - 2; i >= 0; i--)
            {
                dataHistory[i].Add(dataHistory[i][^1] + dataHistory[i + 1][^1]);
            }

            score += dataHistory[0][^1];
        }

        return new(score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        SetupInput();

        var score = 0L;

        foreach (var item in _input.ToList())
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

            dataHistory[^1].Insert(0, 0);

            for (var i = dataHistory.Count - 2; i >= 0; i--)
            {
                dataHistory[i].Insert(0, dataHistory[i][0] - dataHistory[i + 1][0]);
            }

            score += dataHistory[0][0];
        }

        return new(score.ToString());
    }

    private void SetupInput()
    {
        _input = [.. File.ReadAllText(InputFilePath)
            .Trim()
            .Split(Environment.NewLine)
            .Select(x => x.Split(' ').Select(long.Parse).ToList())];
    }
}
