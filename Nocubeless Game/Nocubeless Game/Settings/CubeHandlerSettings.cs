using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeHandlerSettings
    {
        public int MaxLayingDistance { get; set; }
        public float PreviewCubeTransparency { get; set; }

        public static CubeHandlerSettings Default {
            get {
                return new CubeHandlerSettings
                {
                    MaxLayingDistance = 24,
                    PreviewCubeTransparency = 0.2f
                };
            }
        }
    }
}
