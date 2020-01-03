using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerInputProcessor : NocubelessComponent
	{
		private int cursorSet = 0;

		public PlayerInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			Nocubeless.Camera.Speed = Nocubeless.CubeWorld.GetGraphicsCubeRatio() / 2;

			//Nocubeless.Camera.Position = Nocubeless.Player.Position;
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				
				if (cursorSet > 1)
				{
					Vector2 movement = Nocubeless.Input.GetMouseMovement();
					Nocubeless.Camera.Rotate(movement.Y, -movement.X);
				}
				else
				{
					cursorSet++; // Prevent bad camera arisen
				}
				

				var direction = Vector3.Zero;
				if (Input.IsPressed(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed * 3f;
				}
				else if (Input.IsReleased(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed / 3f;
				}


				if (GameInput.IsKeyPressed(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed * 3f;
				}
				else if (GameInput.IsKeyReleased(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Player.Speed = Nocubeless.Player.Speed / 3f;
				}

				Nocubeless.Player.UpdateSpeed((float)gameTime.ElapsedGameTime.TotalSeconds);

				if (Input.IsPressed(Keys.V))
				{
					Nocubeless.Camera.Zoom(120);
				}
				else if (Input.IsReleased(Keys.V))
				{
					Nocubeless.Camera.Zoom(100);
				}
				

				if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
				{
					direction = Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction = Vector3.Zero;
					}
				}
				else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
				{
					direction = -Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction = Vector3.Zero;
					}
				}

				if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
				{

					direction += Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction -= Nocubeless.Camera.Front;
					}
				}
				else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
				{
					direction -= Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction += Nocubeless.Camera.Front;
					}
				}

				if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
				{
					direction.Y += 1;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
					{
						direction.Y -= 1;
					}
				}
				else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
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
