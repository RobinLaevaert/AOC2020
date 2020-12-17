using System;

namespace Days
{
    public class Cube
    {
        public Cube()
        {
        }

        public Coordinate Coordinate { get; set; }
        public bool Active { get; set; }

        public static Cube Create_from_char_and_indices(char input, int x, int y, int width)
        {
            return new Cube
            {
                Active = input == '#',
                Coordinate = new Coordinate(x, y, (int) Math.Floor((double) width / 2))
            };
        }

        public bool Is_neighbour(Cube cube)
        {
            return Coordinate.Is_neighbour(cube.Coordinate);
        }
    }
}