using System.Reflection;
using System.Runtime.CompilerServices;
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
        var ns = testMethod.DeclaringType.Namespace ?? "";
        ns = ns.Replace("Solutions.", "");
        ns = ns.Replace("Solutions", "");
        ns = ns.Replace('.', Path.PathSeparator);

        var path = Path.Combine(ns, testMethod.DeclaringType.Name, $"{_fileName}.txt");

        yield return new [] { File.ReadAllLines(path).ToList() };
    }
}
