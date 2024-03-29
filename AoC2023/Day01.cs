namespace AoC2022
{
  [AoCChallenge(1, 2023)]
  public class Day1 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var sum = 0;
      foreach (var line in input)
      {
        var first = line.First(c => '0' <= c && c <= '9');
        var last = line.Last(c => '0' <= c && c <= '9');
        sum += int.Parse($"{first}{last}");
      }
      return $"{sum}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var digits = 
        new Dictionary<string, string> 
        {
          ["0"] = "0", ["1"] = "1", ["2"] = "2", ["3"] = "3", ["4"] = "4", 
          ["5"] = "5", ["6"] = "6", ["7"] = "7", ["8"] = "8", ["9"] = "9",
          ["one"] = "1", ["two"] = "2", ["three"] = "3", ["four"] = "4", 
          ["five"] = "5", ["six"] = "6", ["seven"] = "7", ["eight"] = "8", ["nine"] = "9"
        };
      var sum = 0;
      foreach (var line in input)
      {
        string first = "";
        int firstIndex = int.MaxValue;
        string last = "";
        int lastIndex = int.MinValue;
        foreach (var digit in digits)
        {
          var f = line.IndexOf(digit.Key);
          if (f >= 0 && f < firstIndex)
          {
            first = digit.Value;
            firstIndex = f;
          }
          var l = line.LastIndexOf(digit.Key);
          if (l >= 0 && l > lastIndex)
          {
            last = digit.Value;
            lastIndex = l;
          }
        }
        sum += int.Parse($"{first}{last}");
      }
      return $"{sum}";
    }
  }
}
