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
		private Vector3 _up;
		private Vector3 _front;

		public override Vector3 Up {
			get {
				return _up;// / WorldRotationDistance;
			}
			protected set {
				_up = value;
			}
		}
		public override Vector3 Front {
			get {
				return _front;// / WorldRotationDistance;
			}
			protected set {
				_front = value;
			}
		}
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
		public float WorldRotationDistance { get; set; }

		protected override float ZNear => 0.0005f; // temp, waiting shader update
		protected override float ZFar => 1000.0f;

		private float pitch = 0.0f;
		private float yaw = 0.0f;

		public EditingCamera(CameraSettings settings, Viewport viewport, float worldRotationDistance) : 
			base(settings.DefaultFov, viewport)
		{
			WorldRotationDistance = worldRotationDistance;
			RotateAround(0.0f, 0.0f, Target);
		}
		
		public void Move(float xVelocity = 0.0f, float yVelocity = 0.0f)
		{
			ScreenPosition += Right * xVelocity;
			ScreenPosition += Up * yVelocity;
			
		}

		public void Zoom(float velocity)
		{
			ScreenPosition += Front * velocity;
		}

		public void RotateAround(float pitch, float yaw, Vector3 around)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			this.pitch = MathHelper.Clamp(this.pitch + pitch, -maxPitch, maxPitch);
			this.yaw += yaw;

			Matrix rotation = Matrix.CreateRotationX(this.pitch) *
				Matrix.CreateRotationY(this.yaw) *
				WorldRotationDistance;

			Vector3 originalFront = -Vector3.UnitZ,
				originalUp = Vector3.UnitY;

			ScreenPosition = Target + Vector3.Transform(originalFront, rotation);
			Front = around - ScreenPosition;
			Up = Vector3.Transform(originalUp, rotation);
		}
	}
}
