namespace AoC2022
{
  [AoCChallenge(9, 2021)]
  public class Day9 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var map =
        input.Select(
          l => l.Select(c => c - 0x30).ToArray())
        .ToArray();

      var risk = 0;

      for (int y = 0; y < map.Length; y++)
      {
        var prev = y > 0 ? map[y - 1] : null;
        var line = map[y];
        var next = y < map.Length - 1 ? map[y + 1] : null;

        for (int x = 0; x < line.Length; x++)
        {
          var val = line[x];
          if (val < (prev?[x] ?? 10) &&
              val < (x > 0 ? line[x - 1] : 10) &&
              val < (x < line.Length - 1 ? line[x + 1] : 10) &&
              val < (next?[x] ?? 10))
            risk += line[x] + 1;
        }
      }
      
      return $"{risk}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var map =
        input.Select(
          l => l.Select<char, (int Height, List<int> Basins)>(c => (c - 0x30, new List<int>())).ToArray())
        .ToArray();

      var height = map.Length;
      var width = map[0].Length;

      var basins = new Dictionary<int, List<(int X, int Y)>>();

      var nextBasinNumber = 0;

      for (int y = 0; y < height; y++)
      {
        var prev = y > 0 ? map[y - 1] : null;
        var line = map[y];
        var next = y < height - 1 ? map[y + 1] : null;

        for (int x = 0; x < width; x++)
        {
          var val = line[x].Height;
          if (val < (prev?[x].Height ?? 10) &&
              val < (x > 0 ? line[x - 1].Height : 10) &&
              val < (x < width - 1 ? line[x + 1].Height : 10) &&
              val < (next?[x].Height ?? 10))
          {
            var basin = nextBasinNumber++;
            line[x].Basins.Add(basin);
            basins[basin] = new List<(int X, int Y)> { (x, y) };
          }
        }
      }

      var basinExpanded = true;
      while (basinExpanded)
      {
        basinExpanded = false;
        foreach (var basin in basins)
        {
          var basinId = basin.Key;
          var points = basin.Value;
          var toTest = new List<(int X, int Y)>();

          foreach (var point in points)
          {
            (int X, int Y) up =     (point.X,     point.Y - 1);
            (int X, int Y) down =   (point.X,     point.Y + 1);
            (int X, int Y) left =   (point.X - 1, point.Y);
            (int X, int Y) right =  (point.X + 1, point.Y);

            var neighbors = new List<(int X, int Y)>();

            if (up.Y >= 0)        neighbors.Add(up);
            if (down.Y < height)  neighbors.Add(down);
            if (left.X >= 0)      neighbors.Add(left);
            if (right.X < width)  neighbors.Add(right);

            foreach (var neighbor in neighbors)
            {
              if (map[point.Y][point.X].Height <= map[neighbor.Y][neighbor.X].Height &&
                  map[neighbor.Y][neighbor.X].Height < 9 &&
                  !points.Contains(neighbor) &&
                  !toTest.Contains(neighbor))
                toTest.Add(neighbor);
            }
          }

          foreach (var point in toTest)
          {
            (int X, int Y) up =     (point.X,     point.Y - 1);
            (int X, int Y) down =   (point.X,     point.Y + 1);
            (int X, int Y) left =   (point.X - 1, point.Y);
            (int X, int Y) right =  (point.X + 1, point.Y);

            var neighbors = new List<(int X, int Y)>();

            if (up.Y >= 0)        neighbors.Add(up);
            if (down.Y < height)  neighbors.Add(down);
            if (left.X >= 0)      neighbors.Add(left);
            if (right.X < width)  neighbors.Add(right);

            if (neighbors.All(
                  neighbor =>
                    map[neighbor.Y][neighbor.X].Basins.Contains(basinId) ||
                    map[point.Y][point.X].Height <= map[neighbor.Y][neighbor.X].Height))
            {
              basins[basinId].Add(point);
              map[point.Y][point.X].Basins.Add(basinId);
              basinExpanded = true;
            }
          }
        }
      }

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (map[y][x].Basins.Count > 1)
          {
            foreach (var basin in map[y][x].Basins)
            {
              basins[basin].Remove((x, y));
            }
          }
        }
      }

      var product = basins.OrderByDescending(b => b.Value.Count).Take(3).Aggregate(1, (a, b) => a * b.Value.Count);

      return $"{product}";
    }
  }
}
