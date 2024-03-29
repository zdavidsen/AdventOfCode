namespace AoC2022
{
  [AoCChallenge(17, 2021)]
  public class Day17 : IAoCChallenge
  {
    public bool Part2Ready => false;

    public string RunPart1(IEnumerable<string> input)
    { // TODO: uhhhh it looks like I never finished this?
      var splits = input.First().Split(" ").Skip(2);

      var xCoords = 
        splits
          .ElementAt(0)
          .Trim('x', '=', ',')
          .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
          .Select(s => int.Parse(s))
          .OrderBy(i => i)
          .ToArray();

      var yCoords =
        splits
          .ElementAt(1)
          .Trim('y', '=')
          .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
          .Select(s => int.Parse(s))
          .OrderBy(i => i)
          .ToArray();

      var canUseTriangularX = false;
      var xVel = -1;

      // Start with a very loose upper bound
      for (int steps = (int)Math.Floor(Math.Sqrt(xCoords[1])); ; steps-- )
      {
        var xDistance = steps * (steps + 1) / 2;
        if (xCoords[0] < xDistance && xDistance < xCoords[1])
        {
          canUseTriangularX = true;
          xVel = steps;
          break;
        }
        else if (xDistance < xCoords[0])
          break;
      }

      var yCandidates = new List<int>();

      // There's an assumption in here that both y coordinates are negative
      for (int i = 0; i < Math.Abs(yCoords[0]); i++)
      {

      }

      return $"";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      
      return $"";
    }
  }
}
