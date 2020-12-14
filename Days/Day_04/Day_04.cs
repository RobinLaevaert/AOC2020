using AOC2020.Shared;
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

        protected override void Gather_input()
        {
            StringBuilder sb = new();
            List<string> strings = new ();
            
            foreach (var line in Read_file())
            {
                if (line == string.Empty) 
                { 
                    strings.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append($" {line}");
                }
            }
            strings.Add(sb.ToString());

            dictionary = strings.Select(x => x.Split(" ")
                                              .Where(y => y != string.Empty)
                                              .Select(y => y.Split(':'))
                                .ToDictionary(y => y[0], y => y[1])).ToList();
        }

        protected override void Part1()
        {
            var validPasswords = dictionary.Where(x => x.Count >= 7)
                .Count(x => x.ContainsKey("byr") &&
                            x.ContainsKey("iyr") &&
                            x.ContainsKey("eyr") &&
                            x.ContainsKey("hgt") &&
                            x.ContainsKey("hcl") &&
                            x.ContainsKey("ecl") &&
                            x.ContainsKey("pid"));
            Console.WriteLine($"There are {validPasswords} validapasswords");
        }

        protected override void Part2()
        {
            var regex = new Regex(@"(#{1})([0-9a-f]{6}|[0-9a-f]{6})$");
            var validPasswords = dictionary.Where(x => x.Count >= 7)
                .Count(x => x.ContainsKey("byr") && (int.Parse(x["byr"]) is >= 1920 and <= 2002) &&
                            x.ContainsKey("iyr") && (int.Parse(x["iyr"]) is >= 2010 and <= 2020) &&
                            x.ContainsKey("eyr") && (int.Parse(x["eyr"]) is >= 2020 and <= 2030) &&
                            x.ContainsKey("hgt") && is_hgt_valid(x["hgt"]) &&
                            x.ContainsKey("hcl") && regex.IsMatch(x["hcl"]) &&
                            x.ContainsKey("ecl") && possibleEyeColors.Contains(x["ecl"]) &&
                            x.ContainsKey("pid") && x["pid"].Length == 9);

            Console.WriteLine($"There are {validPasswords} validapasswords");
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
