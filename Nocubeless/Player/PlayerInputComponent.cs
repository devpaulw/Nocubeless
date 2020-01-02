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
		private PlayerEntity PlayerEntity;
		public PlayerInputComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			PlayerEntity = new PlayerEntity(new Vector3(0, 0, 0), 1, 3, 1, Nocubeless.CubeWorld.GetGraphicsCubeRatio() / 2);
			Nocubeless.Camera.Speed = Nocubeless.CubeWorld.GetGraphicsCubeRatio() / 2;
			//Nocubeless.Camera.Position = PlayerEntity.Position;
		}
		private int cursorSet = 0;

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


				float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
				var direction = Vector3.Zero;

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.Run))
				{
					Nocubeless.Camera.Speed = 2.5f;
				}

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
				{
					direction = Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(PlayerEntity.GetNextPosition(deltaTime * direction))))
					{
						direction = Vector3.Zero;
					}
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
				{
					direction = -Nocubeless.Camera.Right;
					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(PlayerEntity.GetNextPosition(deltaTime * direction))))
					{
						direction = Vector3.Zero;
					}
				}

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
				{

					direction += Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(PlayerEntity.GetNextPosition(deltaTime * direction))))
					{
						direction -= Nocubeless.Camera.Front;
					}
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
				{
					direction -= Nocubeless.Camera.Front;

					if (!Nocubeless.Scene.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(PlayerEntity.GetNextPosition(deltaTime * direction))))
					{
						direction += Nocubeless.Camera.Front;
					}
				}

				if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
				{
					direction.Y += 1;
				}
				else if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
				{
					direction.Y -= 1;
				}

				//Nocubeless.CubeWorld.IsFreeSpace(ToCoordinates(PlayerEntity.Position + deltaTime * direction)
				PlayerEntity.Move(deltaTime * direction);
				Nocubeless.Camera.Position = PlayerEntity.Position;
			}

			base.Update(gameTime);
		}
	}
}
