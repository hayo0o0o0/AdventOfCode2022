using System.Text;

namespace Day5;

public static class Main
{
    public static void Part1()
    {
        var fileData = File.ReadAllLines("input.txt");
        var crateNumberPosition = Array.FindIndex(fileData, x => x.StartsWith(" 1"));
        var crates = PrepareCrates(fileData, crateNumberPosition);

        FillCrates(crateNumberPosition, fileData, crates);

        ExecuteActions(crateNumberPosition, fileData, crates);

        var highest = crates.Max(x => x.Count);

        var answer = string.Join("", crates.Select(x => x.Peek()));
        

        var crateOutput = BuildCrateOutput(crates, highest);

        Console.WriteLine(crateOutput);
        Console.WriteLine("Highest crates: {0}", answer.Replace("[", "").Replace("]", ""));
    }

    private static string BuildCrateOutput(List<Stack<string>> crates, int highest)
    {
        var sb = new StringBuilder();
        while (crates.All(x => x.Count != 0))
        {
            var line = "";
            foreach (var crate in crates)
            {
                if (crate.Count == highest)
                {
                    line += crate.Pop() + " ";
                }
                else
                {
                    line += "    ";
                }
            }

            sb.AppendLine(line);

            highest--;
        }

        var crateLine = "";
        foreach (var (crate, index) in crates.Select((crate, index) => (crate, index)))
        {
            crateLine += $" {index + 1}  ";
        }

        sb.AppendLine(crateLine);
        return sb.ToString();
    }

    private static void ExecuteActions(int crateNumberPosition, string[] fileData, List<Stack<string>> crates)
    {
        for (var i = crateNumberPosition + 1; i < fileData.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(fileData[i]))
            {
                Console.WriteLine(fileData[i]);
                var (quantity, from, to) = ParseAction(fileData, i);

                for (var j = 0; j < quantity; j++)
                {
                    var itemToMove = crates[from - 1].Pop();
                    crates[to - 1].Push(itemToMove);
                }
            }
        }
    }

    private static (int quantity, int from, int to) ParseAction(string[] fileData, int i)
    {
        var action = fileData[i].Replace("move ", "");
        var firstSpace = action.IndexOf(" ");
        var quantity = int.Parse(action[..firstSpace]);
        action = action.Replace(quantity + " from ", "");
        var from = int.Parse(action[..1]);
        action = action.Replace(from + " to ", "");
        var to = int.Parse(action[..1]);
        return (quantity, from, to);
    }

    private static void FillCrates(int crateNumberPosition, string[] fileData, List<Stack<string>> crates)
    {
        for (var i = crateNumberPosition - 1; i >= 0; i--)
        {
            var crateRow = fileData[i].SplitEvery(3).ToArray();

            for (var j = 0; j < crateRow.Length; j++)
            {
                if (!string.IsNullOrWhiteSpace(crateRow[j]))
                {
                    crates[j].Push(crateRow[j]);
                }
            }
        }
    }

    private static List<Stack<string>> PrepareCrates(string[] fileData, int crateNumberPosition)
    {
        var numberOfCrates = fileData[crateNumberPosition].Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
        var crates = new List<Stack<string>>();
        foreach (var numberOfCrate in numberOfCrates)
        {
            crates.Add(new Stack<string>());
        }

        return crates;
    }

    private static IEnumerable<string> SplitEvery(this string str, int size)
    {
        while (!string.IsNullOrEmpty(str))
        {
            var item = str[..size];
            str = str[size..];
            if (str.Length > 0)
            {
                str = str[1..];
            }

            yield return item;
        }
    }
}