using System.Reflection;
using Xunit.Sdk;

namespace Solutions;

public class AdventOfCodeDataAttribute : DataAttribute
{
    private readonly string _fileName;

    public AdventOfCodeDataAttribute(string fileName)
    {
        _fileName = fileName;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new [] { File.ReadAllLines($"{testMethod.DeclaringType.Name}/{_fileName}.txt").ToList() };
    }
}