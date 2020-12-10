using AOC2020.Shared;
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
            new Days.Day_02(),
            new Days.Day_03(),
            new Days.Day_04(),
            new Days.Day_05(),
            new Days.Day_06(),
            new Days.Day_07(),
            new Days.Day_08(),
            new Days.Day_09(),
            new Days.Day_10()
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Which Day do you want ?");
                days.Where(x => x.Title != null).ToList().ForEach(x => x.PrintInfo());
                var input = Console.ReadLine();
                if(int.TryParse(input, out var chosenDay) && days.SingleOrDefault(x => x.DayNumber == chosenDay) != null)
                {
                    var day = days.Single(x => x.DayNumber == chosenDay);
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
