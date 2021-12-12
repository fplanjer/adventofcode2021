public class Day10
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);
        var score = data.Select(l => getCorruptChar(l)).Select(c => GetScore(c));


        return score.Sum().ToString();
    }


    public static async Task<string> Part2Async(bool example = false)
    {
        var data = await ParseInput(example);
        var score = data.Select(l => returnCompleteLineScore(l)).Where(s => s != 0).OrderBy(s => s).ToList();
        return score.Skip(score.Count / 2).Take(1).Single().ToString();
    }

    private static int GetScore(char c)
    {
        return c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0
        };
    }

    private static readonly char[] openChars = new char[] { '(', '[', '{', '<' };

    private static readonly char[] closeChars = new char[] { ')', ']', '}', '>' };
    private static char getCorruptChar(List<char> line)
    {
        var stack = new Stack<char>();
        foreach (var c in line)
        {
            if (openChars.Contains(c))
            {
                stack.Push(c);
            }
            else
            {
                var popC = stack.Pop();
                if (closeChars[Array.IndexOf(openChars, popC)] != c)
                    return c;
            }
        }

        return ' ';
    }


    private static long returnCompleteLineScore(List<char> line)
    {
        var stack = new Stack<char>();
        foreach (var c in line)
        {
            if (openChars.Contains(c))
            {
                stack.Push(c);
            }
            else
            {
                var popC = stack.Pop();
                if (closeChars[Array.IndexOf(openChars, popC)] != c)
                    return 0;
            }
        }
        long score = 0;
        //if stuff is left on the stack, complete te line
        while (stack.TryPop(out char popC))
        {
            var index = Array.IndexOf(openChars, popC);
            Console.Write(closeChars[index]);
            score = (score * 5) + (index + 1);
        }
        Console.WriteLine($"  {score}");
        return score;
    }

    private static async Task<List<List<char>>> ParseInput(bool example)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day10{(example ? ".example" : string.Empty)}.txt");
        var data = input.Select(line => line.ToCharArray().ToList()).ToList();
        return data;

    }
}