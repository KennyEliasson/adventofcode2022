using System.Text;

namespace Solutions.Y2024;

public class Day9(ITestOutputHelper output) : AdventOfCodeTests(output)
{
    public record FilePart(long Index, int Length);
 
    public override void PartOne(List<string> lines)
    {
        var line = lines[0];
        var fileParts = CreateFileParts(line);

        for (int i = 0; i < fileParts.Count; i++)
        {
            var lastIndex = fileParts.FindLastIndex(x => x.Index != -1);
            if (lastIndex < i)
            {
                break;
            }
        
            if(fileParts[i].Index == -1)
            {
                (fileParts[i], fileParts[lastIndex]) = (fileParts[lastIndex], fileParts[i]);
            }
        }

        var result = fileParts.Where(x => x.Index > -1).Select((x, i) => i * x.Index).Sum();
        _output.WriteLine(result.ToString());
    }

    private static List<FilePart> CreateFileParts(string line)
    {
        var fileParts = new List<FilePart>();

        for (var i = 0; i < line.Length; i++)
        {
            var blockLength = int.Parse(line[i].ToString());
            var id = (i % 2 == 0) ? (i == 0 ? 0 : i / 2) : -1;

            fileParts.AddRange(Enumerable.Repeat(new FilePart(id, blockLength), blockLength));
        }

        return fileParts;
    }

    public override void PartTwo(List<string> lines)
    {
        var line = lines[0];
        var fileParts = CreateFileParts(line);
        
        for (var i = fileParts.Count - 1; i >= 0; i--)
        {
            if (fileParts[i].Index == -1)
                continue;

            var index = fileParts.FindIndex(x => x.Index == -1 && x.Length >= fileParts[i].Length);
            if (index > i || index < 0)
            {
                continue;
            }
            
            var freeSpacePart = fileParts[index];
            var swapPart = fileParts[i];
            
            fileParts[index] = swapPart;
            fileParts[i] = swapPart with { Index = -1 };
            fileParts.Insert(index+1, freeSpacePart with { Length = freeSpacePart.Length - swapPart.Length });
        }
        
        long result = 0;
        long currIndex = 0;
        foreach (var part in fileParts)
        {
            for (int i = 0; i < part.Length; i++)
            {
                if (part.Index >= 0)
                {
                    result += currIndex * part.Index;
                }
                
                currIndex++;
            }
        }

        _output.WriteLine(result.ToString());
    }
}
