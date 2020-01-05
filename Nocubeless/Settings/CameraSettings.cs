using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CameraSettings
    {
        public float DefaultFov { get; set; }
        public float MoveSpeed { get; set; }
        public float DefaultMouseSensitivity { get; set; }
        public int ZoomPercentage { get; set; }
        public float MouseSensitivityWhenZooming { get; set; }

        public static CameraSettings Default {
            get {
                return new CameraSettings
                {
                    DefaultFov = 100,
                    ZoomPercentage = 200,
                    DefaultMouseSensitivity = 0.175f,
                    MouseSensitivityWhenZooming = 0.075f
                };
            }
        }

    }
}
