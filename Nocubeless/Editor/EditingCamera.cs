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
		public override Vector3 Up { get; protected set; }
		public override Vector3 Front { get; protected set; }
		public override Vector3 Right {
			get => Vector3.Normalize(Vector3.Cross(Front, Up));
			protected set => throw new NotImplementedException();
		}
		public override Vector3 Target {
			get {
				return ScreenPosition + Front;
			}
			protected set {
				Front = value - ScreenPosition;
			}
		}
		protected override float ZNear => 0.0005f; // temp, waiting shader update
		protected override float ZFar => 1000.0f;

		private float pitch = 0.0f;
		private float yaw = 0.0f;

		public EditingCamera(CameraSettings settings, Viewport viewport) : 
			base(settings.DefaultFov, viewport)
		{
			RotateAround(0.0f, 0.0f, Target);
		}
		
		public void Move(float xPosition = 0.0f, float yPosition = 0.0f, float zPosition = 0.0f)
		{
			ScreenPosition += Right * xPosition;
			ScreenPosition += Up * yPosition;
			ScreenPosition += Front * zPosition;
		}

		public void RotateAround(float pitch, float yaw, Vector3 around)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			this.pitch = MathHelper.Clamp(this.pitch + pitch, -maxPitch, maxPitch);
			this.yaw += yaw;

			Matrix rotation = Matrix.CreateRotationX(this.pitch) *
				Matrix.CreateRotationY(this.yaw);

			Vector3 originalFront = -Vector3.UnitZ,
				originalUp = Vector3.UnitY;

			ScreenPosition = Target + Vector3.Transform(originalFront, rotation);
			Front = around - ScreenPosition;
			Up = Vector3.Transform(originalUp, rotation);
		}
	}
}
