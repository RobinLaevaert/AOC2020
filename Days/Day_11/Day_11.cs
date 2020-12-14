using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public partial class Day_11 : Day
    {
        List<List<Seat_status>> seats_test;
        public Day_11()
        {
            Title = "Seating System";
            DayNumber = 11;
        }

        protected override void Gather_input()
        {
            seats_test = Read_file()
                .Select((x) => x.Select((y) => Get_seat_status(y)).ToList()).ToList();
        }

        protected override void Part1()
        {
            List<List<Seat_status>> current_status = seats_test.ToList();
            while (true)
            {
                var next_status = Get_next_iteration(current_status);
                if (next_status.All((x) => x.SequenceEqual(current_status[next_status.IndexOf(x)]))) break;
                current_status = next_status;
            }
            Console.WriteLine(current_status.SelectMany(x => x.Select(y => y)).Count(x => x == Seat_status.Occupied));
        }

        protected override void Part2()
        {
            List<List<Seat_status>> current_status = seats_test.ToList();
            while (true)
            {
                var next_status = Get_next_iteration_p2(current_status);
                if (next_status.All((x) => x.SequenceEqual(current_status[next_status.IndexOf(x)]))) break;
                current_status = next_status;
            }
            Console.WriteLine(current_status.SelectMany(x => x.Select(y => y)).Count(x => x == Seat_status.Occupied));
        }

        public List<List<Seat_status>> Get_next_iteration(List<List<Seat_status>> status)
        {
            return status.Select((x, xi) => x.Select((y, yi) => 
            {
                var lines = status.Where((z, zi) => xi - 1 <= zi && zi <= xi + 1);
                var test = lines.SelectMany(z => z.Where((a, ai) => yi - 1 <= ai && ai <= yi + 1)).ToList();
                var count = test.Count(x => x == Seat_status.Occupied);
                if (y == Seat_status.Occupied) count -= 1;
                if (y == Seat_status.Empty && count == 0) return Seat_status.Occupied;
                if (y == Seat_status.Occupied && count >= 4) return Seat_status.Empty;
                return y;
            }).ToList()).ToList();
        }

        public List<List<Seat_status>> Get_next_iteration_p2(List<List<Seat_status>> status)
        {
            return status.Select((x, xi) => x.Select((y, yi) =>
            {
                var first_seats = new List<Seat_status>();

                // Horizontal
                var horizontal_line = status[xi];
                var left = horizontal_line.Take(yi);
                var right = horizontal_line.Where((z, zi) => zi > yi);
                var first_left_seat = left.LastOrDefault(x => x != Seat_status.Floor);
                var first_right_seat = right.FirstOrDefault(x => x != Seat_status.Floor);
                first_seats.Add(first_right_seat);
                first_seats.Add(first_left_seat);

                // Vertical
                var vertical_line = status.Select(z => z[yi]).ToList();
                var up = vertical_line.Take(xi);
                var down = vertical_line.Where((z, zi) => zi > xi);
                var first_upper_seat = up.LastOrDefault(x => x != Seat_status.Floor);
                var first_down_seat = down.FirstOrDefault(x => x != Seat_status.Floor);
                first_seats.Add(first_upper_seat);
                first_seats.Add(first_down_seat);

                // Rising Diagonal
                var rising_diagonal = status.Select((z, zi) => z.Where((a, ai) => zi + ai == xi + yi).ToList()).ToList();
                var rising_diagonal_right = rising_diagonal.Take(xi).SelectMany(z => z.Select(a => a));
                var rising_diagonal_left = rising_diagonal.Where((z, zi) => zi > xi).SelectMany(z => z.Select(a => a)); 
                var first_seat_rising_diagonal_left = rising_diagonal_left.FirstOrDefault(x => x != Seat_status.Floor);
                var first_seat_rising_diagonal_right = rising_diagonal_right.LastOrDefault(x => x != Seat_status.Floor);
                first_seats.Add(first_seat_rising_diagonal_left);
                first_seats.Add(first_seat_rising_diagonal_right);

                // Falling Diagonal
                var falling_diagonal = status.Select((z, zi) => z.Where((a, ai) => zi - ai == xi - yi).ToList()).ToList();
                var falling_diagonal_left = falling_diagonal.Take(xi).SelectMany(z => z.Select(a => a));
                var falling_diagonal_right = falling_diagonal.Where((z, zi) => zi > xi).SelectMany(z => z.Select(a => a));
                var first_seat_falling_diagonal_left = falling_diagonal_left.LastOrDefault(x => x != Seat_status.Floor);
                var first_seat_falling_diagonal_right = falling_diagonal_right.FirstOrDefault(x => x != Seat_status.Floor);
                first_seats.Add(first_seat_falling_diagonal_right);
                first_seats.Add(first_seat_falling_diagonal_left);

                var count = first_seats.Count(x => x == Seat_status.Occupied);
                
                if (y == Seat_status.Empty && count == 0) return Seat_status.Occupied;
                if (y == Seat_status.Occupied && count >= 5) return Seat_status.Empty;
                return y;
            }).ToList()).ToList();
        }
        public Seat_status Get_seat_status(char input)
        {
            switch (input)
            {
                case 'L': return Seat_status.Empty;
                case '#': return Seat_status.Occupied;
                default: return Seat_status.Floor;
            }
        }
    }
}
