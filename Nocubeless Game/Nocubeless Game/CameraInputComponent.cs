using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CameraInputComponent : GameComponent
    {
        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;

        private int cursorSet;
        private Point middlePoint;

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

        public InputKeySettings InputKeys { get; set; }
        public Camera Camera { get; set; }
        public CameraSettings CameraSettings { get; set; }

        public CameraInputComponent(Game game, Camera camera, GameSettings settings) : base(game) // TODO: Organize settings ; I don't know how to organize settings.. 
        {
            InputKeys = settings.InputKeys;
            CameraSettings = settings.Camera;
            Camera = camera;
        }

        public override void Initialize()
        {
            middlePoint = new Point(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            #region Rotate head
            currentMouseState = Mouse.GetState();
            Mouse.SetPosition(middlePoint.X, middlePoint.Y);

            if (cursorSet > 1)
            {
                Matrix rotation;

                var deltaPoint = new Point(currentMouseState.X - middlePoint.X, currentMouseState.Y - middlePoint.Y);
                //lastCursorPosition = new Point(currentMouseState.X, currentMouseState.Y);

                Yaw -= deltaPoint.X * CameraSettings.MouseSensitivity;
                Pitch -= deltaPoint.Y * CameraSettings.MouseSensitivity;

                rotation = Matrix.CreateRotationX(radiansPitch) *
                    Matrix.CreateRotationY(radiansYaw);
                Camera.Front = Vector3.Transform(Camera.OriginalFront, rotation);
                Camera.Up = Vector3.Transform(Camera.OriginalUp, rotation);
            }
            else cursorSet++; // Prevent bad camera arisen
            #endregion

            #region Move camera
            currentKeyboardState = Keyboard.GetState();

            var moveDeltaSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * CameraSettings.MoveSpeed;
            Vector3 xAxis, yAxis, zAxis;

            if (currentKeyboardState.IsKeyDown(InputKeys.Run))
                moveDeltaSpeed *= 2.5f;

            xAxis = Vector3.Normalize(Vector3.Cross(Camera.Front, Camera.Up)) * moveDeltaSpeed;
            yAxis = Camera.Up * moveDeltaSpeed;
            zAxis = Camera.Front * moveDeltaSpeed;

            if (currentKeyboardState.IsKeyDown(InputKeys.MoveForward)) Camera.Position += zAxis; // Z
            if (currentKeyboardState.IsKeyDown(InputKeys.MoveBackward)) Camera.Position -= zAxis; // S
            if (currentKeyboardState.IsKeyDown(InputKeys.MoveRight)) Camera.Position += xAxis; // D
            if (currentKeyboardState.IsKeyDown(InputKeys.MoveLeft)) Camera.Position -= xAxis; // Q
            if (currentKeyboardState.IsKeyDown(InputKeys.MoveUpward)) Camera.Position += yAxis; // Space
            if (currentKeyboardState.IsKeyDown(InputKeys.MoveDown)) Camera.Position -= yAxis; // Left SHift
            #endregion

            base.Update(gameTime);
        }
    }
}