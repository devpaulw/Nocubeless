using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayingCameraInputProcessor : InputProcessor
	{
		public PlayingCameraInputProcessor(Nocubeless nocubeless) : base(nocubeless) { }

		public override void Process()
		{
			const float cameraRotationRatio = 1f / 57f;
			((PlayingCamera)Nocubeless.Camera).Rotate(cameraRotationRatio * (Input.CurrentMouseState.Y - Input.MiddlePoint.Y), cameraRotationRatio * (Input.MiddlePoint.X - Input.CurrentMouseState.X));
			Input.SetMouseInTheMiddle();

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Zoom))
			{
				((PlayingCamera)Nocubeless.Camera).Zoom(Nocubeless.Settings.Camera.ZoomPercentage);
				((PlayingCamera)Nocubeless.Camera).Sensitivity = Nocubeless.Settings.Camera.SensitivityWhenZooming;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Zoom))
			{
				((PlayingCamera)Nocubeless.Camera).Zoom(100);
				((PlayingCamera)Nocubeless.Camera).Sensitivity = Nocubeless.Settings.Camera.DefaultSensitivity;
			}
		}
	}
}
