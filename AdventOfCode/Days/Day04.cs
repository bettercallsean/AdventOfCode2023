namespace AdventOfCode.Days;

internal class Day04 : BaseDay
{
    private readonly List<string[]> _input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath)
            .Trim()
            .Split(Environment.NewLine)
            .Select(x => x.Split(": "))
            .Select(x => x[1].Split(" | "))
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var points = 0;

        foreach (var card in _input)
        {
            var numbersOnCard = card[0].Split(' ').Where(x => x != "").Select(int.Parse).ToList();
            var ourNumbers = card[1].Split(' ').Where(x => x != "").Select(int.Parse).ToList();

            var winningNumbers = numbersOnCard.Intersect(ourNumbers).ToList();

            points += (int)Math.Pow(2, winningNumbers.Count - 1);
        }

        return new(points.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cards = new Dictionary<int, int>();

        foreach (var cardNumber in Enumerable.Range(1, _input.Count))
            cards.Add(cardNumber, 1);

        foreach (var card in cards)
        {
            var numbersOnCard = _input[card.Key - 1][0].Split(' ').Where(x => x != "").Select(int.Parse).ToList();
            var ourNumbers = _input[card.Key - 1][1].Split(' ').Where(x => x != "").Select(int.Parse).ToList();

            var winningNumbers = numbersOnCard.Intersect(ourNumbers).Count();

            for (int j = 1; j <= winningNumbers; j++)
                cards[card.Key + j] += card.Value;
        }

        return new((cards.Values.Sum()).ToString());
    }
}
