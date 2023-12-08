namespace Solutions.Y2023;

public class Day5 : AdventOfCodeTests
{
    public class Seed
    {
        public Seed(long seedNumber)
        {
            SeedNumber = seedNumber;
            Sources.Add(seedNumber);
        }

        public long SeedNumber { get; set; }
        public List<long> Sources { get; set; } = new();

    }

    public class SeedRange
    {
        public long SeedRangeStart { get; }
        public int _range { get; }
        public MyRange Range => new(SeedRangeStart, SeedRangeStart + _range);

        public SeedRange(long seedRangeStart, int range)
        {
            SeedRangeStart = seedRangeStart;
            _range = range;

        }

        public List<Seed> Seeds { get; set; }
        public List<MyRange> Ranges { get; set; } = new();
    }

    public class MapCollection
    {
        public string Name { get; }

        public MapCollection(string name)
        {
            Name = name;
        }

        public List<MyMap> Maps { get; set; } = new();

        public long FindDestination(long source)
        {
            foreach (var map in Maps)
            {
                var destination = map.FindDestination(source);
                if (destination != null)
                    return destination.Value;
            }

            return source;
        }

        public List<MyRange> FindDestination(SeedRange range)
        {
            var ranges = new List<MyRange>();
            foreach (var map in Maps)
            {
                var destinationRange = map.FindDestination(range);
                if (destinationRange != null)
                {
                    ranges.Add(destinationRange);
                }
            }

            foreach (var r in ranges)
            {
            }



            return ranges;
        }

        public List<MyRange> FindDestinationRanges(MyRange source)
        {
            var destinations = new List<MyRange>();
            var sources = new List<MyRange>();
            foreach (var map in Maps)
            {
                var (sourceRange, destination) = map.FindDestination(source);
                if(destination != null) {
                    sources.Add(sourceRange);
                    destinations.Add(destination);
                }
            }

            var leftOvers = new List<MyRange>();
            long end = 0;
            foreach (var range in sources.OrderBy(x => x.Start))
            {
                /*if (range.Start > end)
                {
                    // Gap :<
                }*/

                if (source.Start < range.Start)
                {
                    // Split
                }

                // if(source.End > range.)

                end = range.End;
            }

            return destinations;
        }
    }

    public class MyMap
    {
        public MyMap(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }

        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public long? FindDestination(long source)
        {
            if (source >= SourceRangeStart && source <= SourceRangeStart + RangeLength)
            {
                return DestinationRangeStart + (source - SourceRangeStart);
            }

            return null;
        }

        public MyRange? FindDestination(SeedRange source)
        {
            var min = source.Range.Start;
            var max = source.Range.End;

            var hit = Math.Max(0, Math.Min(max, SourceRangeStart + RangeLength) - Math.Max(min, SourceRangeStart) + 1);
            if (hit != 0)
            {
                return new MyRange(DestinationRangeStart, DestinationRangeStart + RangeLength); // Maybe -1
            }

            return null;
        }

        public (MyRange? source, MyRange? destination) FindDestination(MyRange source)
        {
            var min = source.Start;
            var max = source.End;

            var hit = Math.Max(0, Math.Min(max, SourceRangeStart + RangeLength) - Math.Max(min, SourceRangeStart) + 1);
            if (hit != 0)
            {
                var destination = new MyRange(DestinationRangeStart, Math.Min(DestinationRangeStart + RangeLength, DestinationRangeStart+(max-min))); // Maybe -1
                return (new MyRange(SourceRangeStart, SourceRangeStart+RangeLength), destination);
            }

            return (null, null);
        }
    }

    public Day5(ITestOutputHelper output) : base(output)
    {

    }

    public override void PartOne(List<string> lines)
    {
        var seeds = lines[0].Split(" ").Where(x => int.TryParse(x, out _)).Select(x => new Seed(int.Parse(x))).ToList();

        var collectionOfMaps = CreateMaps(lines);

        foreach (var seed in seeds)
        {
            foreach (var collectionOfMap in collectionOfMaps)
            {
                var newSourceIndex = collectionOfMap.FindDestination(seed.Sources.Last());
                seed.Sources.Add(newSourceIndex);
            }
        }

        var min = seeds.Min(x => x.Sources.Last());

        _output.WriteLine($"Lowest is {min}");
    }

    private static List<MapCollection> CreateMaps(List<string> lines)
    {
        var collectionOfMaps = new List<MapCollection>();
        MapCollection currentCollection = null;

        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Contains(":"))
            {
                currentCollection = new MapCollection(line);
                collectionOfMaps.Add(currentCollection);
                continue;
            }

            var mapInput = line.Split(" ").Where(x => long.TryParse(x, out _)).Select(long.Parse).ToArray();
            var map = new MyMap(mapInput[0], mapInput[1], mapInput[2]);
            currentCollection.Maps.Add(map);
        }

        return collectionOfMaps;
    }

    [Fact]
    public void Test()
    {
        var myRange = new MyRange(1, 10);
        MapCollection c = new MapCollection("");

        var maps = new List<MyMap> {new MyMap(103, 3, 3), new MyMap(106, 6, 1000)};

        c.Maps = maps;

        c.FindDestinationRanges(myRange);


    }

    public override void PartTwo(List<string> lines)
    {
        _output.WriteLine("Starting");
        var seedInput = lines[0].Split(" ").Where(x => long.TryParse(x, out _)).Select(long.Parse).ToArray();

        var seedRanges = new List<MyRange>();
        for (int i = 0; i < seedInput.Length; i += 2)
        {
            seedRanges.Add(new MyRange(seedInput[i], seedInput[i] + seedInput[i+1]));
        }


        var collectionOfMaps = CreateMaps(lines);
        var first = seedRanges.First();



        collectionOfMaps.First().FindDestinationRanges(first);

        foreach (var mapCollection in collectionOfMaps)
        {

        }

        foreach (var range in seedRanges)
        {

            foreach (var collectionOfMap in collectionOfMaps)
            {

                var ranges = collectionOfMap.FindDestinationRanges(range);
            }
        }




        // var min = allSeeds.Min(x => x.Sources.Last());

        // _output.WriteLine($"Lowest is {min}");
    }

}

public record MyRange(long Start, long End);
