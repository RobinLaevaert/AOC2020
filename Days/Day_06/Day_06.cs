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

        protected override void Gather_input()
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

        protected override void Part1()
        {
            //Grouping all answer strings per group 
            var groupedAnswers = groups.Select(x => new string(x.Answers.SelectMany(y => y).ToArray()));
            //selecting distinct chars in each group-answer-string
            var distincAnswers = groupedAnswers.Select(x => x.Distinct().Count());
            //counting these all together
            var answer = distincAnswers.Sum();
            Console.WriteLine($"The sum of these counts is: {answer}");
        }

        protected override void Part2()
        {
            // Get list of strings which show the shared chars in each group
            var sharedAnswersPerGroup = groups.Select(x => x.Answers.Aggregate((y, z) => new string(y.Intersect(z).ToArray())));
            // Sum the length of each string == shared char count
            var answer = sharedAnswersPerGroup.Sum(x => x.Length);
            Console.WriteLine($"The sum of these counts is: {answer}");
        }
    }
}
