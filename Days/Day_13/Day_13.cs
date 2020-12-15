using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_13 : Day
    {
        private int starting_point;
        private List<string> instructions;
        public Day_13()
        {
            Title = "Shuttle Search";
            DayNumber = 13;
        }

        protected override void Gather_input()
        {
            var temp = Read_file().ToList();
            starting_point = int.Parse(temp.First());
            instructions = temp.Last().Split(',').ToList();
        }

        protected override void Part1()
        {
            var closest_departure = instructions.Where(x => x != "x")
                                                .Select(x => 
                                                {
                                                    var numeric_x = long.Parse(x);
                                                    return new { Id = numeric_x, Departure = Math.Ceiling((double)starting_point / numeric_x) * numeric_x };
                                                })
                                                .OrderBy(x => x.Departure).First();
            var result = (closest_departure.Departure - starting_point) * closest_departure.Id;
            Console.WriteLine(result);
        }

        protected override void Part2()
        {
            var numbers = instructions.Select((x, i) => new { Id = x, Delta = i })
                                .Where(x => x.Id != "x")
                                .Select(x => long.Parse(x.Id))
                                .ToArray();
            var remainder = instructions.Select((x, i) => new { Id = x, Delta = i })
                                .Where(x => x.Id != "x")
                                .Select(x => long.Parse(x.Id) - x.Delta)
                                .ToArray();
            var result = Chinese_remainder_theorem.Solve(numbers, remainder);
            Console.WriteLine(result);
        }
    }

    public static class Chinese_remainder_theorem
    {
        public static long Solve(long[] n, long[] a)
        {
            long prod = n.Aggregate(1, (long i, long j) => i * j);
            long p;
            long sm = 0;
            for (long i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * Modular_multiplicative_inverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static long Modular_multiplicative_inverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}
