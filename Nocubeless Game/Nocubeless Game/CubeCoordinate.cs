using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CubeCoordinate
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

        public Matrix CreateWorldTranslation(float cubesHeight)
        {
            return Matrix.CreateTranslation(ToVector3() * 2.0f * cubesHeight);
        }
    }
}
