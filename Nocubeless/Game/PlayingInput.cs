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
		private Point WindowCenter { get; set; }

		public PlayingInput(Nocubeless nocubeless) : base(nocubeless)
		{
			Nocubeless.Player.NextColorToLay = new CubeColor(7, 7, 7); // TODO: Manage
			WindowCenter = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
		}

		public override void Initialize()
		{
			var colorPipette = new ColorPipette(Nocubeless);

			Game.Components.Add(colorPipette);
			// SDNMSG: add playerMover, HeadRotater...
			// add cubeLayer, Breaker, Previewer
			// ...
			// why not a lot of small parts like that? Like the color pipette above
			// becoz I really don't like everything in the same class
			// If it's too disturbing, we can think about make private class many partial Playing Input classes, why not.

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				// TODO move this instruction to another class, it doesn't belong to input
				//Nocubeless.Player.UpdateSpeed((float)gameTime.ElapsedGameTime.TotalSeconds);

				Vector2 movement = GetMouseMovement() / 57;
				Nocubeless.Camera.Rotate(movement.Y, -movement.X);

				ProcessKeyboardInput();
				ProcessMouseButtonsInput();
				Mouse.SetPosition(WindowCenter.X, WindowCenter.Y);
			}
		}
		private void ProcessMouseButtonsInput()
		{
			if (shouldLayCube)
			{
				WorldCoordinates cubeToPreviewPosition = Nocubeless.CubeWorld.GetTargetedNewCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
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
					WorldCoordinates cubeToBreakPosition = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
					Nocubeless.CubeWorld.BreakCube(cubeToBreakPosition);
				}
			}
		}

		private void ProcessKeyboardInput()
		{
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
				direction += Nocubeless.Camera.Up;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
				{
					direction -= Nocubeless.Camera.Up;
				}
			}
			else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
			{
				direction -= Nocubeless.Camera.Up;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
				{
					direction += Nocubeless.Camera.Up;
				}
			}

			Nocubeless.Player.Move(direction);


			if (Input.WasJustPressed(Nocubeless.Settings.Keys.SwitchLayBreak))
			{
				shouldLayCube = !shouldLayCube;
			}
		} 

		public Vector2 GetMouseMovement()
		{
			return Nocubeless.Settings.Camera.MouseSensitivity
				* new Vector2(Input.CurrentMouseState.X - WindowCenter.X, Input.CurrentMouseState.Y - WindowCenter.Y);
		}

		public int GetScrollWheelMovement()
		{
			return Input.CurrentMouseState.ScrollWheelValue - Input.OldMouseState.ScrollWheelValue;
		}


		// TODO move to another class
		// "Good idea, Physics?"
		// BBMSG ANSWER probably later, when there will be other methods to put in it, i needed the cube ratio, that's why i left this method here for now
		private bool AreColliding(Player player, Cube cube)
		{
			const float cubeSize = 0.1f;
			var cubeMiddlePoint = new Vector3(cube.Coordinates.X + cubeSize / 2, cube.Coordinates.Y + cubeSize / 2, cube.Coordinates.Z + cubeSize / 2) / Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			var middlePoint = new Vector3(player.Position.X + player.Width / 2, player.Position.Y + player.Height / 2, player.Position.Z + player.Length / 2);
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
