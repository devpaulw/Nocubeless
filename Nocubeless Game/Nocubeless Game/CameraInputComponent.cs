using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class SceneInputComponent : DrawableGameComponent
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
        public CameraSettings Settings { get; set; }
        public World World { get; set; }

        public SceneInputComponent(IGameApp game, Scene scene) : base(game.Instance)
        {
            InputKeys = game.Settings.InputKeys;
            Settings = game.Settings.Camera;
            Camera = scene.Camera;

            middlePoint = new Point(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void MoveFromKeyboard(GameTime gameTime)
        {
            var moveDeltaSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * Settings.MoveSpeed;
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

            Yaw -= deltaPoint.X * Settings.MouseSensitivity;
            Pitch -= deltaPoint.Y * Settings.MouseSensitivity;

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

        public void PreviewCube()
        {
            World.PreviewCube(GetAvailableSpace());
        }

        public void LayCube() // en plus on avait mousestate
        {
            Random rnd = new Random();
            Color cubeColor = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            Cube newCube = new Cube(cubeColor, GetAvailableSpace());
            World.LayCube(newCube);
        }

        public CubeCoordinate GetAvailableSpace() // Is not 100% trustworthy, and is not powerful, be careful
        {
            float sceneCubeRatio = 1.0f / World.Settings.HeightOfCubes / 2.0f; // Because a cube is x times smaller/bigger compared to the scene representation
            // cube ratio in world

            Vector3 checkPosition = Camera.Position * sceneCubeRatio;

            CubeCoordinate oldPosition = null;
            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            int checkIntensity = 40;
            float checkIncrement = 4 / (float)checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front /*Fix design there*/ * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !World.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return oldPosition;
                    else if (actualPosition != null) // Or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
    }
}