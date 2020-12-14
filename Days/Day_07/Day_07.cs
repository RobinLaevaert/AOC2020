using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_07 : Day
    {
        public List<Bag_1> Bags;
        public List<Description> Descriptions;
        public Day_07()
        {
            DayNumber = 7;
            Title = "Handy Haversacks";
        }

        protected override void Gather_input()
        {
            var temp = Read_file();
            Descriptions = temp.Select(Description.Create).ToList();
            var test = temp.Select(x => x.Split("contain"));
            var tempDict = test.ToDictionary(x => x[0], v => v[1].Split(',').AsEnumerable());
            Bags = tempDict.Select(Bag_1.Create_From_Key_value_pair).ToList();
        }

        protected override void Part1()
        {
            var count = Bags.Count(x => x.Child_elements.Any(y => y.Color == "shiny gold"));
            var loop = true;
            while (loop)
            {
                var colorsWithShinyIn = Bags.Where(x => x.Child_elements.Any(y => y.Color == "shiny gold")).Select(x => x.Color);
                var bags_that_contain_bags_with_colorsWithShinyIn = Bags.Where(x => x.Child_elements.Any(y => colorsWithShinyIn.Contains(y.Color)));
                if (bags_that_contain_bags_with_colorsWithShinyIn.Any()){
                    foreach (var item in bags_that_contain_bags_with_colorsWithShinyIn)
                    {
                        Bags.Find(x => x.Color == item.Color).Child_elements.Remove(item.Child_elements.First(x => colorsWithShinyIn.Contains(x.Color)));
                        Bags.Find(x => x.Color == item.Color).Child_elements.Add(new Bag_Item() {Color = "shiny gold" });
                    }
                    colorsWithShinyIn = Bags.Where(x => x.Child_elements.Any(y => y.Color == "shiny gold")).Select(x => x.Color);
                    bags_that_contain_bags_with_colorsWithShinyIn = Bags.Where(x => x.Child_elements.Any(y => colorsWithShinyIn.Contains(y.Color)));
                }
                else
                {
                    loop = false;
                }
            }
            var shiny_bags_count = Bags.Count(x => x.Child_elements.Any(y => y.Color == "shiny gold"));
            Console.WriteLine(shiny_bags_count);
        }

        protected override void Part2()
        {
            var gold_bag = Descriptions.Single(x => x.Parent_bag.color == "shiny gold");
            Console.WriteLine(CalculateAllChildBags(gold_bag));
        }

        public int CalculateAllChildBags(Description description)
        {
            var child_Bags = new List<Bag>();
            var parent_bag_count = 0;
            description.Child_bags.ForEach(x =>
            {
                if (child_Bags.FirstOrDefault(y => y.color == x.color) == null) child_Bags.Add(x);
                else
                {
                    child_Bags.First(y => y.color == x.color).Amount += x.Amount;
                }
            });

            while (child_Bags.Select(x => Descriptions.Single(y => y.Parent_bag.color == x.color)).Any(x => x.Child_bags.Any()))
            {
                var child_bag_description = Descriptions.Single(x => x.Parent_bag.color == child_Bags[0].color);
                
                if (child_bag_description.Child_bags.Any())
                {
                    child_Bags.AddRange(child_bag_description.Child_bags.Select(x =>
                    {
                        var cloned = x.Clone();
                        cloned.Amount *= child_Bags[0].Amount;
                        return cloned;
                    }));
                    parent_bag_count += child_Bags[0].Amount;
                    child_Bags.Remove(child_Bags[0]);
                    continue;
                }
                child_Bags = child_Bags.OrderByDescending(x => Descriptions.Single(y => y.Parent_bag.color == x.color).Child_bags.Count).ToList();
            }

            return child_Bags.Sum(x => x.Amount) + parent_bag_count;
        }
    }

    public class Bag_1
    {
        public Bag_1()
        {

        }

        public static Bag_1 Create_From_Key_value_pair(KeyValuePair<string, IEnumerable<string>> input)
        {
            var bag_color = $"{input.Key.Split(" ")[0]} {input.Key.Split(" ")[1]}";
            var child_items = input.Value.First() == " no other bags." ? new List<Bag_Item>() : input.Value.Select(x =>
            {
                var splitted_string = x.Trim().Split(' ');
                var qty = int.Parse(splitted_string[0]);
                var color = $"{splitted_string[1]} {splitted_string[2]}";

                return new Bag_Item(qty, color);
            }).ToList();

            return new Bag_1()
            {
                Color = bag_color,
                Child_elements = child_items
            };
        }
        public string Color { get; set; }
        public List<Bag_Item> Child_elements { get; set; }
    }
    public class Bag_Item
    {
        public Bag_Item()
        {

        }
        public Bag_Item(int qty, string color)
        {
            Quantity = qty;
            Color = color;
        }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public Bag_1 Bag { get; set; }
    }

    
}
