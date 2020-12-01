using AOC2020.Days;
using AOC2020.Shared;
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
                new Day_2(),
                new Day_3(),
                new Day_4(),
                new Day_5(),
                new Day_6(),
                new Day_7(),
                new Day_8(),
                new Day_9(),
                new Day_10(),
                new Day_11(),
                new Day_12(),
                new Day_13(),
                new Day_14(),
                new Day_15(),
                new Day_16(),
                new Day_17(),
                new Day_18(),
                new Day_19(),
                new Day_20(),
                new Day_21(),
                new Day_22(),
                new Day_23(),
                new Day_24(),
                new Day_25(),
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
