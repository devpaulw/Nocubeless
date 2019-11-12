using OpenTK;
using OpenTK.Input;
using System;

namespace HereWeGo
{
    public partial class Camera // Inputs
    {
        private const float MoveSpeed = 0.25f;
        private const float MouseSensitivity = 0.1f;
        private Vector2 lastCursorPosition = Vector2.Zero;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        public float Pitch {
            get => MathHelper.RadiansToDegrees(_pitch);
            set {
                float angle = MathHelper.Clamp(value, -89f, 89f); // Prevent Weird Pitch Movements
                _pitch = MathHelper.DegreesToRadians(angle);
            }
        }
        public float Yaw {
            get => MathHelper.RadiansToDegrees(_yaw);
            set {
                _yaw = MathHelper.DegreesToRadians(value);
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
            if (keyboardInput.IsKeyDown(Key.Q)) run = true;

            if (keyboardInput.IsKeyDown(Key.W)) Move(Front, run, currentTime); // Move Forward
            if (keyboardInput.IsKeyDown(Key.S)) Move(-Front, run, currentTime); // Move Backward
            if (keyboardInput.IsKeyDown(Key.A)) Move(-Vector3.Normalize(Vector3.Cross(Front, Up)), run, currentTime); // Move Left
            if (keyboardInput.IsKeyDown(Key.D)) Move(Vector3.Normalize(Vector3.Cross(Front, Up)), run, currentTime); // Move Right
            if (keyboardInput.IsKeyDown(Key.Space)) Move(Up, run, currentTime); // Move Upward
            if (keyboardInput.IsKeyDown(Key.ShiftLeft)) Move(-Up, run, currentTime); // Move Down
        }

        public void RotateFromMouse(MouseState mouseInput)
        {
            float deltaX = mouseInput.X - lastCursorPosition.X;
            float deltaY = mouseInput.Y - lastCursorPosition.Y;
            lastCursorPosition = new Vector2(mouseInput.X, mouseInput.Y);

            Yaw += deltaX * MouseSensitivity;
            Pitch -= deltaY * MouseSensitivity;

            _front = Vector3.Normalize(new Vector3(
                (float)Math.Cos(_yaw) * (float)Math.Cos(_pitch),
                (float)Math.Sin(_pitch),
                (float)Math.Sin(_yaw) * (float)Math.Cos(_pitch)));
        }
    }
}
