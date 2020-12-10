using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_09 : Day
    {
        List<long> input_stream;
        public Day_09()
        {
            Title = "Encoding Error";
            DayNumber = 9;
        }
        public override void Gather_input()
        {
            input_stream = Read_file().Select(long.Parse).ToList();
        }

        public override void Part1()
        {
            var faulty_value = Get_faulty_value(input_stream, 5);
            Console.WriteLine(faulty_value);
        }

        public override void Part2()
        {
            var faulty_value = Get_faulty_value(input_stream, 25);
            foreach (var item in input_stream.Select((value, i) => new { i, value }))
            {
                long value = 0;
                List<long> used_values = new();
                var index = item.i;
                while (value < faulty_value)
                {
                    if (index == -1 || value == faulty_value) break;
                    used_values.Add(input_stream[index]);
                    value += input_stream[index];
                    index--;
                }
                if (value == faulty_value && used_values.Count >= 2)
                    Console.WriteLine(used_values.OrderBy(x => x).First() + used_values.OrderBy(x => x).Last());
            }
        }

        public static long Get_faulty_value(List<long> input, int preamble_length)
        {
            foreach (var item in input.Select((value, i) => new { i, value }).Where((x, i) => i >= preamble_length))
            {
                var possible_range = input.Where((x, i) => item.i - preamble_length <= i && i < item.i);
                var matchFound = possible_range.Any(x => possible_range.Any(y => x + y == item.value));
                if (!matchFound)
                {
                    return item.value;
                }
            }
            throw new Exception("Should not happen");
        }
    }
}
