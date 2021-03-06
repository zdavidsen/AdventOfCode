namespace AoC2022
{
  [AoCChallenge(11, 2021)]
  public class Day11 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var map =
        input.Select(
          l => l.Select(c => c - 0x30).ToArray())
        .ToArray();

      bool log = false;

      var flashes = 0;

      for (int _ = 0; _ < 100; _++)
      {
        var flashed = new HashSet<(int X, int Y)>();
        var toCheck = new HashSet<(int X, int Y)>();
        for (int y = 0; y < map.Length; y++)
        {
          var line = map[y];

          for (int x = 0; x < line.Length; x++)
          {
            if (++line[x] > 9)
            {
              toCheck.Add((x, y));
              //flashes++;
              //flashed.Add((x, y));
              //for (int i = -1; i <= 1; i++)
              //{
              //  for (int j = -1; j <= 1; j++)
              //  {
              //    if (y + i >= 0 &&
              //        y + i < map.Length &&
              //        x + j >= 0 &&
              //        x + j < line.Length)
              //    {
              //      map[y + i][x + j]++;
              //      toCheck.Add((y + i, x + j));
              //    }
              //  }
              //}
            }
          }
        }

        while (toCheck.Count > 0)
        {
          var position = toCheck.First();
          toCheck.Remove(position);

          if (flashed.Contains(position))
            continue;

          if (map[position.Y][position.X] > 9)
          {
            flashes++;
            flashed.Add(position);
            for (int i = -1; i <= 1; i++)
            {
              for (int j = -1; j <= 1; j++)
              {
                if (position.Y + i >= 0 &&
                    position.Y + i < map.Length &&
                    position.X + j >= 0 &&
                    position.X + j < map[0].Length &&
                    !(i == 0 && j == 0))
                {
                  if (++map[position.Y + i][position.X + j] > 9)
                    toCheck.Add((position.X + j, position.Y + i));
                }
              }
            }
          }
        }

        if (!log)
        {
          foreach (var flash in flashed)
          {
            map[flash.Y][flash.X] = 0;
          }
        }
        else
        {
          for (int y = 0; y < map.Length; y++)
          {
            var line = map[y];

            for (int x = 0; x < line.Length; x++)
            {
              if (line[x] > 9)
              {
                line[x] = 0;
              }
              Console.Write($"{line[x]}");
            }
            Console.WriteLine();
          }
          Console.WriteLine();
        }

      }

      return $"{flashes}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var map =
        input.Select(
          l => l.Select(c => c - 0x30).ToArray())
        .ToArray();

      bool log = false;

      var flashes = 0;

      for (int _ = 1; ; _++)
      {
        var flashed = new HashSet<(int X, int Y)>();
        var toCheck = new HashSet<(int X, int Y)>();
        for (int y = 0; y < map.Length; y++)
        {
          var line = map[y];

          for (int x = 0; x < line.Length; x++)
          {
            if (++line[x] > 9)
            {
              toCheck.Add((x, y));
              //flashes++;
              //flashed.Add((x, y));
              //for (int i = -1; i <= 1; i++)
              //{
              //  for (int j = -1; j <= 1; j++)
              //  {
              //    if (y + i >= 0 &&
              //        y + i < map.Length &&
              //        x + j >= 0 &&
              //        x + j < line.Length)
              //    {
              //      map[y + i][x + j]++;
              //      toCheck.Add((y + i, x + j));
              //    }
              //  }
              //}
            }
          }
        }

        while (toCheck.Count > 0)
        {
          var position = toCheck.First();
          toCheck.Remove(position);

          if (flashed.Contains(position))
            continue;

          if (map[position.Y][position.X] > 9)
          {
            //flashes++;
            flashed.Add(position);
            for (int i = -1; i <= 1; i++)
            {
              for (int j = -1; j <= 1; j++)
              {
                if (position.Y + i >= 0 &&
                    position.Y + i < map.Length &&
                    position.X + j >= 0 &&
                    position.X + j < map[0].Length &&
                    !(i == 0 && j == 0))
                {
                  if (++map[position.Y + i][position.X + j] > 9)
                    toCheck.Add((position.X + j, position.Y + i));
                }
              }
            }
          }
        }

        if (flashed.Count == map.Length * map[0].Length)
        {
          return $"{_}";
        }

        if (!log)
        {
          foreach (var flash in flashed)
          {
            map[flash.Y][flash.X] = 0;
          }
        }
        else
        {
          for (int y = 0; y < map.Length; y++)
          {
            var line = map[y];

            for (int x = 0; x < line.Length; x++)
            {
              if (line[x] > 9)
              {
                line[x] = 0;
              }
              Console.Write($"{line[x]}");
            }
            Console.WriteLine();
          }
          Console.WriteLine();
        }

      }

      return $"{flashes}";
    }
  }
}
