using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	// CubeWorldSceneInput was merged with PlayingInput because it is the class that manage all the inputs of the class
	// SDNMSG ANSWER: Nice way!
	class PlayingInput : NocubelessComponent
	{
		private CubeColor cubeToLayColor;
		private bool shouldLayCube = true;
		private Point WindowCenter { get; set; }

		public PlayingInput(Nocubeless nocubeless) : base(nocubeless)
		{
			cubeToLayColor = new CubeColor(7, 7, 7);
			WindowCenter = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				// TODO move this instruction to another class, it doesn't belong to input
				Nocubeless.Player.UpdateSpeed((float)gameTime.ElapsedGameTime.TotalSeconds);

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
				Cube cubeToLay = new Cube(cubeToLayColor, cubeToPreviewPosition);

				if (!Nocubeless.Player.IsColliding(cubeToLay, Nocubeless.CubeWorld.GetGraphicsCubeRatio())) // CHEAT
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

			Pipette();

			void Pipette()
			{
				if (Input.WasMiddleMouseButtonJustPressed())
				{
					// TODO: Put in  separated another class
					var targetCubeCoordinates = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
					var targetCubeColor = Nocubeless.CubeWorld.GetCubeColorAt(targetCubeCoordinates);
					OnColorPicking(this, new ColorPickingEventArgs() { Color = targetCubeColor });
				}
			}
		}

		private void ProcessKeyboardInput() // SDNMSG: Run and Up/Down is too fast (do you forgot using gameTime?)
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
				direction.Y += Nocubeless.CubeWorld.GetGraphicsCubeRatio();

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
				{
					direction.Y -= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
				}
			}
			else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
			{
				direction.Y -= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Player.GetNextPosition(direction))))
				{
					direction.Y += Nocubeless.CubeWorld.GetGraphicsCubeRatio();
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

		public void OnColorPicking(object sender, ColorPickingEventArgs e)
		{
			cubeToLayColor = e.Color;
		}
	}
}
