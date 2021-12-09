using System.Collections;
using System.Text.RegularExpressions;

public class Day8
{
    public static async Task<string> Part1Async(bool example = false)
    {
        var data = await ParseInput(example);
        var count = data.Sum(d => d.output.Count(n =>
        n.Length == 2 //1
          || n.Length == 3 //7
          || n.Length == 4 //4
          || n.Length == 7 //8
          ));

        return count.ToString();
    }

    public static async Task<string> Part2Async(bool example = false)
    {
        var data = await ParseInput(example);
        var totalOutput = 0;
        foreach (var row in data)
        {
            //get all different combinations (should be 10)
            var inputs = row.input;
            var number = new string[10];
            //Determine number1,number4,number7,number8
            number[1] = inputs.First(i => i.Length == 2);
            number[4] = inputs.First(i => i.Length == 4);
            number[7] = inputs.First(i => i.Length == 3);
            number[8] = inputs.First(i => i.Length == 7);

            //determine number3
            // length==5, and contains both from number1
            number[3] = inputs
            .Where(n => n.Length == 5)
            .Where(n => $"{number[1]}".All(t => n.Contains(t))).Single();
            
            //Determine number 9
            //topletter is the difference between 1 and 7
            var topChar = number[7].Except(number[1]).Single();
            // number 4 plus topletter must be 9
            number[9] = inputs.Where(n => n.Length == 6).Where(n =>
                   $"{number[4]}{topChar}".All(t => n.Contains(t))).Single();

            //Determine number 0
            //8 minus middelchar
            var bottomChar = number[9].Except($"{number[4]}{topChar}").Single();
            var middleChar = number[3].Except($"{number[1]}{topChar}{bottomChar}").Single();
            number[0] = String.Concat(number[8].Except($"{middleChar}"));

            //Determine number 6
            //only one left with 6 characters
            number[6] = inputs.Where(n => n.Length == 6 && !n.Equals(number[9]) && !n.Equals(number[0])).Single();

            //Determine number 5
            //Number 6 without bottom left
            var bottomLeftChar = number[8].Except(number[9]).Single();
            number[5] = String.Concat(number[6].Except($"{bottomLeftChar}"));

            //Determine number 2
            //only one left with 5 characters
            number[2] = inputs.Where(n => n.Length == 5 && !n.Equals(number[3]) && !n.Equals(number[5])).First();

            var outputs = row.output;
            var outputNumbers = int.Parse(String.Concat(outputs.Select(o => Array.IndexOf(number, o))));
            Console.WriteLine(outputNumbers);
            totalOutput += outputNumbers;
        }
        return totalOutput.ToString();
    }

    private static async Task<IEnumerable<(IEnumerable<string> input, IEnumerable<string> output)>> ParseInput(bool example)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day8{(example ? ".example" : string.Empty)}.txt");
        return input.Select(line =>
        {
            var parts = line.Split("|");
            return (
                input: parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => String.Concat(i.Trim().OrderBy(c => c))),
                output: parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => String.Concat(i.Trim().OrderBy(c => c)))
                );
        });
    }
}