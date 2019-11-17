using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nocubeless
{
    public class Camera
    {
        private float radiansFov = MathHelper.PiOver2; // Pi/3 or a 60 degrees Fov default value
        public float Fov {
            get => MathHelper.ToDegrees(radiansFov);
            set => radiansFov = MathHelper.ToRadians(value);
        }
        public float AspectRatio { get; set; }
            = 1.0F;

        public Vector3 Position { get; set; }
             = Vector3.Zero;
        public Vector3 Front { get; set; }
             = -Vector3.UnitZ;
        public Vector3 Up { get; set; }
             = Vector3.UnitY;

        public Camera()
        {
            
        }

        public Matrix ProjectionMatrix {
            get {
                const float zNear = 0.05f, zFar = 50f;
                return Matrix.CreatePerspectiveFieldOfView(radiansFov,
                    AspectRatio,
                    zNear, zFar);
            }
        }

        public Matrix ViewMatrix {
            get {
                Vector3 target = Position + Front;
                return Matrix.CreateLookAt(Position, target, Up);
            }
        }

        public Matrix WorldMatrix {
            get {
                Vector3 target = Position + Front;
                return Matrix.CreateWorld(target, Vector3.Forward, Vector3.Up);
            }
        }
    }
}
