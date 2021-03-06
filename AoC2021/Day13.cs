namespace AoC2022
{
  [AoCChallenge(13, 2021)]
  public class Day13 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      Dictionary<(int X, int Y), bool> dotMatrix = new();

      var enumer = input.GetEnumerator();

      while (enumer.MoveNext())
      {
        if (enumer.Current == "")
          break;

        var splits = enumer.Current.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        dotMatrix[(splits.First(), splits.Last())] = true;
      }

      //PrintMatrix(dotMatrix);

      while (enumer.MoveNext())
      {
        var instruction = enumer.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries)[2];
        var dir = instruction[0];
        var coord = int.Parse(instruction.Substring(2));

        if (dir == 'y')
        {
          var dots = dotMatrix.Where(d => d.Value && d.Key.Y > coord).Select(d => d.Key).ToList();
          foreach (var dot in dots)
          {
            dotMatrix[dot] = false;
            var newY = coord - (dot.Y - coord);
            dotMatrix[(dot.X, newY)] = true;
          }
        }
        else if (dir == 'x')
        {
          var dots = dotMatrix.Where(d => d.Value && d.Key.X > coord).Select(d => d.Key).ToList();
          foreach (var dot in dots)
          {
            dotMatrix[dot] = false;
            var newX = coord - (dot.X - coord);
            dotMatrix[(newX, dot.Y)] = true;
          }
        }
        //PrintMatrix(dotMatrix);
        break;
      }
      
      return $"{dotMatrix.Values.Count(v => v)}";
    }

    private void PrintMatrix(Dictionary<(int X, int Y), bool> dotMatrix)
    {
      var maxX = dotMatrix.Where(d => d.Value).Max(d => d.Key.X);
      var maxY = dotMatrix.Where(d => d.Value).Max(d => d.Key.Y);
      for (int y = 0; y <= maxY; y++)
      {
        for (int x = 0; x <= maxX; x++)
        {
          if (dotMatrix.ContainsKey((x, y)) && dotMatrix[(x, y)])
            Console.Write("#");
          else
            Console.Write(".");
        }
        Console.WriteLine();
      }
      Console.WriteLine();
    }

    public string RunPart2(IEnumerable<string> input)
    {
      Dictionary<(int X, int Y), bool> dotMatrix = new();

      var enumer = input.GetEnumerator();

      while (enumer.MoveNext())
      {
        if (enumer.Current == "")
          break;

        var splits = enumer.Current.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        dotMatrix[(splits.First(), splits.Last())] = true;
      }

      //PrintMatrix(dotMatrix);

      while (enumer.MoveNext())
      {
        var instruction = enumer.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries)[2];
        var dir = instruction[0];
        var coord = int.Parse(instruction.Substring(2));

        if (dir == 'y')
        {
          var dots = dotMatrix.Where(d => d.Value && d.Key.Y > coord).Select(d => d.Key).ToList();
          foreach (var dot in dots)
          {
            dotMatrix[dot] = false;
            var newY = coord - (dot.Y - coord);
            dotMatrix[(dot.X, newY)] = true;
          }
        }
        else if (dir == 'x')
        {
          var dots = dotMatrix.Where(d => d.Value && d.Key.X > coord).Select(d => d.Key).ToList();
          foreach (var dot in dots)
          {
            dotMatrix[dot] = false;
            var newX = coord - (dot.X - coord);
            dotMatrix[(newX, dot.Y)] = true;
          }
        }
      }

      PrintMatrix(dotMatrix);

      return $"{dotMatrix.Values.Count(v => v)}";
    }
  }
}
