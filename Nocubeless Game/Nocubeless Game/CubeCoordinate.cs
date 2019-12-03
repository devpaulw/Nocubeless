using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public CubeCoordinate(int x, int y, int z)
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
            if (!(obj is CubeCoordinate))
                return false;

            var other = (CubeCoordinate)obj;

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
            return "{X:" + X + " Y:" + Y + " Z:" + Z + "}";
        }
    }

    internal static class CubeCoordinateExtension
    {
        public static CubeCoordinate ToCubeCoordinate(this Vector3 position)
        {
            return new CubeCoordinate((int)Math.Round(position.X), (int)Math.Round(position.Y), (int)Math.Round(position.Z));
        }
    }
}
