using System.Text;

namespace Day7;

public class Directory
{
    public Directory(string name)
    {
        Name = name;
    }

    public Directory(string name, Directory parentDirectory)
    {
        Name = name;
        ParentDirectory = parentDirectory;
    }

    public string Name { get; }
    public Directory? ParentDirectory { get; }
    public List<Directory> Directories { get; } = new();
    public List<File> Files { get; } = new();

    public int Size
    {
        get { return Files.Sum(x => x.Size) + Directories.Sum(x => x.Size); }
    }
}

public class File
{
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public int Size { get; }
}

public class Main
{
    public static void Part1()
    {
        var fileData = System.IO.File.ReadAllLines("input.txt");
        var rootDirectory = BuildFileSystem(fileData);

        var sb = new StringBuilder();
        var output = PrintOutput(rootDirectory, sb);
        var allDirectories = GetAllDirectories(rootDirectory);
        var answer = allDirectories.Where(x => x.Size < 100000).Sum(x => x.Size);
        Console.WriteLine(output);
        Console.WriteLine("Sum of the total sizes of the directories is: {0}", answer);
    }

    public static void Part2()
    {
        var fileData = System.IO.File.ReadAllLines("input.txt");
        var rootDirectory = BuildFileSystem(fileData);

        var sb = new StringBuilder();
        var output = PrintOutput(rootDirectory, sb);
        var allDirectories = GetAllDirectories(rootDirectory);
        const int totalDiskSpace = 70000000;
        var spaceRemaining = totalDiskSpace - rootDirectory.Size;
        var directoryToDelete = rootDirectory;
        var spaceToFreeUp = 30000000 - spaceRemaining;
        if (spaceToFreeUp > 0)
        {
            var sortedDirectoriesBySize = allDirectories.OrderBy(x => x.Size);
            foreach (var directory in sortedDirectoriesBySize)
            {
                if (directory.Size >= spaceToFreeUp)
                {
                    directoryToDelete = directory;
                    break;
                }
            }
        }

        Console.WriteLine(output);
        Console.WriteLine("Directory to delete {0} and size {1}", directoryToDelete.Name, directoryToDelete.Size);
    }

    private static Directory BuildFileSystem(IEnumerable<string> fileData)
    {
        var rootDirectory = new Directory("/");
        var currentDirectory = rootDirectory;
        foreach (var command in fileData)
        {
            if (command == "$ cd .." && currentDirectory.ParentDirectory != null)
            {
                currentDirectory = currentDirectory.ParentDirectory;
            }
            else if (command.StartsWith("$ cd"))
            {
                var commands = command.Replace("$ ", "").Split(" ");
                var directoryName = commands[1];
                if (directoryName == "/")
                {
                    continue;
                }

                var newDirectory = currentDirectory.Directories.FirstOrDefault(x => x.Name == directoryName);
                if (newDirectory == null)
                {
                    newDirectory = new Directory(directoryName, currentDirectory);
                    currentDirectory.Directories.Add(newDirectory);
                }

                currentDirectory = newDirectory;
            }
            else if (command == "$ ls")
            {
            }
            else
            {
                if (command.StartsWith("dir"))
                {
                    var commands = command.Split(" ");
                    var directoryName = commands[1];
                    currentDirectory.Directories.Add(new Directory(directoryName, currentDirectory));
                }
                else
                {
                    var commands = command.Split(" ");
                    currentDirectory.Files.Add(new File(commands[1], int.Parse(commands[0])));
                }
            }
        }

        return rootDirectory;
    }

    private static IEnumerable<Directory> GetAllDirectories(Directory directory)
    {
        var list = new List<Directory>();
        list.AddRange(directory.Directories);
        foreach (var subDirectory in directory.Directories)
        {
            list.AddRange(GetAllDirectories(subDirectory));
        }

        return list;
    }

    private static string PrintOutput(Directory directory, StringBuilder sb, string level = "")
    {
        sb.AppendLine($"{level} - {directory.Name} (dir) (size={directory.Size})");
        var fileLevel = level + "  ";
        level += "  ";
        foreach (var subDirectory in directory.Directories)
        {
            PrintOutput(subDirectory, sb, level);
        }

        foreach (var file in directory.Files)
        {
            sb.AppendLine($"{fileLevel} - {file.Name} (size={file.Size})");
        }

        return sb.ToString();
    }
}