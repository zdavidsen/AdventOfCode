using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC
{
    static class Day14
    {
        public static void Part1(string[] lines)
        {
            long zeroMask = -1;
            long oneMask = 0;

            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                var match = Regex.Match(line, "mask = (?<Mask>[10X]{36})");

                if (match.Success)
                {
                    zeroMask = -1;
                    oneMask = 0;

                    var mask = match.Groups["Mask"].Value.Reverse().ToList();

                    long bitmask = 0b1;

                    for (int i = 0; i < 36; i++, bitmask <<= 1)
                    {
                        if (mask[i] == '0')
                            zeroMask &= ~bitmask;
                        if (mask[i] == '1')
                            oneMask |= bitmask;
                    }

                    continue;
                }

                match = Regex.Match(line, @"mem\[(?<Addr>\d+)\] = (?<Value>\d+)");

                long addr = long.Parse(match.Groups["Addr"].Value);
                long value = long.Parse(match.Groups["Value"].Value);

                value &= zeroMask;
                value |= oneMask;

                memory[addr] = value;
            }

            long total = 0;

            foreach (var val in memory.Values)
            {
                total += val;
            }

            Console.WriteLine($"total: {total}");
        }

        public static void Part2(string[] lines)
        {
            long oneMask = 0;
            List<int> floatingBits = new List<int>();

            Dictionary<long, long> memory = new Dictionary<long, long>();

            for (var j = 0; j < lines.Length; j++)
            {
                var line = lines[j];
                var match = Regex.Match(line, "mask = (?<Mask>[10X]{36})");

                if (match.Success)
                {
                    oneMask = 0;
                    floatingBits.Clear();

                    var mask = match.Groups["Mask"].Value.Reverse().ToList();

                    long bitmask = 0b1;

                    for (int i = 0; i < 36; i++, bitmask <<= 1)
                    {
                        if (mask[i] == '1')
                            oneMask |= bitmask;
                        if (mask[i] == 'X')
                            floatingBits.Add(i);
                    }

                    continue;
                }

                match = Regex.Match(line, @"mem\[(?<Addr>\d+)\] = (?<Value>\d+)");

                long addr = long.Parse(match.Groups["Addr"].Value);
                long value = long.Parse(match.Groups["Value"].Value);

                addr |= oneMask;

                ResolveFloating(addr, value, floatingBits, memory);
            }

            long total = 0;

            foreach (var val in memory.Values)
            {
                total += val;
            }

            Console.WriteLine($"total: {total}");
        }

        private static void ResolveFloating(long addr, long value, List<int> floating, Dictionary<long, long> memory, int depth = 0)
        {
            if (depth == floating.Count)
            {
                memory[addr] = value;
                return;
            }

            long mask = 0b1L << floating[depth];

            ResolveFloating(addr & ~mask, value, floating, memory, depth + 1);
            ResolveFloating(addr | mask, value, floating, memory, depth + 1);
        }
    }
}
