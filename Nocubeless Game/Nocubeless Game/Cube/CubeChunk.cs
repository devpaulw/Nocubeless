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
        public const int Size = 8; /*tmp 4!*/

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
            ///*tmp*/ for (int i = 0; i < Size * Size * Size; i++)
            //    cubeColors[i] = new CubeColor(0, 0, 7);
        }
    }
}
