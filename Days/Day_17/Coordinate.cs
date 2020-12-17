using System;

namespace Days
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public Coordinate(int x, int y, int z, int w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool Equals(Coordinate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinate) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public bool Is_neighbour(Coordinate other)
        {
            return !this.Equals(other) &&
                   X - 1 <= other.X && other.X <= X + 1 &&
                   Y - 1 <= other.Y && other.Y <= Y + 1 &&
                   Z - 1 <= other.Z && other.Z <= Z + 1 &&
                   W - 1 <= other.W && other.W <= W + 1;
        }
    }
}