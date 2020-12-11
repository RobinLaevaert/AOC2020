using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Days
{
    public class Day_11 : Day
    {
        Dictionary<Point, Seat_status> seats;
        List<List<Seat_status>> seats_test;
        public Day_11()
        {
            Title = "Seating System";
            DayNumber = 11;
        }
        public override void Gather_input()
        {
            seats = Read_file()
                .SelectMany((x, xi) => x.Select((y, yi) =>
                            new KeyValuePair<Point, Seat_status>(new Point(yi, xi), Get_seat_status(y))))
                .ToDictionary(x => x.Key, v => v.Value);
            seats_test = Read_file()
                .Select((x) => x.Select((y) => Get_seat_status(y)).ToList()).ToList();
        }

        public override void Part1()
        {
            var currentStatus = seats.ToDictionary(x => x.Key, x => x.Value);
            List<List<Seat_status>> currentStatus_test = seats_test.ToList();
            Console.WriteLine();
            while (true)
            {
                //var nextStatus = Get_next_iteration(currentStatus);
                var next_status_test = Get_next_iteration(currentStatus_test);
                if (next_status_test.All((x) => x.SequenceEqual(currentStatus_test[next_status_test.IndexOf(x)]))) break;
                //currentStatus = nextStatus;
                currentStatus_test = next_status_test;
                //currentStatus_test.ForEach(x => { Console.WriteLine(); x.ForEach(y => Console.Write(Get_seat_status_char(y))); });
            }
            Console.WriteLine(currentStatus_test.SelectMany(x => x.Select(y => y)).Count(x => x == Seat_status.Occupied));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
        public Dictionary<Point, Seat_status> Get_next_iteration(Dictionary<Point, Seat_status> status)
        {
            return status.ToDictionary(x => x.Key, x =>
            {
                if (x.Value == Seat_status.Floor) return Seat_status.Floor;
                var neighbours_occupied = status.Count(y => y.Value == Seat_status.Occupied && 
                                                            x.Key.X - 1 <= y.Key.X &&
                                                           y.Key.X <= x.Key.X + 1 &&
                                                           x.Key.Y - 1 <= y.Key.Y &&
                                                           y.Key.Y <= x.Key.Y + 1 &&
                                                           (x.Key.X != y.Key.X || x.Key.Y != y.Key.Y));
                if (x.Value == Seat_status.Empty && neighbours_occupied == 0) return Seat_status.Occupied;
                if (x.Value == Seat_status.Occupied && neighbours_occupied >= 4) return Seat_status.Empty;
                return x.Value;

            });
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
        public Seat_status Get_seat_status(char input)
        {
            switch (input)
            {
                case 'L': return Seat_status.Empty;
                case '#': return Seat_status.Occupied;
                default: return Seat_status.Floor;
            }
        }

        public char Get_seat_status_char(Seat_status input)
        {
            switch (input)
            {
                case Seat_status.Empty: return 'L';
                case Seat_status.Occupied: return '#';
                default: return '.';
            }
        }

        public void Print_dictionary(Dictionary<Point, Seat_status> dict)
        {
            var test = dict.ToList().OrderBy(x => x.Key.Y).ThenBy(x => x.Key.X).GroupBy(x => x.Key.Y).ToList();
            test.ForEach(x => { Console.WriteLine(); x.ToList().ForEach(y => Console.Write(Get_seat_status_char(y.Value))); });
        }
    }

    public enum Seat_status
    {
        Floor,
        Empty,
        Occupied
    }


}
