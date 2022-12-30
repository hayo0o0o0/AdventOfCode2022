namespace Day4;

public class Main
{
    public static void Part1()
    {
        var fileData = File.ReadAllLines("input.txt");
        var amountOfPairsForReconsideration = 0;
        foreach (var cleaningPair in fileData)
        {
            var splitPairs = cleaningPair.Split(",");
            var firstAssignment = GetRoomsToClean(splitPairs[0]);
            var secondAssignment = GetRoomsToClean(splitPairs[1]);

            if (FullyOverlap(secondAssignment, firstAssignment))
            {
                amountOfPairsForReconsideration++;
            }
            else if (FullyOverlap(firstAssignment, secondAssignment))
            {
                amountOfPairsForReconsideration++;
            }
        }

        Console.WriteLine("Amount of pairs that need reconsideration: {0}", amountOfPairsForReconsideration);
    }

    public static void Part2()
    {
        var fileData = File.ReadAllLines("input.txt");
        var amountOfPairsForReconsideration = 0;
        foreach (var cleaningPair in fileData)
        {
            var splitPairs = cleaningPair.Split(",");
            var firstAssignment = GetRoomsToClean(splitPairs[0]);
            var secondAssignment = GetRoomsToClean(splitPairs[1]);

            if (PartialOverlap(secondAssignment, firstAssignment))
            {
                amountOfPairsForReconsideration++;
            }
            else if (PartialOverlap(firstAssignment, secondAssignment))
            {
                amountOfPairsForReconsideration++;
            }
        }

        Console.WriteLine("Amount of pairs that need reconsideration: {0}", amountOfPairsForReconsideration);
    }

    private static bool FullyOverlap(IEnumerable<int> range1, IEnumerable<int> range2)
    {
        var remaining = range1.Except(range2);
        return !remaining.Any();
    }
    
    private static bool PartialOverlap(IEnumerable<int> range1, IEnumerable<int> range2)
    {
        var remaining = range1.Intersect(range2);
        return remaining.Any();
    }

    private static IEnumerable<int> GetRoomsToClean(string assignment)
    {
        var roomNumbers = assignment.Split('-').Select(int.Parse).ToArray();
        return GetRange(roomNumbers[0], roomNumbers[1]);
    }

    private static IEnumerable<int> GetRange(int from, int to)
    {
        if (from == to)
        {
            yield return from;
        }

        for (var i = from; i <= to; i++)
        {
            yield return i;
        }
    }
}