namespace AoC2022
{
  [AoCChallenge(12, 2021)]
  public class Day12 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var graph = new Dictionary<string, HashSet<string>>();

      foreach (var part in input)
      {
        var caves = part.Split('-');
        if (!graph.ContainsKey(caves[0]))
          graph.Add(caves[0], new HashSet<string>());
        if (!graph.ContainsKey(caves[1]))
          graph.Add(caves[1], new HashSet<string>());
        graph[caves[0]].Add(caves[1]);
        graph[caves[1]].Add(caves[0]);
      }

      List<IEnumerable<string>> completedPaths = new();

      Queue<IEnumerable<string>> paths = new();
      paths.Enqueue(new List<string> { "start" });

      while (paths.Count > 0)
      {
        var path = paths.Dequeue();
        var last = path.Last();

        if (last == "end")
          completedPaths.Add(path);

        var connections = graph[last].Where(c => c == c.ToUpper() || !path.Contains(c));

        foreach (var connection in connections)
        {
          paths.Enqueue(path.Append(connection));
        }
      }

      return $"{completedPaths.Count}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var graph = new Dictionary<string, HashSet<string>>();

      foreach (var part in input)
      {
        var caves = part.Split('-');
        if (!graph.ContainsKey(caves[0]))
          graph.Add(caves[0], new HashSet<string>());
        if (!graph.ContainsKey(caves[1]))
          graph.Add(caves[1], new HashSet<string>());
        if (caves[1] != "start")
          graph[caves[0]].Add(caves[1]);
        if (caves[0] != "start")
          graph[caves[1]].Add(caves[0]);
      }

      List<IEnumerable<string>> completedPaths = new();

      Queue<IEnumerable<string>> paths = new();
      paths.Enqueue(new List<string> { "start" });

      while (paths.Count > 0)
      {
        var path = paths.Dequeue();
        var last = path.Last();

        if (last == "end")
        {
          completedPaths.Add(path);
          continue;
        }

        var smallVisits = path.Where(c => c == c.ToLowerInvariant());

        var canVisitSmall = smallVisits.All(c => smallVisits.Count(c2 => c == c2) == 1);

        var connections =
          graph[last]
            .Where(c => canVisitSmall || (c == c.ToUpper() || !path.Contains(c)));

        foreach (var connection in connections)
        {
          paths.Enqueue(path.Append(connection));
        }
      }

      return $"{completedPaths.Count}";
    }
  }
}
