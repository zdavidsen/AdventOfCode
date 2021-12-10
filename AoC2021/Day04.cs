namespace AoC2022
{
  [AoCChallenge(4, 2021)]
  public class Day4 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var nums = input.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

      // I tried to do IEnumerable<IEnumerable<BingoPosition>> but that didn't seem to propagate 
      // marks properly, didn't take time to investigate because trying to be speedy :)
      BingoBoard[] boards =
        string.Join(Environment.NewLine, input.Skip(2))
        .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(
          b =>
              new BingoBoard(
                b.Split(new[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => new BingoPosition(int.Parse(n))).ToArray())).ToArray();

      BingoBoard? bingo = null;
      var winningNum = 0;
  
      foreach (var num in nums)
      {
        winningNum = num;
        foreach (var board in boards)
        {
          foreach (var spot in board.Positions)
          {
            if (spot.Num == num)
            {
              spot.Marked = true;
              for (int i = 0; i < 5; i++)
              {
                bool row = true, col = true;
                for (int j = 0; j < 5; j++)
                {
                  row &= board.Positions[i * 5 + j].Marked;
                  col &= board.Positions[i + j * 5].Marked;
                }

                if (row || col)
                  bingo = board;
              }
            }
            if (bingo != null)
              break;
          }
          if (bingo != null)
            break;
        }
        if (bingo != null)
          break;
      }

      var val =
        bingo!.Positions
          .Where(b => !b.Marked)
          .Aggregate(0, (b1, b2) => b1 + b2.Num);

      return $"{val * winningNum}";
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var nums = input.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

      // I tried to do IEnumerable<IEnumerable<BingoPosition>> but that didn't seem to propagate 
      // marks properly, didn't take time to investigate because trying to be speedy :)
      BingoBoard[] boards =
        string.Join(Environment.NewLine, input.Skip(2))
        .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(
          b =>
              new BingoBoard(
                b.Split(new[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => new BingoPosition(int.Parse(n))).ToArray())).ToArray();

      BingoBoard? bingo = null;
      var winningNum = 0;

      var unmarked = new List<BingoBoard>();

      foreach (var num in nums)
      {
        winningNum = num;
        foreach (var board in boards)
        {
          board.Mark(num);
        }
        var temp = boards.Where(b => !b.IsCompleted);
        if (temp.Count() == 0)
        {
          bingo = unmarked.Last();
          break;
        }
        // Have to force this to a list or else it errors because it's a lazy evaluation
        // that empties as soon as the last board is completed
        unmarked = temp.ToList();
      }

      var val =
        bingo!.Positions
          .Where(b => !b.Marked)
          .Aggregate(0, (b1, b2) => b1 + b2.Num);

      return $"{val * winningNum}";
    }

    public class BingoPosition
    {
      public int Num;
      public bool Marked;

      public BingoPosition(int num)
      {
        this.Num = num;
      }
    }

    public class BingoBoard
    {
      public BingoPosition[] Positions;
      public bool IsCompleted;

      public BingoBoard(BingoPosition[] positions)
      {
        Positions = positions;
      }

      public bool Mark(int num)
      {
        foreach (var spot in Positions)
        {
          if (spot.Num == num)
          {
            spot.Marked = true;
            for (int i = 0; i < 5; i++)
            {
              bool row = true, col = true;
              for (int j = 0; j < 5; j++)
              {
                row &= Positions[i * 5 + j].Marked;
                col &= Positions[i + j * 5].Marked;
              }

              if (row || col)
                return IsCompleted = true;
            }
          }
        }
        return false;
      }
    }
  }
}