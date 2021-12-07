public class Day1
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day1{(example ? ".example" : string.Empty)}.txt");
        var numbers = input.Select(line => int.Parse(line)).ToArray();

        int count = 0;
        for (var i = 0; i < numbers.Length - 1; i++)
        {
            if (numbers[i] < numbers[i + 1])
                count++;
        }

        return count.ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day1{(example ? ".example" : string.Empty)}.txt");
        var numbers = input.Select(line => int.Parse(line)).ToArray();

        int count = 0;
        for (var i = 0; i < numbers.Count() - 3; i++)
        {
            var span1 = numbers.AsSpan().Slice(i, 3).ToArray().Sum(e => e);
            var span2 = numbers.AsSpan().Slice(i + 1, 3).ToArray().Sum(e => e);
            if (span1 < span2) count++;
        }

        return count.ToString();
    }
}