using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeGo
{
    struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Matrix4 CreateSceneTranslation(float cubeHeight)
        {
            float gap = cubeHeight * 2.0f;
            return Matrix4.CreateTranslation(
                    X * gap,
                    Y * gap,
                    Z * gap);
        }
    }
}
