using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class CameraInputProcessor : NocubelessComponent, IInputProcessor
	{
		private Point windowCenter;

		public CameraInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			windowCenter = new Point(Nocubeless.GraphicsDevice.Viewport.Width / 2, Nocubeless.GraphicsDevice.Viewport.Height / 2);
		}

		public override void Update(GameTime gameTime)
		{
			const float cameraRotationRatio = 1f / 57f;
			Nocubeless.Camera.Rotate(cameraRotationRatio * (Input.CurrentMouseState.Y - windowCenter.Y), cameraRotationRatio * (windowCenter.X - Input.CurrentMouseState.X));
			Mouse.SetPosition(windowCenter.X, windowCenter.Y);

			if (Input.WasJustPressed(Keys.V))
			{
				// BBMSG would Nocubeless.Camera.Settings.ZoomPercentage be better ? moreover we can directly pass the Camera.Default to the Camera() constructor (like i did with Player)
				Nocubeless.Camera.Zoom(Nocubeless.Settings.Camera.ZoomPercentage);
				Nocubeless.Camera.Sensitivity = Nocubeless.Settings.Camera.SensitivityWhenZooming;
			}
			else if (Input.WasJustReleased(Keys.V))
			{
				Nocubeless.Camera.Zoom(100);
				Nocubeless.Camera.Sensitivity = Nocubeless.Settings.Camera.DefaultSensitivity;
			}
		}
	}
}
