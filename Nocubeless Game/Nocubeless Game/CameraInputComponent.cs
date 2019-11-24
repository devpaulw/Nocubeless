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

        public GameInputKeys InputKeys { get; set; }
        public Camera Camera { get; set; }
        public float MoveSpeed { get; set; }
        public float MouseSensitivity { get; set; }

        public CameraInputComponent(IGameApp game, Camera camera) : base(game.Instance)
        {
            InputKeys = game.Settings.InputKeys;
            MoveSpeed = game.Settings.MoveSpeed;
            MouseSensitivity = game.Settings.MouseSensitivity;
            Camera = Camera;

            // Camera Settings ? (graphics, inputs, camera, world settings)
             // put every world in the world class (with effect for example)
             // advise from me: think about world returns over world private statements and then update World (scene, drawableCubes d-update)

            middlePoint = new Point(game.Instance.GraphicsDevice.Viewport.Width / 2, game.Instance.GraphicsDevice.Viewport.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            if (cursorSet > 1) RotateFromMouse();
            else cursorSet++; // Prevent bad camera arisen

            Mouse.SetPosition(middlePoint.X, middlePoint.Y); 
            
            currentKeyboardState = Keyboard.GetState();
            if (gameTime != null) MoveFromKeyboard(gameTime);

            base.Update(gameTime);
        }

        private void MoveFromKeyboard(GameTime gameTime)
        {
            var moveDeltaSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * MoveSpeed;
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
        }

        public void RotateFromMouse()
        {
            Matrix rotation;

            var deltaPoint = new Point(currentMouseState.X - middlePoint.X, currentMouseState.Y - middlePoint.Y);
            //lastCursorPosition = new Point(currentMouseState.X, currentMouseState.Y);

            Yaw -= deltaPoint.X * MouseSensitivity;
            Pitch -= deltaPoint.Y * MouseSensitivity;

            rotation = Matrix.CreateRotationX(radiansPitch) *
                Matrix.CreateRotationY(radiansYaw);
            Camera.Front = Vector3.Transform(Camera.OriginalFront, rotation);
            Camera.Up = Vector3.Transform(Camera.OriginalUp, rotation);

            // Before - deprecated
            //Camera.Front = Vector3.Normalize(new Vector3(
            //    (float)Math.Cos(radiansYaw) * (float)Math.Cos(radiansPitch),
            //    (float)Math.Sin(radiansPitch),
            //    (float)Math.Sin(radiansYaw) * (float)Math.Cos(radiansPitch)));
        }
    }
}
