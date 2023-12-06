namespace AdventOfCode.Days;

public class Day05 : BaseDay
{
    private readonly List<long> _seeds;
    private readonly Dictionary<long, List<List<long>>> _input;

    public Day05()
    {
        var almanac = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine + Environment.NewLine);

        _input = [];

        _seeds = almanac[0]
            .Split(": ")[1]
            .Split(' ')
            .Select(long.Parse)
            .ToList();

        for (var i = 1; i < almanac.Length; i++)
        {
            _input.Add(i, almanac[i]
                .Split(":" + Environment.NewLine)[1]
                .Split(Environment.NewLine)
                .Select(x => x.Split(' ').Select(long.Parse).ToList())
                .ToList());
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var lowestLocationNumber = long.MaxValue;

        foreach (var seed in _seeds)
        {
            var currentMapValue = seed;

            foreach (var map in _input)
            {
                currentMapValue = GetMappedValue(currentMapValue, map.Value);
            }

            if (currentMapValue < lowestLocationNumber)
                lowestLocationNumber = currentMapValue;
        }

        return new(lowestLocationNumber.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        // lazy, brute-force approach
        // this took 4 minutes 4 seconds to run on my M2 Macbook Air

        var lowestLocationNumber = long.MaxValue;

        for (int i = 0; i < _seeds.Count; i += 2)
        {
            long seed = _seeds[i];

            for (var j = seed; j < seed + _seeds[i + 1]; j++)
            {
                var currentMapValue = j;

                foreach (var map in _input)
                    currentMapValue = GetMappedValue(currentMapValue, map.Value);

                if (currentMapValue < lowestLocationNumber)
                    lowestLocationNumber = currentMapValue;
            }
        }

        return new(lowestLocationNumber.ToString());
    }

    private static long GetMappedValue(long sourceValue, List<List<long>> map)
    {
        var mappedValue = sourceValue;

        foreach (var values in map)
        {
            if (sourceValue >= values[1] && sourceValue <= values[1] + values[2])
            {
                mappedValue = sourceValue + (values[0] - values[1]);
                break;
            }
            else
                continue;
        }

        return mappedValue;
    }
}
