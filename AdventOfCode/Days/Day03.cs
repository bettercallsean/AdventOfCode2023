using System.Text;
using AdventOfCode.Utilities.Helpers;

namespace AdventOfCode.Days;

internal class Day03 : BaseDay
{
    private readonly char[][] _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var partNumbers = 0;

        for (var i = 0; i < _input.Length; i++)
        {
            var line = _input[i];
            var number = new StringBuilder();
            var numberTouchingSymbol = false;

            for (int j = 0; j < line.Length; j++)
            {
                if (char.IsDigit(line[j]))
                {
                    number.Append(line[j]);

                    foreach (var value in ArrayHelper.GetSurroundingValues(i, j, _input))
                    {
                        if (_input[value.Item1][value.Item2] != '.' && !char.IsDigit(_input[value.Item1][value.Item2]))
                        {
                            numberTouchingSymbol = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (numberTouchingSymbol)
                        partNumbers += int.Parse(number.ToString());

                    if (number.Length > 0)
                        number.Clear();

                    numberTouchingSymbol = false;
                }
            }
        }

        return new(partNumbers.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var gears = new Dictionary<(int, int), List<int>>();
        for (var i = 0; i < _input.Length; i++)
        {
            var line = _input[i];
            var number = new StringBuilder();
            var numberTouchingGear = false;
            var gearCoordinate = (0, 0);

            for (int j = 0; j < line.Length; j++)
            {
                if (char.IsDigit(line[j]))
                {
                    number.Append(line[j]);

                    foreach (var value in ArrayHelper.GetSurroundingValues(i, j, _input))
                    {
                        if (_input[value.Item1][value.Item2] == '*')
                        {
                            numberTouchingGear = true;

                            if (!gears.ContainsKey(value))
                                gears.Add(value, []);

                            gearCoordinate = value;

                            break;
                        }
                    }
                }
                else
                {
                    if (numberTouchingGear)
                        gears[gearCoordinate].Add(int.Parse(number.ToString()));

                    if (number.Length > 0)
                        number.Clear();

                    numberTouchingGear = false;
                }
            }
        }

        return new(gears.Where(x => x.Value.Count == 2).Sum(x => x.Value[0] * x.Value[1]).ToString());
    }
}
