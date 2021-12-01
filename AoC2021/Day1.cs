namespace AoC2022
{
  [AoCChallenge(1, 2021)]
  public class Day1 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var last = int.Parse(input.ElementAt(0));
      var count = 0;
      foreach(var part in input.Skip(1))
      {
        var current = int.Parse(part);
        if (current > last)
          count++;
        last = current;
      }
      return $"{count}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      Queue<int> nums = new Queue<int>();
      nums.Enqueue(0);
      nums.Enqueue(0);
      nums.Enqueue(0);
      var sum = 0;

      List<string> output = new List<string>();

      foreach (var part in input)
      {
        var current = int.Parse(part);
        sum += current;
        nums.Enqueue(current);
        sum -= nums.Dequeue();
        output.Add($"{sum}");
      }

      return RunPart1(output.Skip(2));
    }
  }
}