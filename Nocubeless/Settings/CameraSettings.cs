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
                    // SDNMSG: removed MoveSpeed because it's replaced with PlayerSettings
                    MouseSensitivity = 0.175f,
                };
            }
        }
    }
}
