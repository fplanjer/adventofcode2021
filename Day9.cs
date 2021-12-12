public class Day9
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);
        var count = 0;
        for (var i = 0; i <= data.Length - 1; i++)
        {
            for (var j = 0; j <= data[i].Length - 1; j++)
            {
                if ((i <= 0 || data[i][j] < data[i - 1][j])
                && (j <= 0 || data[i][j] < data[i][j - 1])
                && (i >= data.Length - 1 || data[i][j] < data[i + 1][j])
                && (j >= data[i].Length - 1 || data[i][j] < data[i][j + 1])
                )
                {
                    Console.WriteLine($"{i},{j} = {data[i][j]}");
                    count += data[i][j] + 1;
                }
            }
        }

        return count.ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var data = await ParseInput(example);

        //loop until a 9 or the edge
        for (var x = 0; x <= data.Length - 1; x++)
        {
            for (var y = 0; y <= data[x].Length - 1; y++)
            {
                if (data[x][y] != 9 && !IsPartOfBasin((x, y)))
                {
                    var newBasin = new List<(int x, int y)>() { };
                    basins.Add(newBasin);
                    DiscoverBasin(data, newBasin, (x, y));
                }
                //find all 

            }
        }
        var total = 1;
        basins.OrderByDescending(b => b.Count()).ToList().Take(3).ToList().ForEach(n => total *= n.Count());
        return total.ToString();
    }

    private static void DiscoverBasin(int[][] data, List<(int x, int y)> basin, (int x, int y) coord)
    {
        if ((coord.x >= 0 && coord.x < data.Length)
        && (coord.y >= 0 && coord.y < data[coord.x].Length)
        && data[coord.x][coord.y] != 9 && !IsPartOfBasin(coord))
        {
            basin.Add(coord);
            DiscoverBasin(data, basin, (coord.x - 1, coord.y));
            DiscoverBasin(data, basin, (coord.x, coord.y - 1));
            DiscoverBasin(data, basin, (coord.x + 1, coord.y));
            DiscoverBasin(data, basin, (coord.x, coord.y + 1));
        }
    }

    static List<List<(int x, int y)>> basins = new List<List<(int x, int y)>>();
    private static bool IsPartOfBasin((int x, int y) coord)
    {
        return basins.Any(b => b.Contains(coord));
    }

    private static async Task<int[][]> ParseInput(bool example)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day9{(example ? ".example" : string.Empty)}.txt");
        var data = input.Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        return data;
        // var res = new int[data.Length, data.Max(x => x.Length)];
        // for (var i = 0; i < data.Length; ++i)
        // {
        //     for (var j = 0; j < data[i].Length; ++j)
        //     {
        //         res[i, j] = data[i][j];
        //     }
        // }

        // return res;

    }
}