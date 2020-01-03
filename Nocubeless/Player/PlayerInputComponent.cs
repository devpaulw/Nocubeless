using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerInputComponent : NocubelessComponent
	{
		private int cursorSet = 0;

		public PlayerInputComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			Nocubeless.Camera.Speed = Nocubeless.CubeWorld.GetGraphicsCubeRatio() / 2;

			//Nocubeless.Camera.Position = Nocubeless.Player.Position;
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				Point middlePoint = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
				Point deltaPoint;

				if (cursorSet > 1)
				{
					deltaPoint = new Point(GameInput.CurrentMouseState.X - middlePoint.X, GameInput.CurrentMouseState.Y - middlePoint.Y);
					Nocubeless.Camera.Rotate(deltaPoint.Y * Nocubeless.Settings.Camera.MouseSensitivity, -deltaPoint.X * Nocubeless.Settings.Camera.MouseSensitivity);

				}
				else
				{
					cursorSet++; // Prevent bad camera arisen
				}
				Mouse.SetPosition(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);

				var direction = Vector3.Zero;


				if (GameInput.IsKeyPressed(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed * 3f;
				}
				else if (GameInput.IsKeyReleased(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed / 3f;
				}

				Nocubeless.Player.UpdateSpeed((float)gameTime.ElapsedGameTime.TotalSeconds);

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
				{
					direction = Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction = Vector3.Zero;
					}
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
				{
					direction = -Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction = Vector3.Zero;
					}
				}

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
				{

					direction += Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction -= Nocubeless.Camera.Front;
					}
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
				{
					direction -= Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction += Nocubeless.Camera.Front;
					}
				}

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
				{
					direction.Y += 1;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction.Y -= 1;
					}
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
				{
					direction.Y -= 1;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction.Y += 1;
					}
				}

				Nocubeless.Player.Move(direction);
				Nocubeless.Camera.Position = Nocubeless.Player.Position;
			}

			base.Update(gameTime);
		}
	}
}
