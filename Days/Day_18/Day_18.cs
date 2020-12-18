using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Channels;

namespace Days
{
    public class Day_18 : Day
    {
        private List<string> expressions;
        public Day_18()
        {
            Title = "Operation Order";
            DayNumber = 18;
        }

        protected override void Gather_input()
        {
            expressions = Read_file().ToList();
        }

        protected override void Part1()
        {
            Console.WriteLine(expressions.Sum(x => Convert.ToInt64(Calculator.QuickMaffs(x,1))));
        }
        
        protected override void Part2()
        {
            Console.WriteLine(expressions.Sum(x => Convert.ToInt64(Calculator.QuickMaffs(x, 2))));
        }
    }
}
