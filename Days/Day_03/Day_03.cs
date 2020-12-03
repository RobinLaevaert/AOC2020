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
            var lines = Read_file().ToList();
            var lines_count = lines.Count();
            var line_length = lines.First().Length;
            while(line_length < 7 * lines_count)
            {
                lines = lines.Select(x => x += x).ToList();
                line_length = lines.First().Length;
            }
            field = lines;
        }

        public override void Part1()
        {
            Console.WriteLine($"Trees encountered: {Get_trees_in_slope(3, 1)}");
        }

        public override void Part2()
        {
            long trees_1_1 = Get_trees_in_slope(1, 1);
            long trees_3_1 = Get_trees_in_slope(3, 1);
            long trees_5_1 = Get_trees_in_slope(5, 1);
            long trees_7_1 = Get_trees_in_slope(7, 1);
            long trees_1_2 = Get_trees_in_slope(1, 2);

            Console.WriteLine($"Product of trees encountered: {trees_1_1 * trees_3_1 * trees_5_1 * trees_7_1 * trees_1_2}");
        }

        public int Get_trees_in_slope(int vX, int vY)
        {
            var x = 0;
            var y = 0;
            var trees_count = 0;
            while (y < field.Count)
            {
                if (field[y][x] == '#') trees_count++;
                x += vX;
                y += vY;
            }
            return trees_count;
        }
    }
}
