using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Day_03 : Day
    {
        List<string> field;
        public Day_03()
        {
            DayNumber = 3;
            Title = "Toboggan Trajectory";
        }
        public override void Gather_input()
        {
            var lines = Read_file();
            while(lines.First().Length < 7 * lines.Count())
            {
                lines = lines.Select(x => x += x).ToList();
            }
            field = lines.ToList();
        }

        public override void Part1()
        {
            Console.WriteLine($"Trees encountered: {field.Select((x, i) => x[3 * i]).Count(x => x == '#')}");
        }

        public override void Part2()
        {
            var trees_1_1 = field.Select((x, i) => x[i]).Count(x => x == '#');
            var trees_3_1 = field.Select((x, i) => x[3 * i]).Count(x => x == '#');
            var trees_5_1 = field.Select((x, i) => x[5 * i]).Count(x => x == '#');
            var trees_7_1 = field.Select((x, i) => x[7 * i]).Count(x => x == '#');
            var trees_1_2 = field.Select((x, i) => x[i / 2]).Where((x, i) => i % 2 == 0).Count(x => x == '#');

            Console.WriteLine($"Product of trees encountered: {(long)trees_1_1 * trees_3_1 * trees_5_1 * trees_7_1 * trees_1_2}");
        }
    }
}
