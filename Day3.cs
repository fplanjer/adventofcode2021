using System.Collections;

public class Day3
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/{nameof(Day3).ToLower()}{(example ? ".example" : string.Empty)}.txt");
        var lineCount = input.Length;
        var bytes = input.Select(line => line.ToCharArray().Select(a => a - '0').ToArray());  //convert to array of 0's and 1's

        var bitCount = bytes.First().Length;
        var gammaBools = new bool[bitCount];
        gammaBools = gammaBools.Select((value, index) => bytes.Sum(b => b[index]) >= lineCount / 2).ToArray();

        var gammaBits = new BitArray(gammaBools.Reverse().ToArray());
        var epsilonBits = ((BitArray)gammaBits.Clone()).Not();

        byte[] numberBytes = new byte[sizeof(int)];
        gammaBits.CopyTo(numberBytes, 0);
        int gamma = BitConverter.ToInt32(numberBytes, 0);

        epsilonBits.CopyTo(numberBytes, 0);
        int epsilon = BitConverter.ToInt32(numberBytes, 0);

        return (gamma * epsilon).ToString();  //4138664
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var input = await File.ReadAllLinesAsync($"inputs/{nameof(Day3).ToLower()}{(example ? ".example" : string.Empty)}.txt");
        var bitCount = input.First().Length;

        var oxygen = getRating("most");
        var co2 = getRating("least");

        IEnumerable<bool[]> filterLines(IEnumerable<bool[]> lines, string type, int index)
        {
            var mostCommon = lines.Count(b => b[index]) >= (lines.Count() / (double)2);  //determine most/least common
            var newLines = lines.Where(line => (type == "most") ? line[index] != mostCommon : line[index] == mostCommon);
            if (newLines.Count() > 1)
                return filterLines(newLines, type, index + 1);

            return newLines;
        }

        int getRating(string type)
        {
            var lines = input.Select(line => line.ToCharArray().Select(a => Convert.ToBoolean(a - '0')).ToArray());
            lines = filterLines(lines, type, 0);

            var ratingBits = new BitArray(lines.First().Select(b => Convert.ToBoolean(b)).Reverse().ToArray());
            byte[] numberBytes = new byte[sizeof(int)];
            ratingBits.CopyTo(numberBytes, 0);
            return BitConverter.ToInt32(numberBytes, 0);
        }

        return (oxygen * co2).ToString();//4273224

    }
}