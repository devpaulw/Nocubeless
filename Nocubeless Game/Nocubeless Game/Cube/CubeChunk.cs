using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
    class CubeChunk
    {
        public const int Size = 26; /*tmp 4!*/
        public const int TotalSize = Size * Size * Size;

        private CubeColor[] cubeColors;

        public Coordinates Coordinates { get; set; }

        public CubeColor this[int position] {
            get {
                return cubeColors[position];
            }
            set {
                cubeColors[position] = value;
            }
        }

        public CubeChunk(Coordinates coordinates)
        {
            Coordinates = coordinates;

            cubeColors = new CubeColor[Size * Size * Size];
        }

        public bool IsEmptyAt(int location)
        {
            if (cubeColors[location] == CubeColor.Empty)
                return true;
            else return false;
        }

        public static class Helper
        {
            public static Coordinates FindBaseCoordinates(Coordinates cubeCoordinates) // find real chunk coordinates from lamba cube coordinates
            {
                /// <summary>
                /// This method find the first value that is a multiple of a CubeChunk Size by crossing from the left.
                /// </summary>
                int FindCloserLeftMultiple(int value)
                {
                    while (value % CubeChunk.Size != 0)
                        value--;

                    return value;
                }

                int x = FindCloserLeftMultiple(cubeCoordinates.X),
                    y = FindCloserLeftMultiple(cubeCoordinates.Y),
                    z = FindCloserLeftMultiple(cubeCoordinates.Z);

                return new Coordinates(x, y, z);
            }
            public static int GetPositionFromCoordinates(Coordinates cubeCoordinates) // get position of the cube in the chunk from lamba cube coordinates
            {
                int GetRemainder(int value) // x < 0 please // DESIGN: Do I update this method using?
                {
                    int remainder = value % CubeChunk.Size;
                    if (value >= 0) return remainder;
                    else
                    {
                        if (remainder == 0) // when (-)x%size == 0
                            return 0;
                        else
                        {
                            return CubeChunk.Size + remainder;
                        }
                    }
                }

                int x = GetRemainder(cubeCoordinates.X),
                    y = GetRemainder(cubeCoordinates.Y),
                    z = GetRemainder(cubeCoordinates.Z);

                return x + (y * CubeChunk.Size) + (z * CubeChunk.Size * CubeChunk.Size);
            }
        }
    }
}
