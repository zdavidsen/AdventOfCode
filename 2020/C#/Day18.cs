using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    public static class Day18
    {
        public static void Part1(string[] lines)
        {
            long val = 0;
            //*/
            foreach (var line in lines)
            {
                var l = line.Replace(" ", "");
                int index = 0;
                val += ParseSubExpression1(l, ref index);
            }
            /*/
            int index = 0;
            val = ParseSubExpression("1 + 2 * 3 + 4 * 5 + 6".Replace(" ", ""), ref index);
            //*/
            Console.WriteLine($"{val}");
        }

        private static long ParseSubExpression1(string line, ref int index)
        {
            long val = line[index++];

            if (val == '(')
                val = ParseSubExpression1(line, ref index);
            else
                val -= 0x30;

            while (index < line.Length)
            {
                var op = line[index++];

                if (op == ')')
                    return val;

                long arg = line[index++];

                if (arg == '(')
                    arg = ParseSubExpression1(line, ref index);
                else
                    arg -= 0x30;

                if (op == '+')
                    val += arg;
                else
                    val *= arg;
            }

            return val;
        }

        public static void Part2(string[] lines)
        {
            long val = 0;
            //*/
            foreach (var line in lines)
            {
                var l = line.Replace(" ", "");
                int index = 0;
                val += ParseSubExpression2(l, ref index);
            }
            /*/
            int index = 0;
            val = ParseSubExpression2("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2".Replace(" ", ""), ref index);
            //*/
            Console.WriteLine($"{val}");
        }

        //private static long ParseSubExpression2(string line, ref int index)
        //{
        //    long val = line[index++];

        //    if (val == '(')
        //        val = ParseSubExpression2(line, ref index);
        //    else
        //        val -= 0x30;

        //    while (index < line.Length)
        //    {
        //        var op = line[index++];
        //        long arg;

        //        if (op == ')')
        //            return val;
        //        else if (op == '*')
        //        {

        //            arg = ParseSubExpression2(line.Substring(0, line.IndexOf(')', index)), ref index);
        //        }
        //        else
        //        {
        //            arg = line[index++];

        //            if (arg == '(')
        //                arg = ParseSubExpression2(line, ref index);
        //            else
        //                arg -= 0x30;
        //        }

        //        if (op == '+')
        //            val += arg;
        //        else
        //            val *= arg;
        //    }

        //    return val;
        //}

        private static long ParseSubExpression2(string line, ref int index)
        {
            List<long> mults = new List<long>();
            long val = line[index++];

            if (val == '(')
                val = ParseSubExpression2(line, ref index);
            else
                val -= 0x30;

            while (index < line.Length)
            {
                var op = line[index++];

                if (op == ')')
                    break;

                long arg = line[index++];

                if (arg == '(')
                    arg = ParseSubExpression2(line, ref index);
                else
                    arg -= 0x30;

                if (op == '+')
                    val += arg;
                else
                {
                    mults.Add(val);
                    val = arg;
                }
            }

            foreach (var mult in mults)
            {
                val *= mult;
            }

            return val;
        }
    }
}
