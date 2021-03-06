namespace AoC2022
{
  [AoCChallenge(15, 2021)]
  public class Day15 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      PriorityQueue<List<(int X, int Y, int risk)>, int> paths = new();

      var map =
        input.Select(
          l => l.Select(c => (c - 0x30, false)).ToArray())
        .ToArray();

      paths.Enqueue(new List<(int X, int Y, int risk)> { (0, 0, 0) }, 0);

      while (true)
      {
        var path = paths.Dequeue();
        var position = path.Last();

        if (position.X == map[0].Length - 1 && position.Y == map.Length - 1)
          return $"{position.risk}";

        map[position.Y][position.X].Item2 = true;

        (int X, int Y) up = (position.X, position.Y - 1);
        (int X, int Y) down = (position.X, position.Y + 1);
        (int X, int Y) left = (position.X - 1, position.Y);
        (int X, int Y) right = (position.X + 1, position.Y);

        var neighbors = new List<(int X, int Y)>();

        if (up.Y >= 0) neighbors.Add(up);
        if (down.Y < map.Length) neighbors.Add(down);
        if (left.X >= 0) neighbors.Add(left);
        if (right.X < map[0].Length) neighbors.Add(right);

        foreach (var neighbor in neighbors)
        {
          if (map[neighbor.Y][neighbor.X].Item2)
            continue;
          var risk = position.risk + map[neighbor.Y][neighbor.X].Item1;
          var cost = risk + (map.Length - neighbor.Y) + (map[0].Length - neighbor.X);
          paths.Enqueue(path.Append((neighbor.X, neighbor.Y, risk)).ToList(), cost);
        }

        //for (int x = -1; x <= 1; x++)
        //{
        //  for (int y = -1; y <= 1; y++)
        //  {
        //    if (position.Y + y >= 0 &&
        //        position.Y + y < map.Length &&
        //        position.X + x >= 0 &&
        //        position.X + x < map[0].Length &&
        //        !(x == 0 && y == 0))
        //    {
        //      int newX = position.X + x, newY = position.Y + y;
        //      var risk = position.risk + map[newY][newX];
        //      var cost = risk + (map.Length - newY) + (map[0].Length - newX);
        //      paths.Enqueue(path.Append((newX, newY, risk)).ToList(), cost);
        //    }
        //  }
        //}
      }  

      return $"";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      PriorityQueue<List<(int X, int Y, int risk)>, int> paths = new();

      var initMap =
        input.Select(
          l => l.Select(c => c - 0x31).ToArray())
        .ToArray();

      var map = new (int risk, bool visited)[initMap.Length * 5][];
      for (int i = 0; i < map.Length; i++)
        map[i] = new (int risk, bool visited)[initMap[0].Length * 5];

      for (int tileY = 0; tileY < 5; tileY++)
      {
        for (int tileX = 0; tileX < 5; tileX++)
        {
          for (int y = 0; y < initMap.Length; y++)
          {
            for (int x = 0; x < initMap[0].Length; x++)
            {
              var risk = (initMap[y][x] + tileY + tileX) % 9 + 1;
              map[tileY * initMap.Length + y][tileX * initMap[0].Length + x] = (risk, false);
            }
          }
        }
      }

      //for (int y = 0; y < map.Length; y++)
      //{
      //  for (int x = 0; x < map[0].Length; x++)
      //  {
      //    Console.Write(map[y][x].risk);
      //  }
      //  Console.WriteLine();
      //}

      paths.Enqueue(new List<(int X, int Y, int risk)> { (0, 0, 0) }, 0);

      while (true)
      {
        var path = paths.Dequeue();
        var position = path.Last();

        if (position.X == map[0].Length - 1 && position.Y == map.Length - 1)
        {
          //PrintPath(map, path);
          return $"{position.risk}";
        }

        map[position.Y][position.X].Item2 = true;

        (int X, int Y) up = (position.X, position.Y - 1);
        (int X, int Y) down = (position.X, position.Y + 1);
        (int X, int Y) left = (position.X - 1, position.Y);
        (int X, int Y) right = (position.X + 1, position.Y);

        var neighbors = new List<(int X, int Y)>();

        if (up.Y >= 0) neighbors.Add(up);
        if (down.Y < map.Length) neighbors.Add(down);
        if (left.X >= 0) neighbors.Add(left);
        if (right.X < map[0].Length) neighbors.Add(right);

        foreach (var neighbor in neighbors)
        {
          if (map[neighbor.Y][neighbor.X].Item2)
            continue;
          var risk = position.risk + map[neighbor.Y][neighbor.X].Item1;
          var cost = risk + (map.Length - neighbor.Y) + (map[0].Length - neighbor.X);
          paths.Enqueue(path.Append((neighbor.X, neighbor.Y, risk)).ToList(), cost);
        }
      }

      return $"";
    }

    private void PrintPath((int risk, bool)[][] map, List<(int X, int Y, int risk)> path)
    {
      var pathCoords = path.Select(p => (p.X, p.Y));

      for (int y = 0; y < map.Length; y++)
      {
        for (int x = 0; x < map[0].Length; x++)
        {
          if (pathCoords.Any(c => c == (x, y)))
            Console.Write(' ');
          else
            Console.Write(map[y][x].risk);
        }
        Console.WriteLine();
      }
    }
  }
}
