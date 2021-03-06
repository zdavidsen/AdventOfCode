namespace AoC2022
{
  [AoCChallenge(14, 2021)]
  public class Day14 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var polymer = input.First().AsEnumerable().ToList();

      Dictionary<string, char> rules = new();

      foreach (var line in input.Skip(2))
      {
        var parts = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        rules[parts[0]] = parts[1][0];
      }

      for (int _ = 0; _ < 10; _++)
      {
        for (int index = polymer.Count() - 2; index >= 0; index--)
        {
          var sequence = polymer.Skip(index).Take(2).Aggregate("", (s, c) => s + c);
          if (rules.ContainsKey(sequence))
          {
            polymer.Insert(index + 1, rules[sequence]);
          }
        }
      }

      Dictionary<char, int> counts = new();

      foreach (var element in polymer)
      {
        if (!counts.ContainsKey(element))
          counts[element] = 0;
        counts[element]++;
      }

      var orderedCounts = counts.Values.OrderByDescending(x => x);

      var result = orderedCounts.First() - orderedCounts.Last();
      
      return $"{result}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var polymer = new LinkedList<char>(input.First().AsEnumerable());

      Dictionary<string, char> rules = new();

      foreach (var line in input.Skip(2))
      {
        var parts = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        rules[parts[0]] = parts[1][0];
      }

      var letters = new Dictionary<char, long>();
      foreach (var letter in polymer)
      {
        if (!letters.ContainsKey(letter))
          letters[letter] = 0;
        letters[letter]++;
      }

      Dictionary<string, List<(string result, char added)>> transforms = new();

      foreach (var rule in rules)
      {
        transforms[rule.Key] = new();
        var trans = new string[] { $"{rule.Key[0]}{rule.Value}", $"{rule.Value}{rule.Key[1]}" };

        foreach (var tran in trans)
        {
          if (rules.ContainsKey(tran))
            transforms[rule.Key].Add((tran, rule.Value));
        }
      }

      var pairs = new Dictionary<string, long>();

      foreach (var rule in rules.Keys)
        pairs[rule] = 0;

      for (int i = 0; i < polymer.Count - 1; i++)
      {
        var sub = polymer.Skip(i).Take(2).Aggregate("", (s, c) => s + c);
        if (pairs.ContainsKey(sub))
          pairs[sub]++;
      }

      pairs = Expand(pairs, 0);

      //var orderedCounts = pairs.Values.OrderByDescending(x => x);
      var orderedCounts = letters.Values.OrderByDescending(x => x);

      var result = orderedCounts.First() - orderedCounts.Last();

      return $"{result}";

      Dictionary<string, long> Expand(Dictionary<string, long> charPairs, int depth)
      {
        if (depth >= 40)
          return charPairs;

        Dictionary<string, long> newPairs = new();

        foreach (var pair in charPairs)
        {
          if (transforms[pair.Key].Count > 0)
          {
            var added = transforms[pair.Key][0].added;
            if (!letters.ContainsKey(added))
              letters[added] = 0;
            letters[added] += pair.Value;
          }
          foreach (var transform in transforms[pair.Key])
          {
            if (!newPairs.ContainsKey(transform.result))
              newPairs[transform.result] = 0;
            newPairs[transform.result] += pair.Value;
          }
        }

        return Expand(newPairs, depth + 1);
      }

      //for (int _ = 0; _ < 40; _++)
      //{
      //  var current = polymer.Last.Previous;
      //  while (current != null)
      //  {
      //    var sequence = $"{current.Value}{current.Next.Value}";
      //    if (rules.ContainsKey(sequence))
      //    {
      //      polymer.AddAfter(current, rules[sequence]);
      //    }
      //    current = current.Previous;
      //  }
      //}

      //Dictionary<char, int> counts = new();

      //foreach (var element in polymer)
      //{
      //  if (!counts.ContainsKey(element))
      //    counts[element] = 0;
      //  counts[element]++;
      //}

      //var orderedCounts = counts.Values.OrderByDescending(x => x);

      //var result = orderedCounts.First() - orderedCounts.Last();

      //return $"{result}";
    }
  }
}
