using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    static class CubeChunkHelper
    {
        public static Coordinates FindBaseCoordinates(Coordinates cubeCoordinates) // find real chunk coordinates from lamba cube coordinates
        {
            int x = FindCloserLeftMultiple(cubeCoordinates.X),
                y = FindCloserLeftMultiple(cubeCoordinates.Y),
                z = FindCloserLeftMultiple(cubeCoordinates.Z);

            //Console.WriteLine(new Coordinates(x, y, z));

            return new Coordinates(x, y, z);
        }
        public static int GetPositionFromCoordinates(Coordinates cubeCoordinates) // get position of the cube in the chunk from lamba cube coordinates
        {
            int x = GetRemainder(cubeCoordinates.X),
                y = GetRemainder(cubeCoordinates.Y),
                z = GetRemainder(cubeCoordinates.Z);

            return x + (y * CubeChunk.Size) + (z * CubeChunk.Size * CubeChunk.Size);
        }

        #region Private Statements
        /// <summary>
        /// This method find the first value that is a multiple of a CubeChunk Size by crossing from the left.
        /// </summary>
        private static int FindCloserLeftMultiple(int value)
        {
            while (value % CubeChunk.Size != 0)
                value--;

            return value;
        }

        public static int GetRemainder(int value) // x < 0 please // DESIGN: Do I update this method using?
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
    }
    #endregion
}
