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

		public Vector3 Position { get; set; }
		public Vector3 Front { get; private set; }
		public Vector3 Up { get; private set; }
		public Vector3 Right { get; private set; }

		public float Speed { get; set; } = 1.0f;

		private float Pitch = 0.0f;
		private float Yaw = 0.0f;

		public float minFov { get; set; }
		public float maxFov { get; set; }
		private float defaultFov;

		public Vector3 Target
		{
			get
			{
				return Position + Front;
			}
		}

		public Camera(CameraSettings settings, Viewport viewport)
		{
			defaultFov = MathHelper.ToRadians(settings.Fov);
			fovInRadians = defaultFov;
			AspectRatio = viewport.AspectRatio;

			Front = Vector3.UnitZ;
			Up = Vector3.UnitY;
			Right = Vector3.Cross(Front, Up);

			minFov = 0.5f;
			maxFov = 2.0f;
		}

		// TODO optimizing
		public Matrix ProjectionMatrix
		{
			get
			{
				const float zNear = 0.0005f, zFar = 100.0f;
				return Matrix.CreatePerspectiveFieldOfView(
					fovInRadians,
					AspectRatio,
					zNear, zFar);
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
			Right = Vector3.Normalize(Vector3.Cross(Front, Up));
		}

		public void Zoom(float percentage)
		{
			fovInRadians = MathHelper.Clamp(defaultFov / (percentage / 100.0f), minFov, maxFov);
		}
	}
}
