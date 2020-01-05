using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
    public class CubeChunk
    {
        public const int Size = 16;
        public const int TotalSize = Size * Size * Size;

        private readonly CubeColor[] cubeColors;

        public CubeCoordinates Coordinates { get; set; }

        public CubeColor this[int position] {
            get {
                return cubeColors[position];
            }
            set {
                cubeColors[position] = value;
            }
        }

        public CubeChunk(CubeCoordinates coordinates)
        {
            Coordinates = coordinates;

            cubeColors = new CubeColor[Size * Size * Size];
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < cubeColors.Length; i++)
                if (cubeColors[i] != null)
                    return false;

            return true;
        }

        //public IEnumerator<Cube> GetEnumerator()
        //{
        //    for (int i = 0; i < cubeColors.Length; i++)
        //    {
        //        if (!IsEmptyAt(i)) // is not a void case
        //        {
        //            yield return new Cube();
        //        }
        //    }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
