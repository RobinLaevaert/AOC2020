﻿using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public override void Part1() => Console.WriteLine($"Trees encountered: {Get_trees_in_slope(3, 1)}");

        public override void Part2() => 
            Console.WriteLine($"Product of trees encountered: {(long)Get_trees_in_slope(1, 1) * Get_trees_in_slope(3, 1) * Get_trees_in_slope(5, 1) * Get_trees_in_slope(7, 1) * Get_trees_in_slope(1, 2)}");

        public int Get_trees_in_slope(int vX, int vY) => field.Select((x, i) => x[(vX * i) / vY]).Where((x, i) => i % vY == 0).Count(x => x == '#');
    }
}