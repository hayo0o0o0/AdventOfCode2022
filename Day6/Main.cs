namespace Day6;

public static class Main
{
    public static void Part1()
    {
        var input = File.ReadAllText("input.txt");
        var start = 0;
        var marker = "";
        var end = 4;
        while (marker.Distinct().Count() != 4)
        {
            marker = input[start..end];
            start++;
            end = start + 4;
        }

        Console.WriteLine("Marker: {1} Answer: {0}", end-1, marker);
    }
    
    public static void Part2()
    {
        var input = File.ReadAllText("input.txt");
        var start = 0;
        var marker = "";
        var end = 14;
        while (marker.Distinct().Count() != 14)
        {
            marker = input[start..end];
            start++;
            end = start + 14;
        }

        Console.WriteLine("Marker: {1} Answer: {0}", end-1, marker);
    }
}