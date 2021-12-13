public class Day11
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);

        int flashCount = 0;
        for (int step = 0; step < 100; step++)
        {
            for (int x = 0; x < data.Length; x++)
                for (int y = 0; y < data[x].Length; y++)
                {
                    flashCount += IncreaseAndFlash(data, x, y);
                }
            ResetEnergy(data);
        }


        return flashCount.ToString();
    }

    private static int IncreaseAndFlash(int[][] data, int x, int y)
    {
        int flashCount = 0;
        if ((x < 0 || x >= data.Length) || (y < 0 || y >= data[x].Length))
            return flashCount;

        data[x][y]++;

        if (data[x][y] == 10)
        {
            flashCount++;
            flashCount += IncreaseAndFlash(data, x - 1, y - 1);
            flashCount += IncreaseAndFlash(data, x - 1, y);
            flashCount += IncreaseAndFlash(data, x - 1, y + 1);
            flashCount += IncreaseAndFlash(data, x, y - 1);
            flashCount += IncreaseAndFlash(data, x, y + 1);
            flashCount += IncreaseAndFlash(data, x + 1, y - 1);
            flashCount += IncreaseAndFlash(data, x + 1, y);
            flashCount += IncreaseAndFlash(data, x + 1, y + 1);
        }
        return flashCount;
    }

    private static void ResetEnergy(int[][] data)
    {
        for (int x = 0; x < data.Length; x++)
        {
            for (int y = 0; y < data[x].Length; y++)
            {
                if (data[x][y] >= 10)
                    data[x][y] = 0;
            }
        }
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var data = await ParseInput(example);

        int flashCount = 0;
        var step = 0;
        while (true)
        {
            for (int x = 0; x < data.Length; x++)
                for (int y = 0; y < data[x].Length; y++)
                {
                    flashCount += IncreaseAndFlash(data, x, y);
                }
            ResetEnergy(data);
            if (data.All(d => d.All(n => n == 0))) return (step + 1).ToString();
            step++;
        }
    }

    private static async Task<int[][]> ParseInput(bool example)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day11{(example ? ".example" : string.Empty)}.txt");
        var data = input.Select(line => line.Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();
        return data;
    }
}