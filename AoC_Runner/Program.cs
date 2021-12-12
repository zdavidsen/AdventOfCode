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
//var examplesDir = Path.Combine(exeDir, "Examples");
//var inputsDir = Path.Combine(exeDir, "Inputs");

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
  var assemblyName = challenge.Type.Assembly.GetName().Name;
  var baseInputDir = Path.Combine(exeDir, assemblyName!);
  if (!Directory.Exists(baseInputDir))
  {
    Console.Error.WriteLine($"Inputs for '{assemblyName}' do not exist; Skipping challenges");
    continue;
  }
  var examplesDir = Path.Combine(baseInputDir, "Examples");
  var inputsDir = Path.Combine(baseInputDir, "Inputs");

  var impl = (IAoCChallenge?)challenge.Type.GetConstructor(new Type[] { })?.Invoke(null);
  if (impl == null)
  {
    Console.Error.WriteLine(
      $"Unable to instantiate '{challenge.Type.FullName}' to run challenges for " +
      $"{challenge.Attr.Year}, Day {challenge.Attr.Day}");
    continue;
  }

  var examples =
    new DirectoryInfo(examplesDir)
      .GetFiles($"Day{challenge.Attr.Day:D2}*.txt");
  var exP1 = examples.Where(f => f.Name.Contains("Part1"));
  var exP2 = examples.Where(f => f.Name.Contains("Part2"));

  var inputs =
    new DirectoryInfo(inputsDir)
      .GetFiles($"Day{challenge.Attr.Day:D2}*.txt");

  if (runExamples && exP1.Count() != 0)
  {
    RunChallenge(challenge, 1, true, exP1);
  }

  if (inputs.Count() != 0)
  {
    RunChallenge(challenge, 1, false, inputs);
  }

  if (impl.Part2Ready)
  {
    if (runExamples && exP2.Count() != 0)
    {
      RunChallenge(challenge, 2, true, exP2);
    }

    if (inputs.Count() != 0)
    {
      RunChallenge(challenge, 2, false, inputs);
    }
  }
}

IAoCChallenge? GetImpl(Type type)
{
  return type.GetConstructor(new Type[] { })?.Invoke(null) as IAoCChallenge;
}

void RunChallenge((Type Type, AoCChallengeAttribute Attr) challenge, int part, bool isExample, IEnumerable<FileInfo> inputFiles)
{
  Console.WriteLine($"{challenge.Attr.Year} Day {challenge.Attr.Day} Part {part}{(isExample ? " Examples" : "")}");
  Console.WriteLine("====================");
  foreach (var inputFile in inputFiles)
  {
    Console.WriteLine($"Running '{inputFile.Name}':");
    var impl = GetImpl(challenge.Type);
    var lines = File.ReadAllLines(inputFile.FullName);
    IEnumerable<string> input;
    if (isExample)
    {
      var delimiter = $"_{lines.First()}";
      var answerEnd = Array.IndexOf(lines, delimiter);
      var expectedLines = lines.Skip(1).Take(answerEnd - 1).Where(l => !l.StartsWith("#"));
      if (!expectedLines.Any())
        continue;
      var expected = string.Join(Environment.NewLine, expectedLines);
      input = lines.Skip(answerEnd + 1);
      Console.WriteLine($"Expected answer:");
      Console.WriteLine(expected);
    }
    else
    {
      input = lines;
    }
    string answer = "";
    if (part == 1)
      answer = impl!.RunPart1(input);
    if (part == 2)
      answer = impl!.RunPart2(input);
    Console.WriteLine($"Produced answer:");
    Console.WriteLine(answer);
    Console.WriteLine();
  }
  Console.WriteLine();
}