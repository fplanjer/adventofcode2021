public class Day2
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day2{(example ? ".example" : string.Empty)}.txt");
        var courseSteps = input.Select(line =>
        {
            var parts = line.Split();
            return (direction: parts[0], count: int.Parse(parts[1]));
        }).ToArray();

        var foreward = courseSteps.Where(s => s.direction == "forward").Sum(s => s.count);
        var up = courseSteps.Where(s => s.direction == "up").Sum(s => s.count);
        var down = courseSteps.Where(s => s.direction == "down").Sum(s => s.count);
        var depth = down - up;

        return (foreward * depth).ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day2{(example ? ".example" : string.Empty)}.txt");
        int aim = 0;
        int position = 0;
        int depth = 0;

        input.ToList()
            .ForEach(line =>
                {
                    var parts = line.Split();
                    var count = int.Parse(parts[1]);
                    switch (parts[0])
                    {
                        case "forward":
                            position += count;
                            depth += count * aim;
                            break;
                        case "down":
                            aim += count;
                            break;
                        case "up":
                            aim -= count;
                            break;
                    }
                });

        return (position * depth).ToString();
    }
}