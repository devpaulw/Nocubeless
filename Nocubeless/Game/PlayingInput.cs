using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class PlayingInput : NocubelessComponent
    {
        private Point WindowCenter { get; set; }

        public PlayingInput(Nocubeless nocubeless) : base(nocubeless)
        {
            WindowCenter = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.Playing)
            {
                // TODO move this instruction to another class, it doesn't belong to input
                Nocubeless.Player.UpdateSpeed((float)gameTime.ElapsedGameTime.TotalSeconds);
 
                ProcessInput();
                Mouse.SetPosition(WindowCenter.X, WindowCenter.Y);

            }
        }

        private void ProcessInput()
        {
            ProcessMouseInput();
            ProcessKeyboardInput();
        }

        private void ProcessMouseInput()
        {
            Vector2 movement = GetMouseMovement() / 57;
            Nocubeless.Camera.Rotate(movement.Y, -movement.X);
        }

        private void ProcessKeyboardInput()
        {
            var direction = Vector3.Zero;

            if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
            {
                Nocubeless.Player.Speed = Nocubeless.Player.Speed * 3f;
            }
            else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
            {
                Nocubeless.Player.Speed = Nocubeless.Player.Speed / 3f;
            }

            if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
            {
                Nocubeless.Player.Speed = Nocubeless.Player.Speed * 3f;
            }
            else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
            {
                Nocubeless.Player.Speed = Nocubeless.Player.Speed / 3f;
            }

            if (Input.WasJustPressed(Keys.V))
            {
                Nocubeless.Camera.Zoom(120);
            }
            else if (Input.WasJustReleased(Keys.V))
            {
                Nocubeless.Camera.Zoom(100);
            }


            if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
            {
                direction = Nocubeless.Camera.Right;
                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction = Vector3.Zero;
                }
            }
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
            {
                direction = -Nocubeless.Camera.Right;
                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction = Vector3.Zero;
                }
            }

            if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
            {
                direction += Nocubeless.Camera.Front;

                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction -= Nocubeless.Camera.Front;
                }
            }
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
            {
                direction -= Nocubeless.Camera.Front;

                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction += Nocubeless.Camera.Front;
                }
            }

            if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
            {
                direction.Y += 1;

                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction.Y -= 1;
                }
            }
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
            {
                direction.Y -= 1;
                if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
                {
                    direction.Y += 1;
                }
            }

            Nocubeless.Player.Move(direction);
        }

        private Vector2 GetMouseMovement()
        {
            return Nocubeless.Settings.Camera.MouseSensitivity
                * new Vector2(Input.CurrentMouseState.X - WindowCenter.X, Input.CurrentMouseState.Y - WindowCenter.Y);
        }

        private int GetScrollWheelMovement()
        {
            return Input.CurrentMouseState.ScrollWheelValue - Input.OldMouseState.ScrollWheelValue;
        }
    }
}
