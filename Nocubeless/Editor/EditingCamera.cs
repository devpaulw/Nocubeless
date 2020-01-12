using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class EditingCamera : Camera
	{
		public override Vector3 Target { get; set; }
		protected override float ZNear => 0.0005f; // temp, waiting shader update
		protected override float ZFar => 1000.0f;

		private float pitch = 0.0f;
		private float yaw = 0.0f;

		public EditingCamera(CameraSettings settings, Viewport viewport) : 
			base(settings.DefaultFov, viewport)
		{
			RotateAround(0.0f, 0.0f, Target);
		}
		
		public void Move(float xPosition, float yPosition)
		{
			Vector3 direction = new Vector3(xPosition, yPosition, 0);
			ScreenPosition += direction;
			Target += direction;
		}

		public void RotateAround(float pitch, float yaw, Vector3 around)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			this.pitch = MathHelper.Clamp(this.pitch - pitch, -maxPitch, maxPitch);
			this.yaw -= yaw;

			ScreenPosition = new Vector3(
				(float)(Math.Cos(this.pitch) * Math.Cos(this.yaw)),
				(float)Math.Sin(this.pitch),
				(float)(Math.Cos(this.pitch) * Math.Sin(this.yaw)));

			Target = around;
		}
	}
}
