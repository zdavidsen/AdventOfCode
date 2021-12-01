using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Framework
{
  public class AoCChallengeAttribute : Attribute
  {
    public int Year { get; }
    public int Day { get; }

    public AoCChallengeAttribute(int day, int year)
    {
      this.Year = year;
      this.Day = day;
    }
  }

  public interface IAoCChallenge
  {
    bool Part2Ready { get; }
    string RunPart1(IEnumerable<string> input);
    string RunPart2(IEnumerable<string> input);
  }
}
