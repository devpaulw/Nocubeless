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
			Nocubeless.Camera.Rotate(cameraRotationRatio * (Input.CurrentMouseState.Y - Input.MiddlePoint.Y), cameraRotationRatio * (Input.MiddlePoint.X - Input.CurrentMouseState.X));
			Input.SetMouseInTheMiddle();

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Zoom))
			{
				// would Nocubeless.Camera.Settings.ZoomPercentage be better ? moreover we can directly pass the Camera.Default to the Camera() constructor (like i did with Player)
				// Nope, we need to keep camera settings because it's our settings pattern, CubeWorld is the only class that contains its settings because there could be many worlds later, but there are not many cameras
				// So should i put PlayerSettings in Nocubeless.Settings ?
				// SDNMSG: Camera, in Nocubeless, and Player I think rather in Player.Settings because there will be online players someday
				Nocubeless.Camera.Zoom(Nocubeless.Settings.Camera.ZoomPercentage);
				Nocubeless.Camera.Sensitivity = Nocubeless.Settings.Camera.SensitivityWhenZooming;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Zoom))
			{
				Nocubeless.Camera.Zoom(100);
				Nocubeless.Camera.Sensitivity = Nocubeless.Settings.Camera.DefaultSensitivity;
			}
		}
	}
}
