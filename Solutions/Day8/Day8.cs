namespace Solutions;

public class Day8 : AdventOfCodeTests
{

    public Day8(ITestOutputHelper output) : base(output)
    { }
    
    public override void PartOne(List<string> lines)
    {
        var columnCount = lines[0].Length;
        var rowCount = lines.Count;
        
        var trees = CreateForest(lines);

        var visible = 0;
        foreach (var tree in trees)
        {
            if (tree.AtTheEdge(rowCount, columnCount))
            {
                visible++;
                continue;
            }
            
            var treesInSameColumn = trees.Where(x => x.Column == tree.Column).ToList();
            var treesInSameRow = trees.Where(x => x.Row == tree.Row).ToList();

            var down = treesInSameColumn.Where(x => x.Row > tree.Row).Any(x => x.Height >= tree.Height);
            var up = treesInSameColumn.Where(x => x.Row < tree.Row).Any(x => x.Height >= tree.Height);
                
            var right = treesInSameRow.Where(x => x.Column > tree.Column).Any(x => x.Height >= tree.Height);
            var left = treesInSameRow.Where(x => x.Column < tree.Column).Any(x => x.Height >= tree.Height);
            
            if (!down || !up || !right || !left)
            {
                visible++;
            }
        }
        
        _output.WriteLine($"Visible tree count is {visible}");
    }

    private static List<Tree> CreateForest(List<string> lines)
    {
        var trees = new List<Tree>();

        foreach (var row in lines.Select((line, index) => (line, index)))
        {
            foreach (var column in row.line.Select((letter, index) => (letter, index)))
            {
                var treeHeight = int.Parse(column.letter.ToString());
                var tree = new Tree(treeHeight, row.index, column.index);
                trees.Add(tree);
            }
        }

        return trees;
    }

    public override void PartTwo(List<string> lines)
    {
        var columnCount = lines[0].Length;
        var rowCount = lines.Count;
        
        var trees = CreateForest(lines);

        var scenicScores = new List<int>();
        foreach (var tree in trees.Where(tree => !tree.AtTheEdge(rowCount, columnCount)))
        {
            var down = trees.Where(x => x.Column == tree.Column && x.Row > tree.Row).ToList();
            var up = trees.Where(x => x.Column == tree.Column && x.Row < tree.Row).Reverse().ToList();
                
            var right = trees.Where(x => x.Row == tree.Row && x.Column > tree.Column).ToList();
            var left = trees.Where(x => x.Row == tree.Row && x.Column < tree.Column).Reverse().ToList();
            
            scenicScores.Add(CalculateScenicScore(down, tree) * CalculateScenicScore(up, tree) * CalculateScenicScore(right, tree) * CalculateScenicScore(left, tree));
        }

        _output.WriteLine($"Maximum scenic score is {scenicScores.Max()}");
    }

    private static int CalculateScenicScore(List<Tree> treeLine, Tree tree)
    {
        for (var i = 0; i < treeLine.Count; i++)
        {
            if (treeLine[i].Height >= tree.Height)
            {
                return i + 1;
            }
        }

        return treeLine.Count;
    }
}

internal record Tree(int Height, int Row, int Column)
{
    public bool AtTheEdge(int maxRows, int maxColumns)
    {
        return Row == 0 || Column == 0 || Row == maxRows - 1 || Column == maxColumns - 1;
    }
}

