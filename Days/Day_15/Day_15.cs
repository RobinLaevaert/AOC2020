using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_15 : Day
    {
        private Dictionary<int, int> _dictionary;
        public Day_15()
        {
            Title = "Rambunctious Recitation";
            DayNumber = 15;
        }

        protected override void Gather_input()
        {
            var temp = Read_file().First().Split(',').Select(int.Parse).ToList();
            _dictionary = temp.ToDictionary(x => x, x => temp.IndexOf(x)+1);
        }

        protected override void Part1()
        {
            Console.WriteLine(Get_Xth_number_said(2020));
        }

        protected override void Part2()
        {
            Console.WriteLine(Get_Xth_number_said(30000000));
        }

        private int Get_Xth_number_said(int number_count)
        {
            var private_dictionary = _dictionary.ToDictionary(x => x.Key, x => x.Value);
            var last_number_said = private_dictionary.Last().Key;
            private_dictionary.Remove(last_number_said);
            var current_index = private_dictionary.Count+2;
            while (current_index <= number_count)
            {
                var new_number = Determine_number(last_number_said, current_index, private_dictionary);
                Store_number(last_number_said, current_index-1, private_dictionary);
                last_number_said = new_number;
                current_index++;
            }

            return last_number_said;
        }

        private static int Determine_number(int last_said_number, int current_round, Dictionary<int,int> dictionary)
        {
            if (dictionary.TryGetValue(last_said_number, out var value))
            {
                return current_round - 1 - value;
            }
            else
            {
                return 0;
            }
        }

        private static void Store_number(int number, int round_said, Dictionary<int,int> dictionary)
        {
            if (dictionary.ContainsKey(number))
                dictionary[number] = round_said;
            else dictionary.Add(number, round_said);
        }
    }
}
