namespace Day3;

public static class Main
{
    public static void Part1()
    {
        var fileData = File.ReadAllLines("input.txt");
        var totalPriorities = 0;
        foreach (var backpack in fileData)
        {
            var backpackLength = backpack.Length / 2;

            var firstCompartment = backpack[..backpackLength];
            var secondCompartment = backpack[backpackLength..];
            var duplicateItems = firstCompartment.Intersect(secondCompartment);
            totalPriorities += duplicateItems.Sum(duplicateItem => duplicateItem.GetNumber());
        }

        Console.WriteLine("Sum of the priorities is: {0}", totalPriorities);
    }

    public static void Part2()
    {
        var fileData = File.ReadAllLines("input.txt");
        var totalPriorities = 0;

        for (var i = 0; i < fileData.Length; i += 3)
        {
            var group = fileData.Skip(i).Take(3);
            var elf1 = group.ElementAt(0);
            var elf2 = group.ElementAt(1);
            var elf3 = group.ElementAt(2);
            var duplicateItems = elf1.Intersect(elf2);
            duplicateItems = duplicateItems.Intersect(elf3);
            totalPriorities += duplicateItems.Sum(duplicateItem => duplicateItem.GetNumber());
        }

        Console.WriteLine("Sum of the priorities is: {0}", totalPriorities);
    }


    private static int GetNumber(this char character)
    {
        if (char.IsUpper(character)) return character - 'A' + 27;

        return character - 'a' + 1;
    }
}