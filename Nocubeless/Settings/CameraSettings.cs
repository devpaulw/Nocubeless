using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CameraSettings
    {
        public float Fov { get; set; }
        public float MoveSpeed { get; set; }
        public float MouseSensitivity { get; set; }

        public static CameraSettings Default {
            get {
                return new CameraSettings
                {
                    Fov = 100,
                    MoveSpeed = 1.0f,
                    MouseSensitivity = 0.01f,
                };
            }
        }
    }
}
