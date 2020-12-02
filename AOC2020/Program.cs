﻿using AOC2020.Shared;
using Days;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    class Program
    {
        public static List<Day> days = new()
        {
            new Days.Day_01(),
            new Days.Day_02()
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Which Day do you want ?");
                days.Where(x => x.Title != null).ToList().ForEach(x => x.PrintInfo());
                var chosenDay = Convert.ToInt32(Console.ReadLine());
                var day = days.FirstOrDefault(x => x.DayNumber == chosenDay);
                if(day != null)
                {
                    day.HandleSelect();
                    day.Deselect();
                }
                else
                {
                    Console.WriteLine("Day not found, Press Key to go back to main menu");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
