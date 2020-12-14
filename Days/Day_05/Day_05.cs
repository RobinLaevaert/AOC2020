using System;
using System.Collections.Generic;
using System.Linq;
using AOC2020.Shared;

namespace Days
{
    public class Day_05 : Day
    {
        private List<Boarding_pass> boarding_passes = new ();
        public Day_05()
        {
            Title = "Binary Boarding";
            DayNumber = 5;
        }

        protected override void Gather_input()
        {
            boarding_passes = Read_file().Select(x => new Boarding_pass(new string(x
                                            .Substring(0, 7)
                                            .Select(y => y == 'F' ? 'L' : 'U').ToArray()),
                                            new string(x.Substring(x.Length - 3, 3)
                                            .Select(y => y == 'L' ? 'L' : 'U').ToArray())))
                                         .ToList();

        }

        protected override void Part1()
        {
            var number_of_rows = 127;
            var number_of_columns = 7;

            Calculate_seats(number_of_rows, number_of_columns);

            var max_seat_id = boarding_passes.Max(x => x.Seat_id);
            Console.WriteLine($"Max Seat Id: {max_seat_id}");
        }

        protected override void Part2()
        {
            var number_of_rows = 127;
            var number_of_columns = 7;

            Calculate_seats(number_of_rows, number_of_columns);

            var min = boarding_passes.Min(x => x.Seat_id);
            var max = boarding_passes.Max(x => x.Seat_id);
            var range = Enumerable.Range(min, max - min); // max-min => this value is count not max value
            var missing_seat_id = range.Except(boarding_passes.Select(x => x.Seat_id)).Single();
            Console.WriteLine($"Missing Seat Id: {missing_seat_id}");
        }

        private void Calculate_seats(int number_of_rows, int number_of_columns)
        {
            boarding_passes = boarding_passes.Select(x =>
            {
                x.Row = get_seat(x.Row_instruction, 0, number_of_rows);
                x.Column = get_seat(x.Column_instruction, 0, number_of_columns);
                return x;
            }).ToList();
        }

        public int get_seat(string instructions, int lower_limit, int upper_limit)
        {
            if (instructions == string.Empty) return lower_limit;
            var middle_point = (int)(instructions.First() == 'U' ? 
                Math.Ceiling((double)(lower_limit + upper_limit) / 2) : 
                Math.Floor((double)(lower_limit + upper_limit) / 2));
            var new_lower_limit = instructions.First() == 'U' ? middle_point : lower_limit;
            var new_upper_limit = instructions.First() == 'U' ? upper_limit : middle_point;
            var new_instructions = instructions[1..];

            return get_seat(new_instructions, new_lower_limit, new_upper_limit);
        }

    }
}
