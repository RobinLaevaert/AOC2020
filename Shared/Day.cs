using System;
using System.IO;
using System.Reflection;


namespace AOC2020.Shared
{
    public abstract class Day
    {
        public string Title;
        public int DayNumber;


        public Day()
        {
        }
        public void PrintInfo()
        {
            Console.WriteLine($"{DayNumber}. {Title}");
        }

        public void HandleSelect()
        {
            Console.WriteLine("Do you want to solve Part 1 or 2?");

            switch (Convert.ToDouble(Console.ReadLine()))
            {
                case 1:
                    Part1();
                    break;
                case 2:
                    Part2();
                    break;
                default:
                    Console.WriteLine($"Not implemented");
                    HandleSelect();
                    break;
            }
        }

        public void Deselect()
        {
            Console.WriteLine("Press Key to go back to main menu");
            Console.ReadKey();
            Console.Clear();
        }

        public string GetFilePath()
        {
            var path =  Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"input{DayNumber}.txt");
            return path;
        }
        public abstract void ReadFile();

        public abstract void Part1();

        public abstract void Part2();
    }
}
