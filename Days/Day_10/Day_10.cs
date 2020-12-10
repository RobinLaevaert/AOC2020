using AOC2020.Shared;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_10 : Day
    {
        List<int> adapters;
        List<Tuple<int, long>> calculatedSteps;
        public Day_10()
        {
            Title = "Adapter Array";
            DayNumber = 10;
        }
        public override void Gather_input()
        {
            adapters = Read_file().Select(int.Parse).ToList();
            adapters.Add(0); // wall Adapter
            adapters.Add(adapters.Max() + 3); // final step
        }

        public override void Part1()
        {
            var sortedInput = adapters.OrderBy(x => x);
            var differences = sortedInput.SelectWithPrevious((prev, curr) => curr - prev);
            Console.WriteLine(differences.Count(x => x == 3) * differences.Count(x => x == 1));
        }

        public override void Part2()
        {
            calculatedSteps = new();
            var sortedInput = adapters.OrderBy(x => x);
            var routeSplits = sortedInput.Select(x => new RouteSplit { Number = x, Routes =adapters.Where(y => x + 1 <= y && y <= x + 3) });
            Console.WriteLine(GetPossibleRoutes(sortedInput, routeSplits, sortedInput.First()));
        }
        public long GetPossibleRoutes(IEnumerable<int> input, IEnumerable<RouteSplit> splits, int beginValue)
        {

            if (beginValue == input.Last())
                return 1;
            else
            {
                var split = splits.Single(x => x.Number == beginValue);
                var alreadyCalculated = calculatedSteps.FirstOrDefault(x => x.Item1 == beginValue);
                if (alreadyCalculated != null) return alreadyCalculated.Item2;
                else
                {
                    var calculation = split.Routes.Sum(x => GetPossibleRoutes(input, splits, x));
                    calculatedSteps.Add(new Tuple<int, long>(beginValue, calculation));
                    return calculation;
                }
               
            }
        }
    }
}

