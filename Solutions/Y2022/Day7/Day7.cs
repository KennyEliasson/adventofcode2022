namespace Solutions.Y2022;

public class Day7 : AdventOfCodeTests
{
    public Day7(ITestOutputHelper output) : base(output)
    { }

    public override void PartOne(List<string> input)
    {
        var commands = ParseCommands(input);
        var (root, directories) = CreateDirectoryStructure(commands);

        var dirsWithLessSize = directories.Where(x => x.TotalFileSize.Value < 100000).ToList();

        _output.WriteLine($"The total size of directories with less than 100000 in size is {dirsWithLessSize.Sum(x => x.TotalFileSize.Value)}");
    }

    public override void PartTwo(List<string> input)
    {
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
            var parts = line.Split(' ');
            if (parts is ["$", "cd", var args])
                commands.Add(new CdCommand(args));
            else if (parts is ["$", "ls", ..])
                commands.Add(new LsCommand());
            else if (parts is ["dir", var directoryName])
                commands.Last().Nodes.Add(new DirectoryNodeInfo(directoryName));
            else if (parts is [var size, var fileName])
                commands.Last().Nodes.Add(new FileNodeInfo(fileName, int.Parse(size)));
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
            currDir = command.CreateDirectoryStructure(currDir);
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

        size += Files.Sum(x => x.Size);

        return size;
    }

    public List<FileNodeInfo> Files { get; } = new();
    public Dictionary<string, Directory> Directories { get; } = new();
}

public class CdCommand : Command
{
    public CdCommand(string arg)
    {
        Argument = arg;
    }

    public string Argument { get; }
    public override Directory CreateDirectoryStructure(Directory directory)
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
    public override Directory CreateDirectoryStructure(Directory directory)
    {
        foreach (var node in Nodes)
        {
            node.AddToDirectory(directory);
        }

        return directory;
    }
}

public abstract class Command
{
    public abstract Directory CreateDirectoryStructure(Directory directory);
    public List<IFileSystemNode> Nodes { get; } = new();
}

public record FileNodeInfo(string Name, int Size) : IFileSystemNode
{
    public void AddToDirectory(Directory directory)
    {
        directory.Files.Add(this);
    }
}

public record DirectoryNodeInfo(string Name) : IFileSystemNode
{
    public void AddToDirectory(Directory directory)
    {
        directory.Directories.Add(Name, new Directory(Name, directory));
    }
}

public interface IFileSystemNode
{
    void AddToDirectory(Directory directory);
}

