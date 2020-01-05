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
		private bool shouldLayCube = true;
		private Point windowCenter { get; set; }

		public PlayingInput(Nocubeless nocubeless) : base(nocubeless)
		{
			Nocubeless.Player.NextColorToLay = new CubeColor(7, 7, 7); // TODO: Manage
			windowCenter = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
		}

		public override void Initialize()
		{
			var colorPipette = new ColorPipette(Nocubeless);

			Game.Components.Add(colorPipette);

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				Vector2 movement = GetMouseMovement() / 57;
				Nocubeless.Camera.Rotate(movement.Y, -movement.X);

				ProcessKeyboardInput();
				ProcessMouseButtonsInput();
				Mouse.SetPosition(windowCenter.X, windowCenter.Y);
			}
		}
		private void ProcessMouseButtonsInput()
		{
			if (shouldLayCube)
			{
				CubeCoordinates cubeToPreviewPosition = Nocubeless.CubeWorld.GetTargetedNewCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
				Cube cubeToLay = new Cube(Nocubeless.Player.NextColorToLay, cubeToPreviewPosition);

				if (!AreColliding(Nocubeless.Player, cubeToLay))
				{
					Nocubeless.CubeWorld.PreviewCube(cubeToLay);

					if (Input.WasRightMouseButtonJustPressed())
					{
						Nocubeless.CubeWorld.LayCube(cubeToLay);
					}
				}
				else
				{
					Nocubeless.CubeWorld.PreviewCube(null);
				}
			}
			else
			{ // break
				Nocubeless.CubeWorld.PreviewCube(null);

				if (Input.WasLeftMouseButtonJustPressed())
				{
					CubeCoordinates cubeToBreakPosition = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
					Nocubeless.CubeWorld.BreakCube(cubeToBreakPosition);
				}
			}
		}

		private void ProcessKeyboardInput()
		{

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.SwitchLayBreak))
			{
				shouldLayCube = !shouldLayCube;
			}

			var direction = Vector3.Zero;

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.RunningSpeed;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.WalkingSpeed;
			}

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Speed * 1.005f;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Speed / 1.005f;
			}

			if (Input.WasJustPressed(Keys.V))
			{
				// BBMSG would Nocubeless.Camera.Settings.ZoomPercentage be better ? moreover we can directly pass the Camera.Default to the Camera() constructor 
				Nocubeless.Camera.Zoom(Nocubeless.Settings.Camera.ZoomPercentage);
				Nocubeless.Camera.MouseSensitivity = Nocubeless.Settings.Camera.MouseSensitivityWhenZooming;
			}
			else if (Input.WasJustReleased(Keys.V))
			{
				Nocubeless.Camera.Zoom(100);
				Nocubeless.Camera.MouseSensitivity = Nocubeless.Settings.Camera.DefaultMouseSensitivity;
			}


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

		private bool IsTargetingCubeIntersection(Vector3 direction)
		{
			// UGLY prevent the user from passing through two diagonal blocks
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

		// TODO move to another class
		public Vector2 GetMouseMovement()
		{
			return Nocubeless.Camera.MouseSensitivity
				* new Vector2(Input.CurrentMouseState.X - windowCenter.X, Input.CurrentMouseState.Y - windowCenter.Y);
		}

		// TODO move to another class
		private bool AreColliding(Player player, Cube cube)
		{
			const float cubeSize = 0.1f;
			var cubeMiddlePoint = new Vector3(cube.Coordinates.X + cubeSize / 2, cube.Coordinates.Y + cubeSize / 2, cube.Coordinates.Z + cubeSize / 2) / Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			var middlePoint = new Vector3(player.ScreenCoordinates.X + player.Width / 2, player.ScreenCoordinates.Y + player.Height / 2, player.ScreenCoordinates.Z + player.Length / 2);
			Vector3 gap = middlePoint - cubeMiddlePoint;
			gap.X = Math.Abs(gap.X);
			gap.Y = Math.Abs(gap.Y);
			gap.Z = Math.Abs(gap.Z);

			return gap.X <= (player.Width + cubeSize) / 2
				&& gap.Y <= (player.Height + cubeSize) / 2
				&& gap.Z <= (player.Length + cubeSize) / 2;
		}
	}
}
