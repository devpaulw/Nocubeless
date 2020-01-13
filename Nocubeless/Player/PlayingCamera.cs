using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	internal class PlayingCamera : Camera
	{
		public WorldCoordinates WorldPosition { get; set; }
		public override Vector3 Up { get; protected set; }
		public override Vector3 Front { get; protected set; }
		public override Vector3 Right { get; protected set; }
		public float Sensitivity { get; set; } // SDNMSG: Is that clever? It's rather the InputProcessor or the Camera directly that should know the Sensitivity (think)? There is my example in my Editing Camera
		private float pitch = 0.0f;
		private float yaw = 0.0f;

		public float MinFov { get; set; }
		public float MaxFov { get; set; }
		private float defaultFov;

		public override Vector3 Target {
			get {
				return ScreenPosition + Front;
			}
			protected set {
				Front = value - ScreenPosition;
			}
		}

		protected override float ZNear => 0.0005f;
		protected override float ZFar => 100.0f;
		

		public PlayingCamera(CameraSettings settings, Viewport viewport) : base(settings.DefaultFov, viewport)
		{
			defaultFov = MathHelper.ToRadians(settings.DefaultFov);
			Sensitivity = settings.DefaultSensitivity;

			Front = Vector3.UnitZ;
			Right = Vector3.Cross(Front, Up);

			MinFov = 0.5f;
			MaxFov = 2.0f;
		}


		public void Move(Vector3 velocity)
		{
			WorldPosition += velocity;
		}

		public void Rotate(float pitch, float yaw)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			this.pitch = MathHelper.Clamp(this.pitch - pitch * Sensitivity, -maxPitch, maxPitch);
			this.yaw -= yaw * Sensitivity;

			Front = new Vector3(
				(float)(Math.Cos(this.pitch) * Math.Cos(this.yaw)),
				(float)Math.Sin(this.pitch),
				(float)(Math.Cos(this.pitch) * Math.Sin(this.yaw)));

			Right = Vector3.Normalize(Vector3.Cross(Front, Up));
		}

		public void Zoom(float percentage)
		{
			radiansFov = MathHelper.Clamp(defaultFov / (percentage / 100.0f), MinFov, MaxFov);
		}
	}
}
