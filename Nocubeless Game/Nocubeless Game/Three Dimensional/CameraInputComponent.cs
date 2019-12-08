using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CameraInputComponent : NocubelessInputComponent
    {
        private int cursorSet;

        private float radiansPitch;
        private float radiansYaw;
        private float Pitch {
            get => MathHelper.ToDegrees(radiansPitch);
            set {
                float angle = MathHelper.Clamp(value, -89f, 89f); // Prevent Weird Pitch Movements
                radiansPitch = MathHelper.ToRadians(angle);
            }
        }
        private float Yaw {
            get => MathHelper.ToDegrees(radiansYaw);
            set {
                radiansYaw = MathHelper.ToRadians(value);
            }
        }

        public CameraInputComponent(Nocubeless nocubeless) : base(nocubeless) { }

        public override void Update(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.Playing)
            {
                ReloadCurrentStates();

                Move(gameTime);
                if (cursorSet > 1) /*->*/ Rotate(); /*<-*/ else cursorSet++; // Prevent bad camera arisen

                SetMouseInTheMiddle();
            }

            base.Update(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            var moveDeltaSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * Nocubeless.Settings.Camera.MoveSpeed;
            Vector3 xAxis, yAxis, zAxis;

            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.Run))
                moveDeltaSpeed *= 2.5f;

            xAxis = Vector3.Normalize(Vector3.Cross(Nocubeless.Camera.Front, Nocubeless.Camera.Up)) * moveDeltaSpeed;
            yAxis = Nocubeless.Camera.Up * moveDeltaSpeed;
            zAxis = Nocubeless.Camera.Front * moveDeltaSpeed;

            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward)) Nocubeless.Camera.Position += zAxis; // Z
            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward)) Nocubeless.Camera.Position -= zAxis; // S
            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight)) Nocubeless.Camera.Position += xAxis; // D
            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft)) Nocubeless.Camera.Position -= xAxis; // Q
            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward)) Nocubeless.Camera.Position += yAxis; // Space
            if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown)) Nocubeless.Camera.Position -= yAxis; // Left SHift
        }

        private void Rotate()
        {
            Matrix rotation;

            var deltaPoint = new Point(CurrentMouseState.X - MiddlePoint.X, CurrentMouseState.Y - MiddlePoint.Y);
            //lastCursorPosition = new Point(currentMouseState.X, currentMouseState.Y);

            Yaw -= deltaPoint.X * Nocubeless.Settings.Camera.MouseSensitivity;
            Pitch -= deltaPoint.Y * Nocubeless.Settings.Camera.MouseSensitivity;

            rotation = Matrix.CreateRotationX(radiansPitch) *
                Matrix.CreateRotationY(radiansYaw);
            Nocubeless.Camera.Front = Vector3.Transform(Nocubeless.Camera.OriginalFront, rotation);
            Nocubeless.Camera.Up = Vector3.Transform(Nocubeless.Camera.OriginalUp, rotation);
        }
    }
}