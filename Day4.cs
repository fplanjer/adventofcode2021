using System.Collections;

public class Day4
{

    class Board
    {
        public Board(int index, List<string> lines)
        {
            Index = index;
            var input = lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList());
            Lists = input.ToList();
            for (int i = 0; i < 5; i++)
            {
                Lists.Add(input.Select(l => l[i]).ToList());
            }
        }

        public int Index { get; set; }
        public List<List<int>> Lists { get; set; } = new List<List<int>>();

        public void RemoveNumber(int number)
        {
            Lists.ForEach(l => l.RemoveAll(n => n == number));
        }

        //board is a winner if any of the list has no more numbers
        public bool IsWinner => Lists.Any(l => !l.Any());

        public int Score => Lists.Take(5).Sum(l => l.Sum(n => n));
    }

    public static async Task<string> Part1Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/{nameof(Day4).ToLower()}{(example ? ".example" : string.Empty)}.txt");
        var numbersDrawn = input.First().Split(',').Select(int.Parse).ToList();

        var boards = input.Skip(1)
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / 6)
            .Select(x => x.Select(v => v.Value).Skip(1).ToList())
            .Select((l, i) => new Board(i, l))
            .ToList();

        foreach (var number in numbersDrawn)
        {
            boards.ForEach(b => b.RemoveNumber(number));
            var winner = boards.Where(b => b.IsWinner);
            if (winner.Any())
                return (winner.First().Score * number).ToString();
        }
        //33462
        return "No winner";
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/{nameof(Day4).ToLower()}{(example ? ".example" : string.Empty)}.txt");
        var numbersDrawn = input.First().Split(',').Select(int.Parse).ToList();

        var boards = input.Skip(1)
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / 6)
            .Select(x => x.Select(v => v.Value).Skip(1).ToList())
            .Select((l, i) => new Board(i, l))
            .ToList();

        IEnumerable<Board>? winners = null;
        int lastNumber=0;
        foreach (var number in numbersDrawn)
        {
            lastNumber=number;
            boards.ForEach(b => b.RemoveNumber(number));
            winners = boards.Where(b => b.IsWinner).ToList();
            boards.RemoveAll(b => b.IsWinner);
            if (boards.Count() == 0)
                break;
        }
        if (winners != null && winners.Any())
            return (winners.First().Score * lastNumber).ToString();


        //33462
        return "No winner";

    }
}