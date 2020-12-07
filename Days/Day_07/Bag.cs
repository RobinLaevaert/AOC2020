using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_07
{
    public class Description
    {
        public List<Bag> Child_bags { get; set; }
        public Bag Parent_bag { get; set; }

        public static Description Create(string inputRecipe)
        {
            var temp = inputRecipe.Split("contain");
            var leftSide = temp[0];
            var rightSide = temp[1];

            var parentBag = Bag.Create($"1 {leftSide}");
            var child_bags = new List<Bag>();
            if(rightSide != " no other bags.")
                child_bags = rightSide.Split(',').Select(x => Bag.Create(x.Trim(new[] { ',', ' ' }))).ToList();

            return new Description
            {
                Child_bags = child_bags,
                Parent_bag = parentBag
            };
        }

        public Description Clone()
        {
            return new Description
            {
                Child_bags = this.Child_bags.Select(x => x.Clone()).ToList(),
                Parent_bag = this.Parent_bag.Clone()
            };
        }

    }

    public class Bag
    {
        public int Amount { get; set; }
        public string color { get; set; }

        public static Bag Create(string input)
        {
            var bag = new Bag();
            var SplittedInput = input.Trim().Split(" ");
            bag.Amount = int.Parse(SplittedInput[0]);
            bag.color = $"{SplittedInput[1]} {SplittedInput[2]}";
            return bag;
        }

        public Bag Clone()
        {
            return new Bag
            {
                Amount = this.Amount,
                color = this.color
            };
        }


    }
}
