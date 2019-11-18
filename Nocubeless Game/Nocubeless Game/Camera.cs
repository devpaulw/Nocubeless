using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Nocubeless
{
    public class Camera
    {
        private float radiansFov;
        public float Fov {
            get => MathHelper.ToDegrees(radiansFov);
            set => radiansFov = MathHelper.ToRadians(value);
        }
        public float AspectRatio { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Front { get; set; }
        public Vector3 Up { get; set; }

        public Camera()
        {
            Fov = 60;
            AspectRatio = 1.0F;
            Position =Vector3.Zero;
            Front = -Vector3.UnitZ;
            Up = Vector3.UnitY;
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
    }
}
