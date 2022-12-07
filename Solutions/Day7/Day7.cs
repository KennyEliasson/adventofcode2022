namespace Solutions;

public class Day7 {

    private readonly ITestOutputHelper _output;

    public Day7(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void PartOne()
    {
        var input = File.ReadLines("Day7/input.txt");
        var commands = ParseCommands(input);
        var (root, directories) = CreateDirectoryStructure(commands);

        var dirsWithLessSize = directories.Where(x => x.TotalFileSize.Value < 100000).ToList();
        
        _output.WriteLine($"The total size of directories with less than 100000 in size is {dirsWithLessSize.Sum(x => x.TotalFileSize.Value)}");

    }

    [Fact]
    public void PartTwo()
    {
        var input = File.ReadLines("Day7/input.txt");
        var commands = ParseCommands(input);
        var (root, directories) = CreateDirectoryStructure(commands);

        var rootDir = directories.First();
        var usedSpace = rootDir.TotalFileSize.Value;
        var unusedSpace = 70000000 - usedSpace;
        var spaceLeftToUpdate = 30000000 - unusedSpace;
        
        var chosenDir = directories.Where(x => x.TotalFileSize.Value > spaceLeftToUpdate).MinBy(x => x.TotalFileSize.Value);

        _output.WriteLine($"The directory to delete is {chosenDir.Name} with a size of {chosenDir.TotalFileSize}");
    }

    private static List<Command> ParseCommands(IEnumerable<string> input)
    {
        var commands = new List<Command>();
        foreach (var line in input)
        {
            if (line.StartsWith("$"))
            {
                if (line.StartsWith("$ cd"))
                    commands.Add(new CdCommand(line[5..]));
                else
                    commands.Add(new LsCommand());
            }
            else
            {
                commands.Last().AddInput(line);    
            }
        }

        return commands;
    }

    private static (Directory Root, HashSet<Directory> Directories) CreateDirectoryStructure(IEnumerable<Command> commands)
    {
        var root = new Directory("/", null!);
        var currDir = root;

        var directories = new HashSet<Directory> {currDir};

        foreach (var command in commands.Skip(1)) // The first one is always navigating to root
        {
            currDir = command.Execute(currDir);
            directories.Add(currDir);
        }

        return (root, directories);
    }
}

public class Directory
{
    public string Name { get; }

    public Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
        TotalFileSize = new Lazy<int>(CalculateTotalFilesize);
    }

    public Directory Parent { get; }
    public Lazy<int> TotalFileSize { get; }

    public int CalculateTotalFilesize()
    {
        var size = 0;
        foreach (var (_, dir) in Directories)
        {
            size += dir.TotalFileSize.Value;
        }

        size += Files.Sum(x => x.size);

        return size;
    }

    public List<(string fileName, int size)> Files { get; } = new();
    public Dictionary<string, Directory> Directories { get; } = new();
}

public class CdCommand : Command
{
    public CdCommand(string arg)
    {
        Argument = arg;
    }

    public string Argument { get; }
    public override Directory Execute(Directory directory)
    {
        if (Argument == "/")
            throw new NotImplementedException();

        if (Argument == "..")
            return directory.Parent;
        
        return directory.Directories[Argument];
    }
}

public class LsCommand : Command
{
    public override Directory Execute(Directory directory)
    {
        foreach (var input in Input)
        {
            var parts = input.Split(" ", StringSplitOptions.TrimEntries);
            
            if (parts[0].StartsWith("dir"))
            {
                directory.Directories.Add(parts[1], new Directory(parts[1], directory));
            }
            else
            {
                directory.Files.Add((parts[1], int.Parse(parts[0])));
            }
        }

        return directory;
    }
}

public abstract class Command
{
    public abstract Directory Execute(Directory directory);
    public List<string> Input { get; } = new();
    
    public void AddInput(string input)
    {
        Input.Add(input);
    }
}

