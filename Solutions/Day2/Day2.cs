using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Solutions;

public class Day2
{
    private readonly ITestOutputHelper _output;

    public Day2(ITestOutputHelper output)
    {
        _output = output;
    }

    private record Round(char Opponent, char Player)
    {
        // A = Rock
        // B = Paper
        // C = Scissor
    
        // X = Rock
        // Y = Paper
        // Z = Scissor
        private static readonly Dictionary<(char, char), short> Outcomes = new()
        {
            {('A', 'X'), 3},
            {('A', 'Y'), 6},
            {('A', 'Z'), 0},
            {('B', 'X'), 0},
            {('B', 'Y'), 3},
            {('B', 'Z'), 6},
            {('C', 'X'), 6},
            {('C', 'Y'), 0},
            {('C', 'Z'), 3},
        };
        
        private static readonly Dictionary<char, short> ShapePoints = new()
        {
            {'X', 1}, {'Y', 2}, {'Z', 3},
        };

        public int CalculateScore()
        {
            var points = ShapePoints[Player];
            return Outcomes[(Opponent, Player)] + points;
        }
    }
    
    private class RoundFactory
    {
        // X = loose
        // Y = Draw
        // Z = win
        private static readonly Dictionary<(char, char), char> Outcomes = new()
        {
            {('A', 'X'), 'Z'},
            {('A', 'Y'), 'X'},
            {('A', 'Z'), 'Y'},
            {('B', 'X'), 'X'},
            {('B', 'Y'), 'Y'},
            {('B', 'Z'), 'Z'},
            {('C', 'X'), 'Y'},
            {('C', 'Y'), 'Z'},
            {('C', 'Z'), 'X'},
        };
        
        public Round CreateRound(char Opponent, char Player)
        {
            var playerShape = Outcomes[(Opponent, Player)];
            return new Round(Opponent, playerShape);
        }
    }

    [Fact]
    public void PartOne()
    {
        var lines = File.ReadAllLines("Day2/input.txt");
        var rounds = lines.Select(line => new Round(line[0], line[2])).ToList();
        var points = rounds.Sum(x => x.CalculateScore());
        _output.WriteLine($"The score is {points}");
    }
    
    [Fact]
    public void PartTwo()
    {
        var lines = File.ReadAllLines("Day2/input.txt");
        var rounds = lines.Select(line => new RoundFactory().CreateRound(line[0], line[2])).ToList();
        
        var points = rounds.Sum(x => x.CalculateScore());
        _output.WriteLine($"The score is {points}");
    }
}

