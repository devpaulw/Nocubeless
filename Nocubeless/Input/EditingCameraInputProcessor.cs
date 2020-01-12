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
		public EditingCameraInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}


		public override void Process()
		{
			if (Input.CurrentMouseState.MiddleButton == ButtonState.Pressed)
			{
				const float cameraRotationRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.MiddlePoint.Y,
					deltaX = Input.MiddlePoint.X - Input.CurrentMouseState.X;

				((EditingCamera)Nocubeless.Camera).RotateAround(
					-cameraRotationRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity,
					cameraRotationRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity,
					Vector3.Zero);
			}

			if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
			{
				const float cameraRotationRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.MiddlePoint.Y,
					deltaX = Input.MiddlePoint.X - Input.CurrentMouseState.X;

				((EditingCamera)Nocubeless.Camera).Move(
					cameraRotationRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity,
					-cameraRotationRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity);
			}
			
			Input.SetMouseInTheMiddle();
		}
	}
}
