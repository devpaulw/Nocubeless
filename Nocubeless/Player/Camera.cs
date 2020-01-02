using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Nocubeless
{
	internal class Camera
	{
		private float fovInRadians;
		public float Fov
		{
			get => MathHelper.ToDegrees(fovInRadians);
			set => fovInRadians = MathHelper.ToRadians(value);
		}
		public float AspectRatio { get; set; }

		public Vector3 Position {
			get; set;
		}
		//public Vector3[] HookedPosition = new Vector3[1];
		public Vector3 Front { get; private set; }
		public Vector3 Up { get; private set; }
		public Vector3 Right { get; private set; }

		public float Speed { get; set; } = 1.0f;

		private float Pitch = 0.0f;
		private float Yaw = 0.0f;
		public Vector3 Target
		{
			get
			{
				return Position + Front;
			}
		}

		//public void HookTo(ref Vector3 position)
		//{
		//	HookedPosition[0] = position;
		//}

		public Camera(CameraSettings settings, Viewport viewport)
		{
			Fov = settings.Fov;
			AspectRatio = viewport.AspectRatio;

			//Position = Vector3.Zero;
			Front = Vector3.UnitZ;
			Up = Vector3.UnitY;
		}

		// TODO optimizing
		public Matrix ProjectionMatrix
		{
			get
			{
				const float nearPlane = 0.0005f, farPlane = 100.0f;
				return Matrix.CreatePerspectiveFieldOfView(
					fovInRadians,
					AspectRatio,
					nearPlane, farPlane);
			}
		}

		public Matrix ViewMatrix
		{
			get
			{
				return Matrix.CreateLookAt(Position, Target, Up);
			}
		}

		public void Rotate(float pitchInRadians, float yawInRadians)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			Pitch = MathHelper.Clamp(Pitch - pitchInRadians, -maxPitch, maxPitch);
			Yaw -= yawInRadians;

			Front = new Vector3(
				(float)(Math.Cos(Pitch) * Math.Cos(Yaw)),
				(float)Math.Sin(Pitch),
				(float)(Math.Cos(Pitch) * Math.Sin(Yaw)));
			Front.Normalize();

			Right = Vector3.Cross(Front, Up);
		}
	}
}
