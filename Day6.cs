using System.Collections;

public class Day6
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);
        var days = 80;
        for (var day = 0; day < days; day++)
        {
            var newFish = data.Count(i => i == 0);//count numbers of 0's
            data = data.Select(i => i = (i == 0) ? 6 : i - 1).ToArray();//decrease all
            data = data.Concat(Enumerable.Repeat(8, newFish));//add new
        }
        return data.Count().ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        //changed algorith because part1 was too slow for this
        //now keeping track of the number of fish in each status
        var data = await ParseInput(example);
        var counts = new long[9];
        for (int i = 0; i < 9; i++)
        {
            counts[i] = data.Count(x => x == i);
        }
        
        var days = 256;
        for (var day = 0; day < days; day++)
        {
            var newFish = counts[0]; //the number of new fish
            for (int i = 0; i < 8; i++)
            {
                counts[i] = counts[i + 1]; //each day gets the fish from the next day
            }
            counts[6] += newFish; //fish that were 0 go to day 6
            counts[8] = newFish; // new fish go to day 8
        }

        return counts.Sum().ToString();
    }

    private static async Task<IEnumerable<int>> ParseInput(bool example)
    {
        var input = await File.ReadAllTextAsync($"inputs/day6{(example ? ".example" : string.Empty)}.txt");
        return input.Split(",").Select(i => int.Parse(i));
    }
}