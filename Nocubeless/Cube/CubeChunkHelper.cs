using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public static class CubeChunkHelper
    {
        public static CubeCoordinates FindBaseCoordinates(CubeCoordinates cubeCoordinates) // find real chunk coordinates from lamba cube coordinates
        {
            if (cubeCoordinates == null)
                throw new NullReferenceException();

            int x = FindCloserLeftMultiple(cubeCoordinates.X),
                y = FindCloserLeftMultiple(cubeCoordinates.Y),
                z = FindCloserLeftMultiple(cubeCoordinates.Z);

            return new CubeCoordinates(x, y, z);

            /// <summary>
            /// This method find the first value that is a multiple of a CubeChunk Size by crossing from the left.
            /// </summary>
            int FindCloserLeftMultiple(int value)
            {
                while (value % CubeChunk.Size != 0)
                    value--;

                return value;
            }
        }
        public static int GetIndexFromCoordinates(CubeCoordinates cubeCoordinates) // get position of the cube in the chunk from lamba cube coordinates
        {
            if (cubeCoordinates == null)
                throw new NullReferenceException();

            int x = GetRemainder(cubeCoordinates.X),
                y = GetRemainder(cubeCoordinates.Y),
                z = GetRemainder(cubeCoordinates.Z);

            return x + (y * CubeChunk.Size) + (z * CubeChunk.Size * CubeChunk.Size);

            int GetRemainder(int value) // x < 0 please
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
    }
}
