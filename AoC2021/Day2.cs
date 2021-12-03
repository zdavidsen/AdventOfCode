namespace AoC2022
{
  [AoCChallenge(2, 2021)]
  public class Day2 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var depth = 0;
      var x = 0;

      foreach (var line in input)
      {
        var parts = line.Split(' ');
        var val = int.Parse(parts[1]);
        switch (parts[0])
        {
          case "forward":
            x += val; break;
          case "down":
            depth += val; break;
          case "up":
            depth -= val; break;
        }
      }

      return $"{depth * x}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var depth = 0;
      var x = 0;
      var aim = 0;

      foreach (var line in input)
      {
        var parts = line.Split(' ');
        var val = int.Parse(parts[1]);
        switch (parts[0])
        {
          case "forward":
            x += val;
            depth += aim * val;
            break;
          case "down":
            aim += val;
            break;
          case "up":
            aim -= val;
            break;
        }
      }

      return $"{depth * x}";
    }
  }
}