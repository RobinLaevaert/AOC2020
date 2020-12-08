using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_08 : Day
    {
        List<Tuple<Task, int>> task_list;
        public Day_08()
        {
            DayNumber = 8;
            Title = "Handheld Halting";
        }
        public override void Gather_input()
        {
            task_list = Read_file().Select(x =>
            {
                var splitted = x.Split(' ');
                _ = Enum.TryParse(splitted[0], out Task task);
                return new Tuple<Task, int>(task, int.Parse(splitted[1]));
            }).ToList();
        }

        public override void Part1()
        {
            var result = Computer.Go_brr(task_list, out _);
            Console.WriteLine(result);
        }

        public override void Part2()
        {
            var possible_changes = task_list.Select((x, i) => new Tuple<Task, int, int>(x.Item1, x.Item2, i)).Where(x => x.Item1 is Task.nop or Task.jmp).Select(x => x.Item3);
            var succesful_run = false;
            foreach(var change in possible_changes)
            {
                if (succesful_run) break;
                var new_task_dictionary = task_list.ToList();
                var item_to_replace = new_task_dictionary[change];
                new_task_dictionary[change] = new Tuple<Task, int>(item_to_replace.Item1 == Task.nop ? Task.jmp : Task.nop, item_to_replace.Item2);

                var result = Computer.Go_brr(new_task_dictionary, out succesful_run);
                
                if(succesful_run)
                    Console.WriteLine(result);
            }
        }
    }
}
