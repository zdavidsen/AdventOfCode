namespace AoC2022
{
  [AoCChallenge(8, 2021)]
  public class Day8 : IAoCChallenge
  {
    public bool Part2Ready => true;



    public string RunPart1(IEnumerable<string> input)
    {
      var value = 0;

      foreach (var part in input)
      {
        value += part.Split("|")[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Count(p => p.Length == 2 || p.Length == 4 || p.Length == 7 || p.Length == 3);
      }
      
      return $"{value}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var value = 0;
      var mults = new int[] { 1000, 100, 10, 1 };

      foreach (var part in input)
      {
        var splits = part.Split("|");
        var patterns = splits[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var outputs = splits[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        byte[] vals = new byte[4];

        bool needToDeduce = true;

        //for (int i = 0; i < 4; i++)
        //{
        //  switch (outputs[i].Length)
        //  {
        //    case 2: vals[i] = 1; break;
        //    case 4: vals[i] = 4; break;
        //    case 3: vals[i] = 7; break;
        //    case 7: vals[i] = 8; break;
        //    default:
        //      needToDeduce = true;
        //      break;
        //  }
        //}

        //if (needToDeduce)
        //{
        var one = patterns.First(p => p.Length == 2);
        var four = patterns.First(p => p.Length == 4);
        var seven = patterns.First(p => p.Length == 3);
        var eight = patterns.First(p => p.Length == 7);

        var l6 = patterns.Where(p => p.Length == 6);

        var six = l6.First(p => !one.All(c => p.Contains(c)));
        var nine = l6.First(p => four.All(c => p.Contains(c)));
        var zero = l6.First(p => p != six && p != nine);

        var l5 = patterns.Where(p => p.Length == 5);

        var two = l5.First(p => nine.Count(c => p.Contains(c)) == 4);
        var three = l5.First(p => p != two && six.Count(c => p.Contains(c)) == 4);
        var five = l5.First(p => six.Count(c => p.Contains(c)) == 5);
        //var three = l5.First(p => nine.Count(c => p.Contains(c)) == 5);
        //var two = l5.First(p => p != three && six.Count(c => p.Contains(c)) == 4);
        //var five = l5.First(p => p != three && p != two);

        for (int i = 0; i < 4; i++)
        {
          if (Comp(outputs[i], zero))
            vals[i] = 0;
          else if (Comp(outputs[i], one))
            vals[i] = 1;
          else if (Comp(outputs[i], two))
            vals[i] = 2;
          else if (Comp(outputs[i], three))
              vals[i] = 3;
          else if (Comp(outputs[i], four))
            vals[i] = 4;
          else if (Comp(outputs[i], five))
            vals[i] = 5;
          else if (Comp(outputs[i], six))
            vals[i] = 6;
          else if (Comp(outputs[i], seven))
            vals[i] = 7;
          else if (Comp(outputs[i], eight))
            vals[i] = 8;
          else if (Comp(outputs[i], nine))
            vals[i] = 9;
        }
        //}

        for (int i = 0; i < 4; i++)
          value += vals[i] * mults[i];
      }
      
      return $"{value}";
    }

    bool Comp (string s1, string s2)
    {
      return s1.Length == s2.Length &&
        s1.All(c => s2.Contains(c));
    }
  }
}
