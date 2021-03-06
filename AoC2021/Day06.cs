namespace AoC2022
{
  [AoCChallenge(6, 2021)]
  public class Day6 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var fish = input.First().Split(",").Select(int.Parse);

      for (int i = 0; i < 80; i++)
      {
        fish = fish.Select(f => --f);
        foreach (var f in fish.Where(f => f == -1))
        {
          fish = fish.Append(6);
          fish = fish.Append(8);
        }
        fish = fish.Where(f => f >= 0); 
      }
      
      return $"{fish.Count()}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      //var fish = input.First().Split(",").Select(sbyte.Parse).ToArray();
      //var fishCount = fish.Length;

      //for (int _ = 0; _ < 256; _++)
      //{
      //  var startingFishCount = fishCount;
      //  for (int i = 0; i < startingFishCount; i++)
      //  {r
      //    if (--fish[i] == -1)
      //    {
      //      fish[i] = 6;
      //      if (fishCount + 1 >= fish.Length)
      //      {
      //        var newFish = new sbyte[fish.Length * 2];
      //        Array.Copy(fish, 0, newFish, 0, fish.Length);
      //        fish = newFish;
      //      }
      //      fish[fishCount++] = 8;
      //    }
      //  }
      //}

      //return $"{fish.Count()}";

      // I'm sure I could do some calculus/other math and find a nice closed form solution,
      //  but I really just don't want to put that kind of effort into this right now

      // Inspiration for this approach goes to reddit.com/r/adventofcode.
      // I was looking for the closed form solution (since I was doing this almost a day late)
      //  and I either saw this exactly, or something similar enough that it inspired me

      var fishInput = input.First().Split(",").Select(sbyte.Parse).ToArray();
      var fish = new long[9];

      for (int i = 0; i < 7; i++)
      {
        fish[i] = fishInput.Count(f => f == i);
      }

      for (int _ = 0; _ < 256; _++)
      {
        var reproducing = fish[0];
        for (int i = 1; i < 9; i++)
        {
          fish[i - 1] = fish[i];
        }
        fish[6] += reproducing;
        fish[8] = reproducing;
      }

      var count = fish.Aggregate((long)0, (l, i) => l + i);

      return $"{count}";
    }
  }
}
