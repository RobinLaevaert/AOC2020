using System;
using System.Collections.Generic;

namespace Days
{
    public static class Computer
    {
        public static int Go_brr(List<Tuple<Task, int>> task_list, out bool succesful_run)
        {
            List<int> passedTasks = new();
            var acc_value = 0;
            var index = 0;
            succesful_run = false;
            while (true)
            {
                if (passedTasks.Contains(index))
                    break;

                passedTasks.Add(index);
                if (index == task_list.Count)
                {
                    succesful_run = true;
                    break;
                }
                var todo = task_list[index];

                switch (todo.Item1)
                {
                    case Task.acc:
                        acc_value += todo.Item2;
                        index++;
                        break;
                    case Task.jmp:
                        index += todo.Item2;
                        break;
                    case Task.nop:
                        index++;
                        break;
                }
            }
            return acc_value;
        }
    }
}
