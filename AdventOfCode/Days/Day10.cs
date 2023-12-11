using AdventOfCode.Utilities.Helpers;

namespace AdventOfCode.Days;
internal class Day10 : BaseDay
{
    private readonly char[][] _input;
    private readonly char[][] _loopDirection;
    private readonly (int, int) _startingPoint;

    private List<(int, int)> _loop;

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
        _loopDirection = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

        for (var i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == 'S')
                {
                    _startingPoint = (i, j);
                    break;
                }
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        _loop = [_startingPoint];

        var previousPoint = _startingPoint;
        var currentPoint = GetPointAfterStartinPoint(_startingPoint);

        var returnedToStart = false;

        while (!returnedToStart)
        {
            _loop.Add(currentPoint);

            var direction = (currentPoint.Item1 - previousPoint.Item1, currentPoint.Item2 - previousPoint.Item2);
            previousPoint = currentPoint;

            if (_input[currentPoint.Item1][currentPoint.Item2] == '-')
            {
                currentPoint = direction == (0, 1) ? (currentPoint.Item1, currentPoint.Item2 + 1) : (currentPoint.Item1, currentPoint.Item2 - 1);
            }
            else if (_input[currentPoint.Item1][currentPoint.Item2] == '|')
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = direction == (1, 0) ? 'v' : '^';
                currentPoint = direction == (1, 0) ? (currentPoint.Item1 + 1, currentPoint.Item2) : (currentPoint.Item1 - 1, currentPoint.Item2);
            }
            else if (_input[currentPoint.Item1][currentPoint.Item2] == 'F')
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = direction == (-1, 0) ? '^' : 'v';
                currentPoint = direction == (-1, 0) ? (currentPoint.Item1, currentPoint.Item2 + 1) : (currentPoint.Item1 + 1, currentPoint.Item2);
            }
            else if (_input[currentPoint.Item1][currentPoint.Item2] == 'J')
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = direction == (0, 1) ? '^' : 'v';
                currentPoint = direction == (0, 1) ? (currentPoint.Item1 - 1, currentPoint.Item2) : (currentPoint.Item1, currentPoint.Item2 - 1);
            }
            else if (_input[currentPoint.Item1][currentPoint.Item2] == '7')
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = direction == (0, 1) ? 'v' : '^';
                currentPoint = direction == (0, 1) ? (currentPoint.Item1 + 1, currentPoint.Item2) : (currentPoint.Item1, currentPoint.Item2 - 1);
            }
            else if (_input[currentPoint.Item1][currentPoint.Item2] == 'L')
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = direction == (1, 0) ? 'v' : '^';
                currentPoint = direction == (1, 0) ? (currentPoint.Item1, currentPoint.Item2 + 1) : (currentPoint.Item1 - 1, currentPoint.Item2);
            }

            if (currentPoint == _startingPoint)
            {
                _loopDirection[currentPoint.Item1][currentPoint.Item2] = _loopDirection[previousPoint.Item1][previousPoint.Item2];
                break;
            }
        }

        return new((_loop.Count / 2).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var groupedLoopCoordinates = _loop.OrderBy(x => x.Item2)
            .GroupBy(x => x.Item1)
            .OrderBy(x => x.Key)
            .ToList();

        var insideLoopCount = 0;

        foreach (var horizontalAxis in groupedLoopCoordinates)
        {
            var nonLoopCoordinatesXCoordinates = Enumerable.Range(horizontalAxis.First().Item2, (horizontalAxis.Last().Item2 - horizontalAxis.First().Item2) + 1).Except(horizontalAxis.Select(x => x.Item2));

            if (!nonLoopCoordinatesXCoordinates.Any())
                continue;

            foreach (var xAxis in nonLoopCoordinatesXCoordinates)
            {
                var xCoordinate = xAxis + 1;
                var windingNumber = 0;
                var lastLoopDirection = 'X';

                while (xCoordinate <= horizontalAxis.Last().Item2)
                {
                    if (_loop.Contains((horizontalAxis.Key, xCoordinate)) && _loopDirection[horizontalAxis.Key][xCoordinate] != lastLoopDirection)
                    {
                        if (_loopDirection[horizontalAxis.Key][xCoordinate] == '^')
                            windingNumber--;
                        else if (_loopDirection[horizontalAxis.Key][xCoordinate] == 'v')
                            windingNumber++;

                        lastLoopDirection = _loopDirection[horizontalAxis.Key][xCoordinate];
                    }

                    xCoordinate++;
                }

                if (windingNumber != 0)
                    insideLoopCount++;
            }
        }

        return new(insideLoopCount.ToString());
    }

    private (int, int) GetPointAfterStartinPoint((int, int) currentPoint)
    {
        if (ArrayHelper.IsValidCoordinate(currentPoint.Item1 - 1, currentPoint.Item2, _input) && _input[currentPoint.Item1 - 1][currentPoint.Item2] is '|' or 'F' or '7')
            return (currentPoint.Item1 - 1, currentPoint.Item2);
        else if (ArrayHelper.IsValidCoordinate(currentPoint.Item1, currentPoint.Item2 + 1, _input) && _input[currentPoint.Item1][currentPoint.Item2 + 1] is '-' or 'J' or '7')
            return (currentPoint.Item1, currentPoint.Item2 + 1);
        else if (ArrayHelper.IsValidCoordinate(currentPoint.Item1, currentPoint.Item2 - 1, _input) && _input[currentPoint.Item1][currentPoint.Item2 - 1] is '-' or 'F' or 'L')
            return (currentPoint.Item1, currentPoint.Item2 - 1);
        else if (ArrayHelper.IsValidCoordinate(currentPoint.Item1 + 1, currentPoint.Item2, _input) && _input[currentPoint.Item1 + 1][currentPoint.Item2] is '|' or 'J' or 'L')
            return (currentPoint.Item1 + 1, currentPoint.Item2);
        else
            return (int.MinValue, int.MinValue);
    }
}
