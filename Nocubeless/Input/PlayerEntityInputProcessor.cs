using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerEntityInputProcessor : NocubelessComponent, IInputProcessor
	{
		public PlayerEntityInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.RunningSpeed;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.WalkingSpeed;
			}

			var direction = Vector3.Zero;
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
			{
				direction = Nocubeless.Camera.Right;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction = Vector3.Zero;
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
			{
				direction = -Nocubeless.Camera.Right;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction = Vector3.Zero;
				}
			}

			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
			{
				direction += Nocubeless.Camera.Front;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction -= Nocubeless.Camera.Front;
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
			{
				direction -= Nocubeless.Camera.Front;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction += Nocubeless.Camera.Front;
				}
			}

			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
			{
				direction += Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction -= Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
			{
				direction -= Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(direction))))
				{
					direction += Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;
				}
			}

			if (!IsTargetingCubeIntersection(direction))
			{
				Nocubeless.Player.Move(direction);
				Nocubeless.Camera.Position = Nocubeless.Player.ScreenCoordinates;
			}
		}

		// TMP move to another place
		private bool IsTargetingCubeIntersection(Vector3 direction)
		{
			// prevent the user from passing through two diagonal blocks
			// horizontal verification
			Vector3 directionTmp = direction;
			directionTmp.X = 0;

			if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(directionTmp))))
			{
				directionTmp.X = direction.X;
				directionTmp.Z = 0;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(directionTmp))))
				{
					return true;
				}
			}

			// vertical verification
			directionTmp.X = direction.X;
			directionTmp.Y = 0;
			directionTmp.Z = direction.Z;

			if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(directionTmp))))
			{
				directionTmp.X = 0;
				directionTmp.Y = direction.Y;
				directionTmp.Z = 0;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextGraphicalPosition(directionTmp))))
				{
					return true;
				}
			}

			return false;
		}
	}
}
