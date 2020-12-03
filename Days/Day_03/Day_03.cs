using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_03 : Day
    {
        List<string> field;
        private int width; 
        public Day_03()
        {
            DayNumber = 3;
            Title = "Toboggan Trajectory";
        }
        public override void Gather_input()
        {
            var lines = Read_file();
            width = lines.First().Length;
            field = lines.ToList();
        }

        public override void Part1() => Console.WriteLine($"Trees encountered: {Get_trees_in_slope(3, 1)}");

        public override void Part2()
        {
            var trees_1_1 = Get_trees_in_slope(1, 1);
            var trees_3_1 = Get_trees_in_slope(3, 1);
            var trees_5_1 = Get_trees_in_slope(5, 1);
            var trees_7_1 = Get_trees_in_slope(7, 1);
            var trees_1_2 = Get_trees_in_slope(1, 2);

            Console.WriteLine($"Product of trees encountered: {(long)trees_1_1 * trees_3_1 * trees_5_1 * trees_7_1 * trees_1_2}");
        }

        public int Get_trees_in_slope(int vX, int vY) => field.Select((line, index) => line[(vX * index / vY)%width]).Where((x, index) => index % vY == 0).Count(x => x == '#');
    }
}
