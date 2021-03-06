﻿using AOC2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Days
{
    public class Day_17 : Day
    {
        private List<Cube> _starting_field;

        public Day_17()
        {
            Title = "Conway Cubes";
            DayNumber = 17;
        }

        protected override void Gather_input()
        {
            _starting_field = Read_file()
                .SelectMany((x, xi) => x.Select((y, yi) => Cube.Create_from_char_and_indices(y, yi, xi, x.Length)).ToList())
                .ToList();
        }

        protected override void Part1()
        {
            var starting_side = _starting_field.Max(x => x.Coordinate.Y + 1);
            var number_of_rounds = 6;
            // Add other 2 layers of the 3x3x3 cube of cubes
            var field = new Dictionary<(int, int, int), bool>();
            for (var i = 0 - number_of_rounds; i < starting_side + number_of_rounds; i++)
            {
                for (var j = 0 - number_of_rounds; j < starting_side + number_of_rounds; j++)
                {
                    for (var k = 0 - number_of_rounds; k < starting_side + number_of_rounds; k++)
                    {

                            field.Add((i, j, k), false);
                    }
                }
            }
            
            foreach (var cube in _starting_field)
            {
                field[(cube.Coordinate.X, cube.Coordinate.Y, cube.Coordinate.Z)] = cube.Active;
            }

            //var test1 = part_1_field.Where(x => x.Active || part_1_field.Count(y => y.Active && y.Is_neighbour(x)) == 3);
            //PrintCubes(part_1_field);
            for (var a = 0; a < 6; a++)
            {
                var copy = field.ToDictionary(x => x.Key, x => x.Value);
                foreach (var keyValuePair in copy)
                {
                    var count = GetNeighbours(field, keyValuePair);
                    
                    if (keyValuePair.Value)
                    {
                        if (count != 2 && count != 3)
                            copy[keyValuePair.Key] = false;
                    }
                    else
                    {
                        if (count == 3)
                            copy[keyValuePair.Key] = true;
                    }
                }

                field = copy.ToDictionary(x => x.Key, x => x.Value);
            }
            
            Console.WriteLine();
            Console.WriteLine(field.Count(x => x.Value));
        }


        protected override void Part2()
        {
            var starting_side = _starting_field.Max(x => x.Coordinate.Y + 1);
            var number_of_rounds = 6;
            // Add other 2 layers of the 3x3x3 cube of cubes
            var field = new Dictionary<(int, int, int, int), bool>();
            for (var i = 0 - number_of_rounds; i < starting_side + number_of_rounds; i++)
            {
                for (var j = 0 - number_of_rounds; j < starting_side + number_of_rounds; j++)
                {
                    for (var k = 0 - number_of_rounds; k < starting_side + number_of_rounds; k++)
                    {
                        for (var l = 0 - number_of_rounds; l < starting_side + number_of_rounds; l++)
                        {
                            field.Add((i, j, k, l), false);
                        }
                    }
                }
            }
            
            foreach (var cube in _starting_field)
            {
                field[(cube.Coordinate.X, cube.Coordinate.Y, cube.Coordinate.Z, cube.Coordinate.W)] = cube.Active;
            }
            
            for (var a = 0; a < 6; a++)
            {
                var copy = field.ToDictionary(x => x.Key, x => x.Value);
                foreach (var keyValuePair in copy)
                {
                    var count = GetNeighbours(field, keyValuePair);
                    
                    if (keyValuePair.Value)
                    {
                        if (count != 2 && count != 3)
                            copy[keyValuePair.Key] = false;
                    }
                    else
                    {
                        if (count == 3)
                            copy[keyValuePair.Key] = true;
                    }
                }

                field = copy.ToDictionary(x => x.Key, x => x.Value);
            }
            Console.WriteLine(field.Count(x => x.Value));
        }

        private int GetNeighbours(Dictionary<(int, int, int, int), bool> field, KeyValuePair<(int, int, int, int), bool> keyValuePair)
        {
            var count = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    for (var k = -1; k <= 1; k++)
                    {
                        for (var l = -1; l <= 1; l++)
                        {
                            if (i == 0 && j == 0 && k == 0 && l == 0) continue;
                            field.TryGetValue((keyValuePair.Key.Item1 + i, keyValuePair.Key.Item2 + j,
                                keyValuePair.Key.Item3 + k, keyValuePair.Key.Item4 + l), out var is_active);
                            if (is_active) count++;
                        }
                    }
                }
            }

            return count;
        }
        
        private int GetNeighbours(Dictionary<(int, int, int), bool> field, KeyValuePair<(int, int, int), bool> keyValuePair)
        {
            var count = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    for (var k = -1; k <= 1; k++)
                    {
                        
                            if (i == 0 && j == 0 && k == 0) continue;
                            field.TryGetValue((keyValuePair.Key.Item1 + i, keyValuePair.Key.Item2 + j,
                                keyValuePair.Key.Item3 + k), out var is_active);
                            if (is_active) count++;
                        
                    }
                }
            }

            return count;
        }
    }
}