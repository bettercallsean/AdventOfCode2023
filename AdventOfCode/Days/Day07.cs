namespace AdventOfCode.Days;

public class Day07 : BaseDay
{
    private readonly Dictionary<string, int> _input;
    private readonly Dictionary<string, int> _handPriorities = new()
    {
        {"5", 7},
        {"41", 6},
        {"32", 5},
        {"311", 4},
        {"221", 3},
        {"2111", 2},
        {"11111", 1}
    };

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine).Select(x => x.Split(' ')).ToDictionary(x => x[0], x => int.Parse(x[1]));
    }

    public override ValueTask<string> Solve_1()
    {
        var cardPriorities = new Dictionary<char, int>
        {
            {'A', 13},
            {'K', 12},
            {'Q', 11},
            {'J', 10},
            {'T', 9},
            {'9', 8},
            {'8', 7},
            {'7', 6},
            {'6', 5},
            {'5', 4},
            {'4', 3},
            {'3', 2},
            {'2', 1}
        };
        var orderedHands = new List<(string, int, int)>();

        foreach (var hand in _input)
        {
            var handCount = string.Join(string.Empty, hand.Key.GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x));
            orderedHands.Add((hand.Key, hand.Value, _handPriorities[handCount]));
        }

        var cards = new List<int>();
        foreach (var item in orderedHands.OrderBy(x => x.Item3).GroupBy(x => x.Item3))
        {
            var groupedHands = item.ToList();

            if (groupedHands.Count == 1)
            {
                cards.AddRange(groupedHands.Select(x => x.Item2));
                continue;
            }

            for (var i = 0; i < groupedHands.Count - 1; i++)
            {
                var swapped = false;

                for (var j = 0; j < groupedHands.Count - 1; j++)
                {
                    var ordered = false;

                    for (var k = 0; k < 5; k++)
                    {
                        if (groupedHands[j].Item1[k] != groupedHands[j + 1].Item1[k])
                        {
                            ordered = cardPriorities[groupedHands[j].Item1[k]] < cardPriorities[groupedHands[j + 1].Item1[k]];
                            break;
                        }
                    }

                    if (!ordered)
                    {
                        (groupedHands[j], groupedHands[j + 1]) = (groupedHands[j + 1], groupedHands[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }

            cards.AddRange(groupedHands.Select(x => x.Item2));
        }

        var score = 0;
        for (var i = 0; i < cards.Count; i++)
        {
            score += cards[i] * (i + 1);
        }

        return new(score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cardPriorities = new Dictionary<char, int>
        {
            {'A', 13},
            {'K', 12},
            {'Q', 11},
            {'T', 10},
            {'9', 9},
            {'8', 8},
            {'7', 7},
            {'6', 6},
            {'5', 5},
            {'4', 4},
            {'3', 3},
            {'2', 2},
            {'J', 1},
        };

        var orderedHands = new List<(string, int, int)>();

        foreach (var hand in _input)
        {
            var foo = hand.Key.GroupBy(x => x).ToDictionary(x => x.First(), x => x.Count());

            if (foo.TryGetValue('J', out int value) && foo['J'] != 5)
            {
                var key = foo.Where(x => x.Key != 'J').MaxBy(kvp => kvp.Value).Key;
                foo[key] += value;
                foo.Remove('J');
            }

            var handCount = string.Join(string.Empty, foo.OrderByDescending(x => x.Value).Select(x => x.Value));
            orderedHands.Add((hand.Key, hand.Value, _handPriorities[handCount]));
        }

        var cards = new List<int>();
        foreach (var item in orderedHands.OrderBy(x => x.Item3).GroupBy(x => x.Item3))
        {
            var groupedHands = item.ToList();

            if (groupedHands.Count == 1)
            {
                cards.AddRange(groupedHands.Select(x => x.Item2));
                continue;
            }

            for (var i = 0; i < groupedHands.Count - 1; i++)
            {
                var swapped = false;

                for (var j = 0; j < groupedHands.Count - 1; j++)
                {
                    var ordered = false;

                    for (var k = 0; k < 5; k++)
                    {
                        if (groupedHands[j].Item1[k] != groupedHands[j + 1].Item1[k])
                        {
                            ordered = cardPriorities[groupedHands[j].Item1[k]] < cardPriorities[groupedHands[j + 1].Item1[k]];
                            break;
                        }
                    }

                    if (!ordered)
                    {
                        (groupedHands[j], groupedHands[j + 1]) = (groupedHands[j + 1], groupedHands[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }

            cards.AddRange(groupedHands.Select(x => x.Item2));
        }

        var score = 0;
        for (var i = 0; i < cards.Count; i++)
        {
            score += cards[i] * (i + 1);
        }

        return new(score.ToString());
    }
}
