using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        private const float MoveSpeed = 0.25f;
        private const float MouseSensitivity = 0.1f;
        private Vector2 lastCursorPosition = Vector2.Zero;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        public float Pitch {
            get => MathHelper.ToDegrees(_pitch);
            set {
                float angle = MathHelper.Clamp(value, -89f, 89f); // Prevent Weird Pitch Movements
                _pitch = MathHelper.ToRadians(angle);
            }
        }
        public float Yaw {
            get => MathHelper.ToDegrees(_yaw);
            set {
                _yaw = MathHelper.ToRadians(value);
            }
        }

        public void Move(Vector3 toDirection, bool run, double currentTime)
        {
            float deltaSpeed = MoveSpeed * (float)currentTime;
            if (run) deltaSpeed *= 2.5f;

            Vector3 move = toDirection * deltaSpeed;
            Position += move;
        }

        public void MoveFromKeyboard(KeyboardState keyboardInput, double currentTime)
        {
            bool run = false;
            if (keyboardInput.IsKeyDown(Keys.Q)) run = true;

            if (keyboardInput.IsKeyDown(Keys.W)) Move(Front, run, currentTime); // Move Forward
            if (keyboardInput.IsKeyDown(Keys.S)) Move(-Front, run, currentTime); // Move Backward
            if (keyboardInput.IsKeyDown(Keys.A)) Move(-Vector3.Normalize(Vector3.Cross(Front, Up)), run, currentTime); // Move Left
            if (keyboardInput.IsKeyDown(Keys.D)) Move(Vector3.Normalize(Vector3.Cross(Front, Up)), run, currentTime); // Move Right
            if (keyboardInput.IsKeyDown(Keys.Space)) Move(Up, run, currentTime); // Move Upward
            if (keyboardInput.IsKeyDown(Keys.LeftShift)) Move(-Up, run, currentTime); // Move Down
        }

        public void RotateFromMouse(MouseState mouseInput)
        {
            float deltaX = mouseInput.X - lastCursorPosition.X;
            float deltaY = mouseInput.Y - lastCursorPosition.Y;
            lastCursorPosition = new Vector2(mouseInput.X, mouseInput.Y);

            Yaw += deltaX * MouseSensitivity;
            Pitch -= deltaY * MouseSensitivity;

            Front = Vector3.Normalize(new Vector3(
                (float)Math.Cos(_yaw) * (float)Math.Cos(_pitch),
                (float)Math.Sin(_pitch),
                (float)Math.Sin(_yaw) * (float)Math.Cos(_pitch)));
        }
    }
}
