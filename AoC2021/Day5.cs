namespace AoC2022
{
  [AoCChallenge(5, 2021)]
  public class Day5 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      Dictionary<int, Dictionary<int, int>> map = new();

      foreach (string part in input)
      {
        var match = System.Text.RegularExpressions.Regex.Match(part, "([0-9]*),([0-9]*) -> ([0-9]*),([0-9]*)");
        var line =
          new
          {
            StartX = int.Parse(match.Groups[1].Value),
            StartY = int.Parse(match.Groups[2].Value),
            EndX = int.Parse(match.Groups[3].Value),
            EndY = int.Parse(match.Groups[4].Value),
          };
        if (line.StartX == line.EndX)
        {
          for (int y = Math.Min(line.StartY, line.EndY); y <= Math.Max(line.StartY, line.EndY); y++)
          {
            if (!map.ContainsKey(line.StartX))
              map[line.StartX] = new();
            if (!map[line.StartX].ContainsKey(y))
              map[line.StartX][y] = 0;
            map[line.StartX][y]++;
          }
        }
        if (line.StartY == line.EndY)
        {

          for (int x = Math.Min(line.StartX, line.EndX); x <= Math.Max(line.StartX, line.EndX); x++)
          {
            if (!map.ContainsKey(x))
              map[x] = new();
            if (!map[x].ContainsKey(line.StartY))
              map[x][line.StartY] = 0;
            map[x][line.StartY]++;
          }
        }
      }

      var count =
        map
          .SelectMany(x => x.Value.Values)
          .Count(v => v > 1);

      return $"{count}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      Dictionary<int, Dictionary<int, int>> map = new();

      foreach (string part in input)
      {
        var match = System.Text.RegularExpressions.Regex.Match(part, "([0-9]*),([0-9]*) -> ([0-9]*),([0-9]*)");
        var line =
          new
          {
            StartX = int.Parse(match.Groups[1].Value),
            StartY = int.Parse(match.Groups[2].Value),
            EndX = int.Parse(match.Groups[3].Value),
            EndY = int.Parse(match.Groups[4].Value),
          };

        if (line.StartX == line.EndX)
        {
          for (int y = Math.Min(line.StartY, line.EndY); y <= Math.Max(line.StartY, line.EndY); y++)
          {
            if (!map.ContainsKey(line.StartX))
              map[line.StartX] = new();
            if (!map[line.StartX].ContainsKey(y))
              map[line.StartX][y] = 0;
            map[line.StartX][y]++;
          }
        }
        else if (line.StartY == line.EndY)
        {
          for (int x = Math.Min(line.StartX, line.EndX); x <= Math.Max(line.StartX, line.EndX); x++)
          {
            if (!map.ContainsKey(x))
              map[x] = new();
            if (!map[x].ContainsKey(line.StartY))
              map[x][line.StartY] = 0;
            map[x][line.StartY]++;
          }
        }
        else
        {
          var xmul = Math.Sign(line.EndX - line.StartX);
          var ymul = Math.Sign(line.EndY - line.StartY);
          for (int offset = 0; offset <= Math.Abs(line.StartX - line.EndX); offset++)
          {
            var x = line.StartX + offset * xmul;
            var y = line.StartY + offset * ymul;
            if (!map.ContainsKey(x))
              map[x] = new();
            if (!map[x].ContainsKey(y))
              map[x][y] = 0;
            map[x][y]++;
          }
        }
      }

      var count =
        map
          .SelectMany(x => x.Value.Values)
          .Count(v => v > 1);

      return $"{count}";
    }
  }
}