﻿using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Days
{
    public class Day_04 : Day
    {
        List<Dictionary<string, string>> dictionary = new();
        List<string> possibleEyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public Day_04()
        {
            DayNumber = 4;
            Title = "Passport Processing";
        }
        public override void Gather_input()
        {
            var currentString = "";
            List<string> strings = new ();
            
            foreach (var line in Read_file())
            {
                if (line == string.Empty) 
                { 
                    strings.Add(currentString);
                    currentString = "";
                }
                else
                {
                    currentString += $" {line}";
                }
            }
            strings.Add(currentString);

            dictionary = strings.Select(x => x.Split(" ").Where(y => y != string.Empty).Select(y => y.Split(':')).ToDictionary(y => y[0], y => y[1])).ToList();
        }

        public override void Part1()
        {
            var validPasswords = dictionary.Where(x => x.Count() >= 7)
                .Count(x => x.ContainsKey("byr") &&
                            x.ContainsKey("iyr") &&
                            x.ContainsKey("eyr") &&
                            x.ContainsKey("hgt") &&
                            x.ContainsKey("hcl") &&
                            x.ContainsKey("ecl") &&
                            x.ContainsKey("pid"));
            Console.WriteLine($"There are {validPasswords} validapasswords");
        }

        public override void Part2()
        {
            var regex = new Regex(@"(#{1})([0-9a-f]{6}|[0-9a-f]{6})$");
            var validPasswords = dictionary.Where(x => x.Count() >= 6)
                .Where(x => x.ContainsKey("byr") && (1920 <= int.Parse(x.GetValueOrDefault("byr")) && int.Parse(x.GetValueOrDefault("byr")) <= 2002) &&
                            x.ContainsKey("iyr") && (2010 <= int.Parse(x.GetValueOrDefault("iyr")) && int.Parse(x.GetValueOrDefault("iyr")) <= 2020) &&
                            x.ContainsKey("eyr") && (2020 <= int.Parse(x.GetValueOrDefault("eyr")) && int.Parse(x.GetValueOrDefault("eyr")) <= 2030) &&
                            x.ContainsKey("hgt") && is_hgt_valid(x.GetValueOrDefault("hgt")) &&
                            x.ContainsKey("hcl") && regex.IsMatch(x.GetValueOrDefault("hcl")) &&
                            x.ContainsKey("ecl") && possibleEyeColors.Contains(x.GetValueOrDefault("ecl")) &&
                            x.ContainsKey("pid") && x.GetValueOrDefault("pid").Length == 9);

            Console.WriteLine($"There are {validPasswords.Count()} validapasswords");
        }

        bool is_hgt_valid(string input)
        {
            if (input.Length < 4) return false;
            var unit = input.Substring(input.Length - 2);
            var value = int.Parse(input.Substring(0, input.Length - 2));
            if (unit == "cm" && 150 <= value && value <= 193) return true;
            if (unit == "in" && 59 <= value && value <= 76) return true;
            return false;
        }
    }

}
