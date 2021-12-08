using System.Collections;

public class Day5
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var lines = await ParseInput(example);
        var grid = CreateGrid(lines, false, out (int x, int y) maxValues);

        return CountDoubleOrMore(grid, maxValues).ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var lines = await ParseInput(example);
        var grid = CreateGrid(lines, true, out (int x, int y) maxValues);

       return CountDoubleOrMore(grid, maxValues).ToString();
    }

    private static int CountDoubleOrMore(int[,] grid, (int x, int y) maxValues)
    {
        var counter = 0;
        for (var x = 0; x <= maxValues.x; x++)
        {
            for (var y = 0; y <= maxValues.y; y++)
            {
                if (grid[x, y] >= 2)
                    counter++;
            }
        }
        return counter;
    }
    
    private static void ShowGrid(int[,] grid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write($"{grid[x, y]} ");
            }
            Console.WriteLine();
        }
    }

    private static int[,] CreateGrid(IEnumerable<((int x, int y) start, (int x, int y) end)> lines, bool includeDiagonal, out (int maxX, int maxY) maxValues)
    {
        var maxXAll = lines.Select(d => d.start.x).Concat(lines.Select(d => d.end.x)).Max();
        var maxYAll = lines.Select(d => d.start.y).Concat(lines.Select(d => d.end.y)).Max();

        var grid = new int[maxXAll + 1, maxYAll + 1];

        foreach (var line in lines)
        {
            if ((line.start.x == line.end.x) || (line.start.y == line.end.y)) //horizontal or vetical
            {
                var minX = Math.Min(line.start.x, line.end.x);
                var maxX = Math.Max(line.start.x, line.end.x);
                var minY = Math.Min(line.start.y, line.end.y);
                var maxY = Math.Max(line.start.y, line.end.y);
                for (var x = minX; x <= maxX; x++)
                {
                    for (var y = minY; y <= maxY; y++)
                    {
                        grid[x, y]++;
                    }
                }
            }
            else if (includeDiagonal)
            { //diagonal
                var xDelta = line.end.x.CompareTo(line.start.x);
                var yDelta = line.end.y.CompareTo(line.start.y);
                var x = line.start.x;
                var y = line.start.y;
                for (int i = 0; i <= Math.Abs(line.start.x - line.end.x); i++)
                {
                    grid[x, y]++;
                    y += yDelta;
                    x += xDelta;
                }
            }
        }
        maxValues = (maxXAll, maxYAll);
        return grid;
        //8362  too low
        //19374
    }

    private static async Task<IEnumerable<((int x, int y) start, (int x, int y) end)>> ParseInput(bool example)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day5{(example ? ".example" : string.Empty)}.txt");
        return input.Select(ToStartEnd);

        ((int x, int y) start, (int x, int y) end) ToStartEnd(string s)
        {
            var parts = s.Split("->");
            return (start: ToTuple(parts[0]), end: ToTuple(parts[1]));
        }

        (int x, int y) ToTuple(string s)
        {
            var parts = s.Split(",");
            return (x: int.Parse(parts[0]), y: int.Parse(parts[1]));
        }
    }
}