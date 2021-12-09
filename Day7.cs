using System.Collections;

public class Day7
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);

        var maxpos = data.Max();
        var mincost = int.MaxValue;
        for (int p = 0; p <= maxpos; p++)
        {
            var cost = data.Select(d => Math.Abs(d - p)).Sum();
            if (cost < mincost)
                mincost = cost;
            Console.WriteLine($"{p}: {cost} ({mincost})");
        }

        return mincost.ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var data = await ParseInput(example);

        var maxpos = data.Max();
        var mincost = int.MaxValue;
        for (int p = 0; p <= maxpos; p++)
        {
            var cost = data.Select(d =>
            {
                var n = Math.Abs(d - p);
                return (n*(n+1)/2);
            }).Sum();
            if (cost < mincost)
                mincost = cost;
            Console.WriteLine($"{p}: {cost} ({mincost})");
        }

        return mincost.ToString();
    }

    private static async Task<IEnumerable<int>> ParseInput(bool example)
    {
        var input = await File.ReadAllTextAsync($"inputs/day7{(example ? ".example" : string.Empty)}.txt");
        return input.Split(",").Select(i => int.Parse(i));
    }
}