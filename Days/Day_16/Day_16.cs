using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_16 : Day
    {
        private List<Category> categories;
        private List<List<int>> nearby_tickets;
        private List<int> your_ticket;
        public Day_16()
        {
            Title = "Ticket Translation";
            DayNumber = 16;
        }

        protected override void Gather_input()
        {
            categories = new();
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
                        var ticket_type = new Category();
                        ticket_type.Name = line.Split(':')[0];
                        var line_split = line.Split(':')[1].Trim();
                        var ranges_split = line_split.Split(" or ");
                        var tuples = ranges_split.Select(x => x.Split('-').Select(int.Parse))
                            .Select(x => new Tuple<int, int>(x.First(), x.Last())).ToList();
                        ticket_type.Ranges = tuples;
                        categories.Add(ticket_type);
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
                .Where(x => !categories.SelectMany(category => category.Ranges).Any(range => range.Item1 <= x && x <= range.Item2)).ToList();
            Console.WriteLine(invalid_tickets.Sum());
        }

        protected override void Part2()
        {
            // Get all wrong tickets and remove them (Pt1)
            var all_ticket_numbers = nearby_tickets.SelectMany(number => number);
            var invalid_tickets_numbers = all_ticket_numbers
                .Where(x => !categories.SelectMany(category => category.Ranges).Any(range => range.Item1 <= x && x <= range.Item2)).ToList();
            nearby_tickets.RemoveAll(ticket => invalid_tickets_numbers.Any(ticket.Contains));
            
            // Add your own ticket since this can be used to determine correct types
            nearby_tickets.Add(your_ticket);
            
            // Select all the possible types for each index for each ticket
            var possible_categories = nearby_tickets.Select(ticket => ticket.Select(number =>
                categories.Where(category => category.Ranges.Any(z => z.Item1 <= number && number <= z.Item2)).Select(category => category.Name)
                    .ToList()).ToArray());
            // Aggregate them by getting the intersection if each line of each type
            var current_possible_categories = possible_categories.Aggregate(
                (possible_categories, next_possible_categories) => possible_categories
                    .Select((category_names, index) => next_possible_categories[index].Intersect(category_names).ToList()).ToArray());
            // All the types which are certain => only 1 possible
            var taken_types = current_possible_categories.Select(possible_category_names =>
                possible_category_names.Count == 1 ? possible_category_names.Single() : "").ToList();
            
            while (taken_types.Any(x => x == ""))
            {
                // Remove all possible types from the list which are certainly used for another index
                current_possible_categories = current_possible_categories.Select(possible_category_names =>
                        possible_category_names.Where(category_name => !taken_types.Contains(category_name)).ToList())
                    .ToArray();
                // All the types which are certain at this moment => only 1 possible
                var current_certain_types = current_possible_categories.Select(possible_category_names =>
                    possible_category_names.Count == 1 ? possible_category_names.Single() : "").ToList();
                // Aggregate them with the already known types
                taken_types = new List<List<string>>() {current_certain_types, taken_types}.Aggregate((x, y) =>
                    x.Select((z, i) => z == "" ? y[i] : z).ToList());
            }
            //Select all values from your ticket with a type that starts with 'departure'
            var departure_types = taken_types.Select((category_name, i) => new {Name = category_name, index = i})
                .Where(Category => Category.Name.StartsWith("departure")).Select(Category => Category.index)
                .Select(index => your_ticket[index]);
            Console.WriteLine(departure_types.Select(Convert.ToInt64).Aggregate((x, y) => x * y));
        }
    }
}
