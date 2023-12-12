using System.Data;

namespace AdventOfCode.Days;

public class Day11 : BaseDay
{
    private readonly List<(int, int)> _galaxies;

    private Dictionary<int, int> _columnOffsets;
    private Dictionary<int, int> _rowOffsets;

    public Day11()
    {
        var input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine).Select(x => x.ToCharArray().ToList()).ToList();

        _galaxies = [];

        var emptyRows = new List<int>();
        var emptyColumns = Enumerable.Range(0, input[0].Count).ToList();
        var galaxies = new List<(int, int)>();

        for (var i = 0; i < input.Count; i++)
        {
            var galaxyOnRow = false;
            for (var j = 0; j < input[0].Count; j++)
            {
                if (input[i][j] == '#')
                {
                    galaxyOnRow = true;
                    emptyColumns.Remove(j);
                    galaxies.Add((i, j));
                }
            }

            if (!galaxyOnRow)
                emptyRows.Add(i);
        }

        _rowOffsets = CalculateOffsets(input, emptyRows);
        _columnOffsets = CalculateOffsets(input, emptyColumns);
        _galaxies = galaxies;
    }

    public override ValueTask<string> Solve_1()
    {
        const int Scale = 2;
        var score = GetShortestDistanceScore(Scale);

        return new(score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        const int Scale = 1000000;
        var score = GetShortestDistanceScore(Scale);

        return new(score.ToString());
    }

    private static Dictionary<int, int> CalculateOffsets(List<List<char>> input, List<int> emptyRows)
    {
        var offset = 1;
        var offsets = Enumerable.Range(0, input.Count).ToDictionary(x => x, x => 0);

        for (var i = 0; i < offsets.Count; i++)
        {
            if (i <= emptyRows[offset - 1])
                continue;
            else if (offset < emptyRows.Count && i == emptyRows[offset])
                offset++;

            offsets[i] += offset;
        }

        return offsets;
    }

    private long GetShortestDistanceScore(int expansionScale)
    {
        var offset = 1;
        var score = 0L;

        for (var i = 0; i < _galaxies.Count; i++)
        {
            for (var j = offset; j < _galaxies.Count; j++)
            {
                var x = Math.Abs(_galaxies[i].Item1 + _rowOffsets[_galaxies[i].Item1] * (expansionScale - 1) - (_galaxies[j].Item1 + _rowOffsets[_galaxies[j].Item1] * (expansionScale - 1)));
                var y = Math.Abs(_galaxies[i].Item2 + _columnOffsets[_galaxies[i].Item2] * (expansionScale - 1) - (_galaxies[j].Item2 + _columnOffsets[_galaxies[j].Item2] * (expansionScale - 1)));
                score += x + y;
            }

            offset++;
        }

        return score;
    }
}
