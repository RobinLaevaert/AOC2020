using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_12 : Day
    {
        List<Instruction> instructions;
        public Day_12()
        {
            Title = "Rain Risk";
            DayNumber = 12;
        }
        public override void Gather_input()
        {
            instructions = Read_file().Select(Instruction.Create_from_input_string).ToList();
        }

        public override void Part1()
        {
            var current_direction = Wind_direction.E;
            var current_coordinate = new Coordinate(0, 0);
            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.F:
                        Move(current_coordinate, current_direction, instruction.Steps);
                        break;
                    case Direction.R:
                        current_direction = Change_direction(current_direction, instruction.Steps/90);
                        break;
                    case Direction.L:
                        current_direction = Change_direction(current_direction, -(instruction.Steps/90));
                        break;
                    default:
                        Move(current_coordinate, (Wind_direction)instruction.Direction, instruction.Steps);
                        break;
                }
            }
            Console.WriteLine(current_coordinate.Manhattan_distance);
        }

        public override void Part2()
        {
            var current_ship_coordinate = new Coordinate(0, 0);
            var current_waitpoint_coordinate = new Coordinate(10, 1);

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.F:
                        Move(current_ship_coordinate, current_waitpoint_coordinate, instruction.Steps);
                        break;
                    case Direction.R:
                        Turn(current_waitpoint_coordinate, instruction.Steps / 90);
                        break;
                    case Direction.L:
                        Turn(current_waitpoint_coordinate, -(instruction.Steps / 90));
                        break;
                    default:
                        Move(current_waitpoint_coordinate, (Wind_direction)instruction.Direction, instruction.Steps);
                        break;
                }
            }
            Console.WriteLine(current_ship_coordinate.Manhattan_distance);
        }

        public static Wind_direction Change_direction(Wind_direction curr_dir, int steps) 
        {
            var temp = (Wind_direction)(((int)curr_dir + steps) % 4);
            if (temp < 0) return 4 + temp;
            else return temp;
        }

        public static void Move(Coordinate current_coordinate, Wind_direction direction, int steps)
        {
            switch (direction)
            {
                case Wind_direction.N:
                    current_coordinate.Y += steps;
                    break;
                case Wind_direction.E:
                    current_coordinate.X += steps;
                    break;
                case Wind_direction.S:
                    current_coordinate.Y -= steps;
                    break;
                case Wind_direction.W:
                    current_coordinate.X -= steps;
                    break;
            }
        }

        public static void Move(Coordinate current_coordinate, Coordinate waitpoint, int steps)
        {
            current_coordinate.X += (steps * waitpoint.X);
            current_coordinate.Y += (steps * waitpoint.Y);
        }

        public static void Turn(Coordinate current_coordinate, int steps)
        {
            var stepsRefined = steps % 4;
            if (stepsRefined < 0) stepsRefined += 4;
            for (int i = 0; i < stepsRefined; i++)
            {
                var tempX = current_coordinate.Y;
                var tempY = -current_coordinate.X;
                current_coordinate.X = tempX;
                current_coordinate.Y = tempY;
            }
        }
    }

}
