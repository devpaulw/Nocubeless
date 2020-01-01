using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class Coordinates : IEquatable<Coordinates> // TODO: Fix equal and use the true operator.
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinates);
        }

        public bool Equals(Coordinates other)
        {
            return X == other.X &&
                Y == other.Y &&
                Z == other.Z;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        public override string ToString()
        {
            return "{X:" + X + "; Y:" + Y + "; Z:" + Z + "}";
        }

        public static bool operator>(Coordinates left, Coordinates right)
        {
            return left.X > right.X
                || left.Y > right.Y
                || left.Z > right.Z;
        }
        public static bool operator<(Coordinates left, Coordinates right)
        {
            return left.X < right.X
                || left.Y < right.Y
                || left.Z < right.Z;
        }
    }

    internal static class CoordinateExtension
    {
        public static Coordinates ToCubeCoordinate(this Vector3 position) // TODO: This is not clean, put it in the real class.
        {
            return new Coordinates((int)Math.Round(position.X), (int)Math.Round(position.Y), (int)Math.Round(position.Z));
        }
    }
}
