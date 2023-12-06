namespace AdventOfCode.Days;
internal class Day06 : BaseDay
{
    private readonly List<int> _raceTime;
    private readonly List<int> _raceDistance;

    public Day06()
    {
        var input = File.ReadAllText(InputFilePath).Trim().Split(Environment.NewLine);

        _raceTime = input[0].Split(": ")[1]
            .Split(' ')
            .Where(x => x != string.Empty)
            .Select(int.Parse)
            .ToList();

        _raceDistance = input[1].Split(": ")[1]
            .Split(' ')
            .Where(x => x != string.Empty)
            .Select(int.Parse)
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var waysToWinRace = new List<int>();

        for (int i = 0; i < _raceTime.Count; i++)
        {
            var raceTime = _raceTime[i];
            var raceDistance = _raceDistance[i];

            var recordBroken = false;
            var chargeTime = 1;

            while (!recordBroken)
            {
                var distance = chargeTime * (raceTime - chargeTime);

                if (distance > raceDistance)
                    recordBroken = true;
                else
                    chargeTime++;
            }

            waysToWinRace.Add((raceTime - chargeTime + 1) - chargeTime);
        }

        return new(waysToWinRace.Aggregate((a, b) => a * b).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var raceTime = int.Parse(string.Join("", _raceTime));
        var raceDistance = long.Parse(string.Join("", _raceDistance));

        long lowestChargeTime = 0;
        long highestChargeTime = raceTime / 2;
        long lastChargeTime = 0;

        while (true)
        {
            long chargeTime = lowestChargeTime + (highestChargeTime - lowestChargeTime) / 2;

            if (chargeTime == lastChargeTime)
                break;
            else
                lastChargeTime = chargeTime;

            var distance = chargeTime * (raceTime - chargeTime);

            if (distance > raceDistance)
                highestChargeTime = chargeTime - 1;
            else if (distance < raceDistance)
                lowestChargeTime = chargeTime + 1;
        }

        return new(((raceTime - lastChargeTime + 1) - lastChargeTime).ToString());
    }
}