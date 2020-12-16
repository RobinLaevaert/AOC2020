using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_16 : Day
    {
        private List<Ticket_type> ranges;
        private List<List<int>> nearby_tickets;
        private List<int> your_ticket;
        public Day_16()
        {
            Title = "Ticket Translation";
            DayNumber = 16;
        }

        protected override void Gather_input()
        {
            ranges = new();
            nearby_tickets = new();
            your_ticket = new();
            var temp = Read_file().ToList();
            var mode = "ranges";
            foreach (var line in temp)
            {
                switch (line)
                {
                    case "":
                        continue;
                    case "your ticket:":
                        mode = "your_ticket";
                        continue;
                    case "nearby tickets:":
                        mode = "nearby_tickets";
                        continue;
                }

                switch (mode)
                {
                    case "ranges":
                        var ticket_type = new Ticket_type();
                        ticket_type.ticket_type = line.Split(':')[0];
                        var line_split = line.Split(':')[1].Trim();
                        var ranges_split = line_split.Split(" or ");
                        var tuples = ranges_split.Select(x => x.Split('-').Select(int.Parse))
                            .Select(x => new Tuple<int, int>(x.First(), x.Last())).ToList();
                        ticket_type.Ranges = tuples;
                        ranges.Add(ticket_type);
                        break;
                        
                    case "your_ticket":
                        your_ticket.AddRange(line.Split(',').Select(int.Parse));
                        break;
                    case "nearby_tickets":
                        nearby_tickets.Add(line.Split(',').Select(int.Parse).ToList());
                        break;
                        
                }
            }
        }

        protected override void Part1()
        {
            var all_ticket_numbers = nearby_tickets.SelectMany(x => x);
            var invalid_tickets = all_ticket_numbers
                .Where(x => !ranges.SelectMany(x => x.Ranges).Any(y => y.Item1 <= x && x <= y.Item2)).ToList();
            Console.WriteLine(invalid_tickets.Sum());
        }

        protected override void Part2()
        {
            // Get all wrong tickets and remove them (Pt1)
            var all_ticket_numbers = nearby_tickets.SelectMany(x => x);
            var invalid_tickets_numbers = all_ticket_numbers
                .Where(x => !ranges.SelectMany(x => x.Ranges).Any(y => y.Item1 <= x && x <= y.Item2)).ToList();
            nearby_tickets.RemoveAll(x => invalid_tickets_numbers.Any(x.Contains));
            
            // Add your own ticket since this can be used to determine correct types
            nearby_tickets.Add(your_ticket);
            
            // Select all the possible types for each index for each ticket
            var possible_types = nearby_tickets.Select(q => q.Select(x =>
                ranges.Where(y => y.Ranges.Any(z => z.Item1 <= x && x <= z.Item2)).Select(y => y.ticket_type)
                    .ToList()).ToArray());
            // Aggregate them by getting the intersection if each line of each type
            var current_possible_types = possible_types.Aggregate((x, y) => x.Select((z, i) => y[i].Intersect(z).ToList()).ToArray());
            // All the types which are certain => only 1 possible
            var taken_types = current_possible_types.Select(x => x.Count == 1 ? x.Single() : "").ToList();
            
            while (taken_types.Any(x => x == ""))
            {
                // Remove all possible types from the list which are certainly used for another index
                current_possible_types = current_possible_types.Select(x => x.Where(z => !taken_types.Contains(z)).ToList()).ToArray();
                // All the types which are certain at this moment => only 1 possible
                var current_certain_types = current_possible_types.Select(x => x.Count == 1 ? x.Single() : "").ToList();
                // Aggregate them with the already known types
                taken_types = new List<List<string>>() {current_certain_types, taken_types}.Aggregate((x, y) =>
                    x.Select((z, i) => z == "" ? y[i] : z).ToList());
            }
            //Select all values from your ticket with a type that starts with 'departure'
            var departure_types = taken_types.Select((x, i) => new {type = x, index = i})
                .Where(x => x.type.StartsWith("departure")).Select(x => x.index).Select(x => your_ticket[x]);
            Console.WriteLine(departure_types.Select(Convert.ToInt64).Aggregate((x, y) => x * y));
        }
    }
}
