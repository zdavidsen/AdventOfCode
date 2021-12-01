using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            int dayNum = 18;

            string[] lines =
                //*/
                System.IO.File.ReadAllLines($"../../../day{dayNum}input.txt");
                /*/
                @".#.
..#
###".Replace("\r", "").Split('\n');
                //*/

            var dayType = Assembly.GetExecutingAssembly().GetType($"AoC.Day{dayNum}");

            if (dayType.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(m => m.Name == "Part1"))
                dayType.InvokeMember("Part1", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new[] { lines });

            if (dayType.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(m => m.Name == "Part2"))
                dayType.InvokeMember("Part2", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new[] { lines });

            Console.ReadKey();
        }
    }



}
