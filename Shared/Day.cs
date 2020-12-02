﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            switch (Console.ReadLine())
            {
                case "1":
                    ReadFile();
                    Part1();
                    break;
                case "2":
                    ReadFile();
                    Part2() ;
                    break;
                case "1p":
                    Performance_logging(ReadFile, Part1);
                    break;
                case "2p":
                    Performance_logging(ReadFile, Part2);
                    break;
                case "3p":
                    Performance_logging(ReadFile, Part1, Part2);
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

        public void Performance_logging(params Action[] actions) 
        {
            Stopwatch stopwatch = new();
            Dictionary<string, double> timings_per_action = new();
            Console.WriteLine("Results:");
            foreach (var action in actions)
            {
                var action_name = action.GetMethodInfo().Name;
                if(action_name != "ReadFile")
                    Console.WriteLine(action_name);
                stopwatch.Reset();
                stopwatch.Start();
                action();
                stopwatch.Stop();
                timings_per_action.Add(action.GetMethodInfo().Name, (double)stopwatch.ElapsedTicks / TimeSpan.TicksPerMillisecond);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Performance metrics");
            foreach (var timing in timings_per_action)
            {
                Console.WriteLine($"{timing.Key}: {timing.Value} ms");
            }
            Console.WriteLine();
            stopwatch.Reset();
        }
    }
}
