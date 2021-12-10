namespace AoC2022
{
  [AoCChallenge(3, 2021)]
  public class Day3 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var gamma = new int[input.First().Length];

      foreach (var part in input)
      {
        for (int i = 0; i < gamma.Length; i++)
        {
          gamma[i] += part[i] == '0' ? 0 : 1;
        }
      }

      var actualGamma = 0;
      var epsilon = 0;

      for (int i = 0; i < gamma.Length; i++)
      {
        if (gamma[gamma.Length - i - 1] >= input.Count() / 2)
        {
          actualGamma += 1 << i;
        }
        else
        {
          epsilon += 1 << i;
        }
      }

      return $"{actualGamma * epsilon}";
    }

    private void Partition(IEnumerable<string> input, int index, out IEnumerable<string> oxygen, out IEnumerable<string> co2)
    {
      var gamma = new int[input.First().Length];

      foreach (var part in input)
      {
        for (int i = 0; i < gamma.Length; i++)
        {
          gamma[i] += part[i] == '0' ? 0 : 1;
        }
      }

      var selector = new bool?[gamma.Length];

      for (int i = 0; i < gamma.Length; i++)
      {
        if (gamma[i] == input.Count() / 2.0)
        {
          selector[i] = null;
        }
        else if (gamma[i] > input.Count() / 2.0)
          selector[i] = true;
        else
        {
          selector[i] = false;
        }
      }

      oxygen = new List<string>();
      co2 = new List<string>();

      foreach (var part in input)
      {
        if (selector[index] == null)
        {
          if (part[index] == '0')
            co2 = co2.Append(part);
          else
            oxygen = oxygen.Append(part);
        }
        else if (selector[index] == (part[index] == '1'))
          oxygen = oxygen.Append(part);
        else
          co2 = co2.Append(part);
      }
    }

    public string RunPart2(IEnumerable<string> input)
    {
      Partition(input, 0, out IEnumerable<string> oxygen, out IEnumerable<string> co2);

      for (int i = 1; i < input.First().Length; i++)
      {
        if (oxygen.Count() > 1)
          Partition(oxygen, i, out oxygen, out _);
        if (co2.Count() > 1)
          Partition(co2, i, out _, out co2);
      }

      var o2 = 0;
      var c2 = 0;

      for (int i = 0; i < oxygen.First().Length; i++)
      {
        if (oxygen.First()[oxygen.First().Length - i - 1] == '1')
          o2 += 1 << i;
        if (co2.First()[co2.First().Length - i - 1] == '1')
          c2 += 1 << i;
      }

      return $"{o2 * c2}";
    }
  }
}