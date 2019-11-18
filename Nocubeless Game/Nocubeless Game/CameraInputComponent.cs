using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class CameraInputComponent : GameComponent
    {
        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;
        
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

        public GameInputKeys KeysInputs { get; set; }
        public Camera Camera { get; set; }
        public float MoveSpeed { get; set; } = 0.25f;
        public float MouseSensitivity { get; set; } = 0.1f;
        public float RunSpeed { get; set; }


        public CameraInputComponent(GameApp game, 
            GameInputKeys keysInputs, 
            Camera camera, 
            float moveSpeed, 
            float mouseSensitivity, 
            float runSpeed) 
            : base(game)
        {
            KeysInputs = keysInputs;
            Camera = camera;
            MoveSpeed = moveSpeed;
            MouseSensitivity = mouseSensitivity;
            RunSpeed = runSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            Point middlePoint = new Point(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Width / 2);

            currentKeyboardState = Keyboard.GetState();
            if (gameTime != null) MoveFromKeyboard(gameTime);

            currentMouseState = Mouse.GetState();
            RotateFromMouse(middlePoint);

            base.Update(gameTime);
        }

        private void MoveFromKeyboard(GameTime gameTime)
        {
            var moveDeltaSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * MoveSpeed;
            Vector3 moveX, moveY, moveZ;

            if (currentKeyboardState.IsKeyDown(KeysInputs.Run))
                moveDeltaSpeed *= RunSpeed;

            moveX = Vector3.Normalize(Vector3.Cross(Camera.Front, Camera.Up)) * moveDeltaSpeed;
            moveY = Camera.Up * moveDeltaSpeed;
            moveZ = Camera.Front * moveDeltaSpeed;

            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveForward)) Camera.Position += moveZ; // Z
            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveBackward)) Camera.Position -= moveZ; // S
            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveRight)) Camera.Position += moveX; // D
            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveLeft)) Camera.Position -= moveX; // Q
            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveUpward)) Camera.Position += moveY; // Space
            if (currentKeyboardState.IsKeyDown(KeysInputs.MoveDown)) Camera.Position -= moveY; // Left SHift
        }


        public void RotateFromMouse(Point middlePoint)
        {
            var deltaPoint = new Point(currentMouseState.X - middlePoint.X, currentMouseState.Y - middlePoint.Y);

            Yaw += deltaPoint.X * MouseSensitivity;
            Pitch -= deltaPoint.Y * MouseSensitivity;

            Camera.Front = Vector3.Normalize(new Vector3(
                (float)Math.Cos(radiansYaw) * (float)Math.Cos(radiansPitch),
                (float)Math.Sin(radiansPitch),
                (float)Math.Sin(radiansYaw) * (float)Math.Cos(radiansPitch)));
        }
    }
}
