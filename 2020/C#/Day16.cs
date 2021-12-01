using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC
{
    public static class Day16
    {
        public static void Part1(string[] lines)
        {
            int i = 0;

            List<Rule> rules = new List<Rule>();

            for (; lines[i] != ""; i++)
            {
                var match = Regex.Match(lines[i], @"([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)");

                if (!match.Success)
                    continue; //?????

                rules.Add(
                    new Rule
                    {
                        Name = match.Groups[1].Value,
                        Ranges =
                            new List<Range>
                            {
                                new Range { Min = int.Parse(match.Groups[2].Value), Max = int.Parse(match.Groups[3].Value) },
                                new Range { Min = int.Parse(match.Groups[4].Value), Max = int.Parse(match.Groups[5].Value) }
                            }
                    });
            }

            i += 2;

            // Parse own ticket

            i += 3;

            var ticketCount = 0;
            var invalidRate = 0;

            for (; i < lines.Length; i++, ticketCount++)
            {
                var vals = lines[i].Split(',');

                foreach (var val in vals)
                {
                    int iVal = int.Parse(val);

                    bool passes = false;

                    foreach (var rule in rules)
                    {
                        passes |= rule.Ranges.Any(r => r.Contains(iVal));
                    }

                    if (!passes)
                        invalidRate += iVal;
                }
            }

            Console.WriteLine($"{invalidRate}");
        }

        public static void Part2(string[] lines)
        {
            int i = 0;

            List<Rule> rules = new List<Rule>();

            for (; lines[i] != ""; i++)
            {
                var match = Regex.Match(lines[i], @"([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)");

                if (!match.Success)
                    continue; //?????

                rules.Add(
                    new Rule
                    {
                        Name = match.Groups[1].Value,
                        Ranges =
                            new List<Range>
                            {
                                new Range { Min = int.Parse(match.Groups[2].Value), Max = int.Parse(match.Groups[3].Value) },
                                new Range { Min = int.Parse(match.Groups[4].Value), Max = int.Parse(match.Groups[5].Value) }
                            }
                    });
            }

            i += 2;

            var myTicket = lines[i].Split(',').Select(v => int.Parse(v)).ToList();

            i += 3;

            var fields =
                lines
                    .Skip(i)
                    .Select(l => l.Split(',').Select(v => int.Parse(v)))
                    .Where(t => t.All(v => rules.Any(r => r.Contains(v))))
                    .Union(new List<IEnumerable<int>> { myTicket })
                    .SelectMany(t => t.Select((val, index) => (val, index)))
                    .GroupBy(f => f.index);

            var fieldQueue = new Queue<IGrouping<int, (int, int)>>(fields);
            var assocs = new List<Rule>();

            while (fieldQueue.Count > 0)
            {
                var field = fieldQueue.Dequeue();
                var matches = rules.Where(r => !assocs.Any(r2 => r.Name == r2.Name) && field.All(v => r.Contains(v.Item1)));

                if (matches.Count() != 1)
                {
                    fieldQueue.Enqueue(field);
                    continue;
                }

                var rule = matches.First();
                rule.Index = field.Key;
                assocs.Add(rule);
            }

            var departs =
                assocs
                    .Where(r => r.Name.StartsWith("departure"));

            var result =
                assocs
                    .Where(r => r.Name.StartsWith("departure"))
                    .Select(r => myTicket[r.Index])
                    .Aggregate(1L, (a1, a2) => a1 * a2);

            Console.WriteLine($"{result}");
        }

        private struct Rule
        {
            public string Name;
            public List<Range> Ranges;
            public int Index;

            public bool Contains(int num)
            {
                return Ranges.Any(r => r.Contains(num));
            }
        }

        private struct Range
        {
            public int Min;
            public int Max;

            public bool Contains(int num)
            {
                return Min <= num && num <= Max;
            }
        }
    }
}
