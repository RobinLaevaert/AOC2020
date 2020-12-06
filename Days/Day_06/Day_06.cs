using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Days
{
    public class Day_06 : Day
    {
        readonly List<Group> groups = new();
        public Day_06()
        {
            DayNumber = 6;
            Title = "Custom Customs";
        }
        public override void Gather_input()
        {
            Group group = new();
            foreach (var line in Read_file())
            {
                if (line == string.Empty)
                {
                    groups.Add(group);
                    group = new();
                }
                else
                {
                    group.Answers.Add(line);
                }
            }
            groups.Add(group);
        }

        public override void Part1()
        {
            var groupedAnswers = groups.Select(x => new string(x.Answers.SelectMany(y => y).ToArray()));
            var distincAnswers = groupedAnswers.Select(x => x.Distinct().Count());
            var answer = distincAnswers.Aggregate((x, y) => x + y);
            Console.WriteLine($"The sum of these counts is: {answer}");
        }

        public override void Part2()
        {
            var sharedAnswersPerGroup = groups.Select(x => x.Answers.Aggregate((y, z) => new string(y.Intersect(z).ToArray())));
            var answer = sharedAnswersPerGroup.Sum(x => x.Length);
            Console.WriteLine($"The sum of these counts is: {answer}");
        }
    }
}
