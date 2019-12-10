using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CubeWorldSettings
    {
        public float HeightOfCubes { get; set; }

        public static CubeWorldSettings Default {
            get {
                return new CubeWorldSettings
                {
                    HeightOfCubes = 0.1f
                };
            }
        }
    }
}
