using System;

namespace Days
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int Manhattan_distance => Math.Abs(X) + Math.Abs(Y);
    }

}
