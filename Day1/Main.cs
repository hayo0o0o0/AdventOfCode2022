namespace Day1;

public class Main
{
    public static void Part1()
    {
        var calories = ReadCalories();
        var maxCalories = calories.Max();

        Console.WriteLine("Highest calories are {0}", maxCalories);
    }

    public static void Part2()
    {
        var calories = ReadCalories();
        var top3 = calories.OrderByDescending(x=>x).Take(3);
        var totalCalories = top3.Sum();
        
        Console.WriteLine("The top 3 elves are carrying {0} calories", totalCalories);
    }

    private static IEnumerable<int> ReadCalories()
    {
        var fileData = File.ReadAllText("input.txt");
        var elves = fileData.Split($"{Environment.NewLine}{Environment.NewLine}",
            StringSplitOptions.RemoveEmptyEntries);
        var calories = elves.Select(x =>
            x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum());
        return calories;
    }
}