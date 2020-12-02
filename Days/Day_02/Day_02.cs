using AOC2020.Shared;
using Day_02;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Days
{
    public class Day_02 : Day
    {
        List<Password_validation> input;
        public Day_02()
        {
            DayNumber = 2;
            Title = "Password Philosophy";
        }
        public override void Part1()
        {
            var results = input.Select(x => x.Is_valid_part1());
            var count_valid_passwords = results.Count(x => x == true);
            Console.WriteLine($"Amount of correct passwords: {count_valid_passwords}");
        }

        public override void Part2()
        {
            var results = input.Select(x => x.Is_valid_part2());
            var count_valid_passwords = results.Count(x => x == true);
            Console.WriteLine($"Amount of correct passwords: {count_valid_passwords}");
        }


        public override void Gather_input()
        {
            var string_input = Read_file().ToList();
            input = string_input.Select(x =>
            {
                var string_input = x.Split(" ");
                var lower_index = int.Parse(string_input[0].Split("-")[0]);
                var upper_index = int.Parse(string_input[0].Split("-")[1]);
                var character = Char.Parse(string_input[1].Remove(1));
                var attempted_password = string_input[2];
                return new Password_validation()
                {
                    Character = character,
                    Lower_index = lower_index,
                    Upper_index= upper_index,
                    Password = attempted_password
                };
            }).ToList();
        }
    }
}
