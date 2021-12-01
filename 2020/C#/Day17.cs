using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    public static class Day17
    {
        public static void Part1(string[] lines)
        {
            const int cycles = 6;

            int dim = lines[0].Length + (cycles + 4) * 2;

            var lastConway = new char[dim, dim, dim];
            var conway = new char[dim, dim, dim];

            var middle = dim / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    lastConway[middle - lines.Length / 2 + i, middle - line.Length / 2 + j, middle] = line[j];
                }
            }

            for (int _ = 0; _ < cycles; _++)
            {
                for (int i = 1; i < dim - 1; i++)
                {
                    for (int j = 1; j < dim - 1; j++)
                    {
                        for (int k = 1; k < dim - 1; k++)
                        {
                            var count = 0;
                            for (var i1 = -1; i1 <= 1; i1++)
                            {
                                for (var j1 = -1; j1 <= 1; j1++)
                                {
                                    for (var k1 = -1; k1 <= 1; k1++)
                                    {
                                        if (i1 == j1 && j1 == k1 && k1 == 0)
                                            continue;
                                        if (lastConway[i + i1, j + j1, k + k1] == '#')
                                            count++;
                                    }
                                }
                            }

                            if (lastConway[i, j, k] == '#' && (count != 2 && count != 3))
                                conway[i, j, k] = '.';
                            else if (lastConway[i, j, k] != '#' && count == 3)
                                conway[i, j, k] = '#';
                            else
                                conway[i, j, k] = lastConway[i, j, k];
                        }
                    }
                }
                var temp = lastConway;
                lastConway = conway;
                conway = temp;
            }

            long activeCount = 0;

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        if (lastConway[i, j, k] == '#')
                            activeCount++;
                    }
                }
            }

            Console.WriteLine($"{activeCount}");
        }

        public static void Part2(string[] lines)
        {
            const int cycles = 6;

            int dim = lines[0].Length + (cycles + 4) * 2;

            var lastConway = new char[dim, dim, dim, dim];
            var conway = new char[dim, dim, dim, dim];

            var middle = dim / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    lastConway[middle - lines.Length / 2 + i, middle - line.Length / 2 + j, middle, middle] = line[j];
                }
            }

            for (int _ = 0; _ < cycles; _++)
            {
                for (int i = 1; i < dim - 1; i++)
                {
                    for (int j = 1; j < dim - 1; j++)
                    {
                        for (int k = 1; k < dim - 1; k++)
                        {
                            for (int l = 1; l < dim - 1; l++)
                            {
                                var count = 0;
                                for (var i1 = -1; i1 <= 1; i1++)
                                {
                                    for (var j1 = -1; j1 <= 1; j1++)
                                    {
                                        for (var k1 = -1; k1 <= 1; k1++)
                                        {
                                            for (var l1 = -1; l1 <= 1; l1++)
                                            {
                                                if (i1 == j1 && j1 == k1 && k1 == 0 && l1 == 0)
                                                    continue;
                                                if (lastConway[i + i1, j + j1, k + k1, l + l1] == '#')
                                                    count++;
                                            }
                                        }
                                    }
                                }

                                if (lastConway[i, j, k, l] == '#' && (count != 2 && count != 3))
                                    conway[i, j, k, l] = '.';
                                else if (lastConway[i, j, k, l] != '#' && count == 3)
                                    conway[i, j, k, l] = '#';
                                else
                                    conway[i, j, k, l] = lastConway[i, j, k, l];
                            }
                        }
                    }
                }
                var temp = lastConway;
                lastConway = conway;
                conway = temp;
            }

            long activeCount = 0;

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        for (int l = 0; l < dim; l++)
                        {
                            if (lastConway[i, j, k, l] == '#')
                                activeCount++;
                        }
                    }
                }
            }

            Console.WriteLine($"{activeCount}");
        }
    }
}
