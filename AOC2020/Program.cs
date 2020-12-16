using AOC2020.Shared;
using Days;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace AOC2020
{
    internal static class Program
    {
        private static readonly List<Day> Days = new()
        {
            new Day_01(),
            new Day_02(),
            new Day_03(),
            new Day_04(),
            new Day_05(),
            new Day_06(),
            new Day_07(),
            new Day_08(),
            new Day_09(),
            new Day_10(),
            new Day_11(),
            new Day_12(),
            new Day_13(),
            new Day_14(),
            new Day_15(),
            new Day_16()
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Which Day do you want ?");
                var infos = Days.Where(x => x.Title != null).ToList().Select(x => x.Info).ToList();
                WriteHelper.PrintInfos(infos);
                var input = Console.ReadLine();
                if(int.TryParse(input, out var chosenDay) && Days.SingleOrDefault(x => x.DayNumber == chosenDay) != null)
                {
                    var day = Days.Single(x => x.DayNumber == chosenDay);
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
