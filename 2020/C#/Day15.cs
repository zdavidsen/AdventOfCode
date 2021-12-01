using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{

    public static class Day15
    {
        public static void Part2(string[] lines)
        {
            int[] init = { 12, 20, 0, 6, 1, 17, 7 };

            Dictionary<int, int> nums = new Dictionary<int, int>();

            for (int i = 0; i < init.Length - 1; i++)
            {
                nums[init[i]] = i;
            }

            var lastNum = init.Last();

            for (int i = init.Length - 1; i < 30000000 - 1; i++)
            {
                int newNum;

                if (nums.TryGetValue(lastNum, out newNum))
                    newNum = i - newNum;
                else
                    newNum = 0;

                nums[lastNum] = i;

                lastNum = newNum;
            }

            Console.WriteLine($"{lastNum}");
        }
    }
}
