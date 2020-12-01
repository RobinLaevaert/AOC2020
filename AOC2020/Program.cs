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
                new Day_1(),
            };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Which Day do you want ?");
                days.Where(x => x.Title != null).ToList().ForEach(x => x.PrintInfo());
                var chosenDay = Convert.ToInt32(Console.ReadLine());
                days.First(x => x.DayNumber == chosenDay).HandleSelect();
                days.First(x => x.DayNumber == chosenDay).Deselect();
            }
        }
    }
}
