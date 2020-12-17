using AOC2020.Shared;
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
            var starting_side = 8;
            var number_of_rounds = 6;
            // Add other 2 layers of the 3x3x3 cube of cubes
            var part_1_field = new List<Cube>();
            for (var i = 0 - number_of_rounds; i < starting_side + number_of_rounds; i++)
            {
                for (var j = 0 - number_of_rounds; j < starting_side + number_of_rounds; j++)
                {
                    for (var k = 0 - number_of_rounds; k < starting_side + number_of_rounds; k++)
                    {
                        part_1_field.Add(new Cube() {Coordinate = new Coordinate(i, j, k), Active = false});
                    }
                }
            }
            
            foreach (var cube in _starting_field)
            {
                part_1_field.Single(x => x.Coordinate.Equals(cube.Coordinate)).Active = cube.Active;
            }

            //var test1 = part_1_field.Where(x => x.Active || part_1_field.Count(y => y.Active && y.Is_neighbour(x)) == 3);
            //PrintCubes(part_1_field);
            for (int i = 0; i < 6; i++)
            {
                var test = new List<Cube>();
                foreach (var x in part_1_field)
                {
                    var newCube = new Cube()
                    {
                        Coordinate = new Coordinate(x.Coordinate.X, x.Coordinate.Y, x.Coordinate.Z), Active = x.Active
                    };
                    var neighbours = part_1_field.Count(y => y.Active && y.Is_neighbour(x));
                    if (x.Active)
                    {
                        if (neighbours != 2 && neighbours != 3)
                            newCube.Active = false;
                    }
                    else
                    {
                        if (neighbours == 3)
                            newCube.Active = true;
                    }
                    test.Add(newCube);
                }
                part_1_field = test;
            }

            //PrintCubes(part_1_field);
            Console.WriteLine();
            Console.WriteLine(part_1_field.Count(x => x.Active));
        }


        protected override void Part2()
        {
            var starting_side = 8;
            var number_of_rounds = 6;
            // Add other 2 layers of the 3x3x3 cube of cubes
            Dictionary<(int,int,int,int), bool> testField = new Dictionary<(int, int, int, int), bool>();
            for (var i = 0 - number_of_rounds; i < starting_side + number_of_rounds; i++)
            {
                for (var j = 0 - number_of_rounds; j < starting_side + number_of_rounds; j++)
                {
                    for (var k = 0 - number_of_rounds; k < starting_side + number_of_rounds; k++)
                    {
                        for (var l = 0 - number_of_rounds; l < starting_side + number_of_rounds; l++)
                        {
                            testField.Add((i, j, k, l), false);
                        }
                    }
                }
            }
            
            foreach (var cube in _starting_field)
            {
                testField[(cube.Coordinate.X, cube.Coordinate.Y, cube.Coordinate.Z, cube.Coordinate.W)] = cube.Active;
            }
            
            for (var a = 0; a < 6; a++)
            {
                var copy = testField.ToDictionary(x => x.Key, x => x.Value);
                foreach (var keyValuePair in copy)
                {
                    var count = GetNeighbours(testField, keyValuePair);
                    
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

                testField = copy.ToDictionary(x => x.Key, x => x.Value);
            }
            Console.WriteLine(testField.Count(x => x.Value));
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
                            field.TryGetValue
                            ((keyValuePair.Key.Item1 + i, keyValuePair.Key.Item2 + j,
                                keyValuePair.Key.Item3 + k, keyValuePair.Key.Item4 + l), out var isTrue);
                            if (isTrue) count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}