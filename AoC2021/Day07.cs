namespace AoC2022
{
  [AoCChallenge(7, 2021)]
  public class Day7 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var positions = input.First().Split(",").Select(int.Parse);

      var avg = (int)Math.Round(positions.Sum() / (float) positions.Count());

      var tests = new List<int>();

      for (int i = -positions.Count() / 2; i < positions.Count() / 2; i++)
      {
        tests.Add(avg + i);
      }

      var fuel = tests.Select(t => positions.Sum(p => Math.Abs(p - t))).Min();
      
      return $"{fuel}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var positions = input.First().Split(",").Select(int.Parse);

      var avg = (int)Math.Round(positions.Sum() / (float)positions.Count());

      var tests = new List<int>();

      for (int i = -positions.Count() / 2; i < positions.Count() / 2; i++)
      {
        tests.Add(avg + i);
      }

      var fuel = tests.Select(t => positions.Sum(p => Math.Abs(p - t) * (Math.Abs(p - t) + 1) / 2)).Min();

      return $"{fuel}";
    }
  }
}
