using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class EditingCameraInputProcessor : InputProcessor
	{
		bool isRotating = false;
		Vector3 rotateAround = Vector3.Zero;

		public EditingCameraInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}

		public override void Process()
		{
			Nocubeless.CubeWorld.PreviewableCube.Coordinates = Nocubeless.CubeWorld.GetTargetedCube(
							Nocubeless.Camera as EditingCamera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);

			if (Input.WasMiddleMouseButtonJustPressed())
			{
				rotateAround = Nocubeless.CubeWorld.GetGraphicsCubePosition(
						Nocubeless.CubeWorld.GetTargetedCube(
							Nocubeless.Camera as EditingCamera, Nocubeless.Settings.CubeHandler.MaxLayingDistance));

				Console.WriteLine(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(rotateAround));
				Console.WriteLine(Nocubeless.Camera.Target);
				Console.WriteLine(Nocubeless.Camera.ScreenPosition);

				isRotating = true;
			}

			if (Input.CurrentMouseState.MiddleButton == ButtonState.Released)
			{
				isRotating = false;
			}

			if (isRotating)
			{
				const float cameraRotationRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.OldMouseState.Y,
					deltaX = Input.OldMouseState.X - Input.CurrentMouseState.X;

				((EditingCamera)Nocubeless.Camera).RotateAround(
					-cameraRotationRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity,
					cameraRotationRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity,
					rotateAround);
			}
			

			if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
			{
				const float cameraRotationRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.OldMouseState.Y,
					deltaX = Input.OldMouseState.X - Input.CurrentMouseState.X;

				(Nocubeless.Camera as EditingCamera).Move(
					cameraRotationRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity,
					-cameraRotationRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity);
			}
			
			//Input.SetMouseInTheMiddle();
		}
	}
}
