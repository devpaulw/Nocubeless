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
        public int MaxLayingDistance { get; set; }

        public static CameraSettings Default {
            get {
                return new CameraSettings
                {
                    Fov = 70,
                    MoveSpeed = 0.75f,
                    MouseSensitivity = 0.175f,
                    MaxLayingDistance = 16
                };
            }
        }
    }
}
