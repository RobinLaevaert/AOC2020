using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Days
{
    public class Day_14 : Day
    {
        List<Instruction> Instructions;
        public Day_14()
        {
            Title = "Docking Data";
            DayNumber = 14;
        }
        public override void Gather_input()
        {
            Instructions = new List<Instruction>();
            Instruction current_instruction = null;
            foreach (var line in Read_file())
            {
                var splitted_string = line.Split(" = ");
                if(splitted_string.First() == "mask")
                {
                    if (current_instruction != null) Instructions.Add(current_instruction);
                    current_instruction = new Instruction() { Bit_mask = splitted_string.Last() };
                }
                else
                {
                    var address = splitted_string.First()[4..^1];
                    current_instruction.Memory_instructions.Add(new Memory_instruction()
                    {
                        Memory_address = int.Parse(splitted_string.First()[4..^1]),
                        Value = long.Parse(splitted_string.Last())
                    }); ;
                }
            }
            Instructions.Add(current_instruction);
        }

        public override void Part1()
        {
            var memory = new List<Memory>();
            foreach (var instruction in Instructions)
            {
                foreach (var memory_instruction in instruction.Memory_instructions)
                {
                    var new_mask = instruction.Bit_mask.Select(x => x == 'X' ? '1' : '0').ToArray();
                    var value_with_0_bits_where_mask_will_change = memory_instruction.Value & Convert.ToInt64(new string(new_mask), 2);
                    var mask_filtered = instruction.Bit_mask.Select(x => x == 'X' ? '0' : x).ToArray();
                    var new_long_value = value_with_0_bits_where_mask_will_change | Convert.ToInt64(new string(mask_filtered), 2);

                    if (!memory.Any(x => x.Address == memory_instruction.Memory_address)) memory.Add(new Memory() { Address = memory_instruction.Memory_address, Value = new_long_value });
                    else
                    {
                        memory.Single(x => x.Address == memory_instruction.Memory_address).Value = new_long_value;
                    }
                }
            }
            Console.WriteLine(memory.Sum(x => x.Value));
        }

        public override void Part2()
        {
            var memory = new List<Memory>();
            foreach (var instruction in Instructions)
            {
                foreach (var memory_instruction in instruction.Memory_instructions)
                {
                    var first = instruction.Bit_mask.Select(x => x == '0' ? 'X' : x).Select(x => x == 'X' ? '1' : '0').ToArray();
                    var tempFirst = memory_instruction.Memory_address & Convert.ToInt64(new string(first), 2);
                    var second = instruction.Bit_mask.Select(x => x == 'X' ? '0' : x).ToArray();
                    var tempSecond = Convert.ToInt64(tempFirst) | Convert.ToInt64(new string(second), 2);
                    var tempSecondBinary = Convert.ToString(tempSecond, 2).PadLeft(36, '0');
                    var Items = instruction.Bit_mask.Select((item, index) => new {
                        ItemName = item,
                        Position = index
                    }).Where(i => i.ItemName == 'X').ToList();
                    var sb = new StringBuilder(tempSecondBinary);
                    foreach (var item in Items)
                    {
                        sb[item.Position] = 'X';
                    }
                    var tempThird = sb.ToString();

                    for (int i = 0; i < Math.Pow(2, instruction.Bit_mask.Count(x => x == 'X')); i++)
                    {
                        var binary = Convert.ToString(i, 2).PadLeft(instruction.Bit_mask.Count(x => x == 'X'), '0');
                        var new_value_full = new StringBuilder(tempThird);
                        for (int j = 0; j < instruction.Bit_mask.Count(x => x == 'X'); j++)
                        {
                            var position = Items[j].Position;
                            var new_value = binary[j];
                            new_value_full[position] = new_value;
                        }

                        var result = Convert.ToInt64(new_value_full.ToString(), 2);
                        if (!memory.Any(x => x.Address == result)) memory.Add(new Memory() { Address = result, Value = memory_instruction.Value });
                        else
                        {
                            memory.Single(x => x.Address == result).Value = memory_instruction.Value;
                        }
                    }
                }
            }
            Console.WriteLine(memory.Sum(x => x.Value));
        }
    }

    public class Instruction
    {
        public string Bit_mask { get; set; }
        public List<Memory_instruction> Memory_instructions { get; set; } = new();
    }
    public class Memory_instruction
    {
        public int Memory_address { get; set; }
        public long Value { get; set; }
    }

    public class Memory
    {
        public long Address { get; set; }
        public long Value { get; set; }
    }
}
