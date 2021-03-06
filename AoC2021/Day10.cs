namespace AoC2022
{
  [AoCChallenge(10, 2021)]
  public class Day10 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var score = 0;

      foreach (var part in input)
      {
        var index = 0;
        do
        {
          var status = ParseChunk1(part, ref index);
          if (status.code == 1)
          {
            switch (status.errorChar)
            {
              case ']':
                score += 57;
                break;
              case ')':
                score += 3;
                break;
              case '}':
                score += 1197;
                break;
              case '>':
                score += 25137;
                break;
            }
            break;
          }
        } while (index < part.Length);
      }
      
      return $"{score}";
    }

    private Dictionary<char, char> open_close =
      new Dictionary<char, char>
      {
        ['['] = ']',
        ['('] = ')',
        ['{'] = '}',
        ['<'] = '>'
      };

    private (int code, char errorChar) ParseChunk1(string chunk, ref int index)
    {
      var open = chunk[index];

      if (!open_close.ContainsKey(open))
        return (1, open);

      index++;

      while (true)
      {
        if (index >= chunk.Length)
          return (2, '\0');

        if (chunk[index] == open_close[open])
        {
          index++;
          return (0, '\0');
        }

        if (!open_close.ContainsKey(chunk[index]))
          return (1, chunk[index]);

        var status = ParseChunk1(chunk, ref index);
        if (status.code != 0)
          return status;
      } //while (open_close.ContainsKey(chunk[index]));
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var scores = new List<long>();

      foreach (var part in input)
      {
        var score = 0L;
        var index = 0;
        List<char> autocomplete = new();
        do
        {
          var status = ParseChunk2(part, ref index);
          if (status.code == 1)
            break;
          if (status.code == 2)
            autocomplete.AddRange(status.autocomplete);
        } while (index < part.Length);
        foreach (var c in autocomplete)
        {
          switch (c)
          {
            case ']':
              score = score * 5 + 2;
              break;
            case ')':
              score = score * 5 + 1;
              break;
            case '}':
              score = score * 5 + 3;
              break;
            case '>':
              score = score * 5 + 4;
              break;
          }
        }
        //Console.WriteLine($"line: {part}\tautocomplete: {string.Join("", autocomplete)}");
        if (score != 0)
          scores.Add(score);
      }

      scores.Sort();

      return $"{scores[scores.Count / 2]}";
    }

    private (int code, char errorChar, IEnumerable<char> autocomplete) ParseChunk2(string chunk, ref int index)
    {
      var autocomplete = new List<char>();
      var open = chunk[index++];

      if (!open_close.ContainsKey(open))
        return (1, open, autocomplete);

      var close = open_close[open];

      while (true)
      {
        if (index >= chunk.Length)
        {
          autocomplete.Add(open_close[open]);
          return (2, '\0', autocomplete);
        }

        if (chunk[index] == close)
        {
          index++;
          return (0, '\0', autocomplete);
        }

        var status = ParseChunk2(chunk, ref index);
        autocomplete.AddRange(status.autocomplete);
        if (status.code == 1)
          return (status.code, status.errorChar, autocomplete);
      } //while (open_close.ContainsKey(chunk[index]));
    }
  }
}
