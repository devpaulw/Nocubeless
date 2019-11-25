using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class SceneInputComponent : GameComponent
    {
        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;

        private int cursorSet;
        private Point middlePoint;
        private bool mouseLeftButtonPressed;

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
            World = scene.World;
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

                Yaw -= deltaPoint.X * Settings.MouseSensitivity;
                Pitch -= deltaPoint.Y * Settings.MouseSensitivity;

                rotation = Matrix.CreateRotationX(radiansPitch) *
                    Matrix.CreateRotationY(radiansYaw);
                Camera.Front = Vector3.Transform(Camera.OriginalFront, rotation);
                Camera.Up = Vector3.Transform(Camera.OriginalUp, rotation);
            }
            else cursorSet++; // Prevent bad camera arisen
            #endregion

            #region Move camera
            currentKeyboardState = Keyboard.GetState();

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
            #endregion

            #region Preview & Lay Cubes in the World
            CubeCoordinate previewCubePosition = GetWorldAvailableSpace();
            { // Prev
                World.PreviewCube(previewCubePosition);
            } // Lay
            {
                if (!mouseLeftButtonPressed && currentMouseState.RightButton == ButtonState.Pressed)
                {
                    mouseLeftButtonPressed = true;

                    Random random = new Random();
                    Color cubeColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                    Cube newCube = new Cube(cubeColor, previewCubePosition);
                    World.LayCube(newCube);
                }
                else if (currentMouseState.RightButton == ButtonState.Released)
                    mouseLeftButtonPressed = false;
            }
            #endregion

            base.Update(gameTime);
        }

        private CubeCoordinate GetWorldAvailableSpace() // Is not 100% trustworthy, and is not powerful, be careful
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