// See https://aka.ms/new-console-template for more information

using AoC_Framework;
using System.Reflection;
using System.Text.RegularExpressions;

var year = DateTime.Today.Year;
var month = DateTime.Today.Month;
var day = DateTime.Today.Day;

//year = 2021;
//month = 12;
//day = 1;

var runExamples = true;

List<int> years = new() { year };
List<int> days = new() { day };

if (month != 12)
{
  years.Clear();
  days.Clear();
  for (int i = 2022; i <= year; i++)
    years.Add(i);
  for (int i = 1; i <= 25; i++)
    days.Add(i);
}

var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
var examplesDir = Path.Combine(exeDir, "Examples");
var inputsDir = Path.Combine(exeDir, "Inputs");

var challenges =
  new DirectoryInfo(exeDir)
    .GetFiles("AoC*.dll")
    .Where(f => Regex.IsMatch(f.Name, "AoC[0-9]{4}.dll"))
    .SelectMany<FileInfo, (Type Type, AoCChallengeAttribute Attr)>(
      f =>
      {
        try
        {
          return
            Assembly.LoadFile(f.FullName)
              .GetTypes()
              .Select(t => (t, t.GetCustomAttribute<AoCChallengeAttribute>()))
              .Where(
                ((Type Type, AoCChallengeAttribute? Attr) t) =>
                {
                  return 
                    t.Attr != null &&
                    years.Contains(t.Attr.Year) &&
                    days.Contains(t.Attr.Day) &&
                    t.Type.IsAssignableTo(typeof(IAoCChallenge));
                })!;
        }
        catch { /* Ignore it */ }
        return default!;
      })
    .Where(t => t != default)
    .OrderBy(t => t.Attr.Year)
    .OrderBy(t => t.Attr.Day);

foreach (var challenge in challenges)
{
  var baseInputName = challenge.Type.Assembly.GetName().Name;
  var impl = (IAoCChallenge?)challenge.Type.GetConstructor(new Type[] { })?.Invoke(null);
  if (impl == null)
    continue; //TODO: print error message?
  if (runExamples)
  {
    var examples =
      new DirectoryInfo(examplesDir)
        .GetFiles($"{baseInputName}-Day{challenge.Attr.Day}*.txt");
    var exP1 = examples.Where(f => f.Name.Contains("Part1"));
    var exP2 = examples.Where(f => f.Name.Contains("Part2"));
    if (exP1.Count() != 0)
    {
      foreach (var example in exP1)
      {
        var lines = File.ReadAllLines(example.FullName);
        var delimiter = $"_{lines.First()}";
        var answerEnd = Array.IndexOf(lines, delimiter);
        var expectedLines = lines.Skip(1).Take(answerEnd - 1).Where(l => !l.StartsWith("#"));
        if (!expectedLines.Any())
          continue;
        var expected = string.Join(Environment.NewLine, expectedLines);
        var input =lines.Skip(answerEnd + 1);
        Console.WriteLine($"Testing part 1 example '{example.Name}':");
        Console.WriteLine();
        Console.WriteLine($"Expected answer:");
        Console.WriteLine(expected);
        Console.WriteLine();
        var answer = impl.RunPart1(input);
        Console.WriteLine($"Produced answer:");
        Console.WriteLine(answer);
        Console.WriteLine();
        Console.WriteLine();
      }
    }
    if (impl.Part2Ready && exP2.Count() != 0)
    {
      foreach (var example in exP2)
      {
        var lines = File.ReadAllLines(example.FullName);
        var delimiter = $"_{lines.First()}";
        var answerEnd = Array.IndexOf(lines, delimiter);
        var expectedLines = lines.Skip(1).Take(answerEnd - 1).Where(l => !l.StartsWith("#"));
        if (!expectedLines.Any())
          continue;
        var expected = string.Join(Environment.NewLine, expectedLines);
        var input = lines.Skip(answerEnd + 1);
        Console.WriteLine($"Testing part 2 example '{example.Name}':");
        Console.WriteLine();
        Console.WriteLine($"Expected answer:");
        Console.WriteLine(expected);
        Console.WriteLine();
        var answer = impl.RunPart2(input);
        Console.WriteLine($"Produced answer:");
        Console.WriteLine(answer);
        Console.WriteLine();
        Console.WriteLine();
      }
    }
  }
  var inputs =
    new DirectoryInfo(inputsDir)
      .GetFiles($"{baseInputName}-Day{challenge.Attr.Day}*.txt");
  if (inputs.Count() != 0)
  {
    foreach (var input in inputs)
    {
      var lines = File.ReadAllLines(input.FullName);
      Console.WriteLine($"Running part 1 input '{input.Name}':");
      Console.WriteLine();
      var answer = impl.RunPart1(lines);
      Console.WriteLine($"Produced answer:");
      Console.WriteLine(answer);
      Console.WriteLine();
      Console.WriteLine();
    }
  }
  if (impl.Part2Ready && inputs.Count() != 0)
  {
    foreach (var input in inputs)
    {
      var lines = File.ReadAllLines(input.FullName);
      Console.WriteLine($"Running part 2 input '{input.Name}':");
      Console.WriteLine();
      var answer = impl.RunPart2(lines);
      Console.WriteLine($"Produced answer:");
      Console.WriteLine(answer);
      Console.WriteLine();
      Console.WriteLine();
    }
  }
}