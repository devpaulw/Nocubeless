using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class WorldSettings
    {
        public float HeightOfCubes { get; set; }

        public static WorldSettings Default {
            get {
                return new WorldSettings
                {
                    HeightOfCubes = 0.1f
                }
            }
        }
    }
}
