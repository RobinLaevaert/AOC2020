using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Days
{
    public class Day_01 : Day
    {
        public List<int> input;
        public Day_01()
        {
            DayNumber = 1;
            Title = "Report Repair";
        }
        public override void Part1()
        {
            var first = input.First(x => input.Any(y => x + y == 2020));
            var result = first * input.Single(x => x == 2020 - first);
            Console.WriteLine(result);
        }

        public override void Part2()
        {
            var first = input.First(x => input.Any(y => input.Any(z => x+y+z == 2020)));
            var second = input.First(x => input.Any(z => first + x + z == 2020));
            var third = input.First(x => first + second + x == 2020);
            var result = first * second * third;
            Console.WriteLine(result);
        }

        public override void Gather_input()
        {
            input = Read_file().Select(int.Parse).ToList();
        }
    }
}
