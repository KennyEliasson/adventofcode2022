using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Solutions;

public class Day13 : AdventOfCodeTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public Day13(ITestOutputHelper output) : base(output)
    {
        _serializerOptions = new JsonSerializerOptions() {NumberHandling = JsonNumberHandling.Strict};
    }

    public override void PartOne(List<string> lines)
    {
        var groups = lines.Chunk(3).ToList();

        var list = new List<int>();
        for (int i = 0; i < groups.Count; i++)
        {
            var left = JsonNode.Parse(groups[i][0]);
            var right = JsonNode.Parse(groups[i][1]);

            var result = Compare(left, right);
            if (result < 0)
            {
                list.Add(i+1);
            }
            _output.WriteLine($"Group with index {i+1} returns {result.ToString().ToUpper()}");
        }
        _output.WriteLine(list.Sum().ToString());
    }
    
    public override void PartTwo(List<string> lines)
    {
    }

    private int Compare(JsonNode left, JsonNode right)
    {
        if (left is JsonValue && right is JsonValue)
        {
            return left.GetValue<int>() - right.GetValue<int>();
        }

        var leftArray = left as JsonArray ?? new JsonArray((int) left);
        var rightArray = right as JsonArray ?? new JsonArray((int) right);

        var comparisons = leftArray.Zip(rightArray).Select(p => Compare(p.First, p.Second));
        var result = comparisons.FirstOrDefault(calculation => calculation != 0);
        if (result == 0)
        {
            return leftArray.Count - rightArray.Count;
        }

        return result;
    }
    
    

    public override void PartTwo(List<string> lines)
    {
        
    }
}