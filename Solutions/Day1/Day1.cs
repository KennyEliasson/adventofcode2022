using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using Xunit;
using Xunit.Abstractions;

namespace Solutions;

public class Day1
{
    private readonly ITestOutputHelper _output;

    public Day1(ITestOutputHelper output)
    {
        _output = output;
    }

    private IEnumerable<Elf> CreateElfs()
    {
        var lines = File.ReadAllLines("Day1/input.txt");
        
        var currentElf = new Elf();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                yield return currentElf;
                currentElf = new Elf();
                continue;
            }

            currentElf.Calories += int.Parse(line);
        }

        yield return currentElf;
    }

    [Fact]
    public void PartOne()
    {
        var elfs = CreateElfs();
        
        _output.WriteLine($"Max calories is {elfs.Max(x => x.Calories)}");
        
    }
    
    [Fact]
    public void PartTwo()
    {
        var elfs = CreateElfs();
        _output.WriteLine($"Top three elves with most calories are carrying {elfs.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories)} calories");
        
    }
}

public class Elf
{
    public int Calories { get; set; }
}