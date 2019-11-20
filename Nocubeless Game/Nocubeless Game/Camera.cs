using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Nocubeless
{
    internal class Camera
    {
        private float radiansFov;
        public float Fov {
            get => MathHelper.ToDegrees(radiansFov);
            set => radiansFov = MathHelper.ToRadians(value);
        }
        public float AspectRatio { get; set; }

        public Vector3 OriginalFront { get; } = -Vector3.UnitZ;
        public Vector3 OriginalUp { get; } = Vector3.UnitY;

        public Vector3 Position { get; set; }
        public Vector3 Front { get; set; }
        public Vector3 Up { get; set; }

        

        public Camera(GameApp game)
        {
            Fov = game.Settings.CameraFov;
            AspectRatio = game.GraphicsDevice.Viewport.AspectRatio;

            Position = Vector3.Zero;
            Front = OriginalFront;
            Up = OriginalUp;
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
