﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	internal class EulerCamera
	{
		private float radiansFov;
		public float Fov {
			get => MathHelper.ToDegrees(radiansFov);
			set => radiansFov = MathHelper.ToRadians(value);
		}
		public float AspectRatio { get; set; }

		public Vector3 ScreenPosition { get; set; }
		public WorldCoordinates WorldPosition { get; set; }
		public Vector3 Front { get; set; }
		public Vector3 Up { get; private set; }
		public Vector3 Right { get; private set; }
		public float Sensitivity { get; set; }
		private float pitch = 0.0f;
		private float yaw = 0.0f;

		public float MinFov { get; set; }
		public float MaxFov { get; set; }
		private float defaultFov;

		public Vector3 Target {
			get {
				return ScreenPosition + Front;
			}
		}

		// TODO optimizing
		public Matrix ProjectionMatrix {
			get {
				const float zNear = 0.0005f, zFar = 100.0f;
				return Matrix.CreatePerspectiveFieldOfView(
					radiansFov,
					AspectRatio,
					zNear, zFar);
			}
		}

		public Matrix ViewMatrix {
			get {
				return Matrix.CreateLookAt(ScreenPosition, Target, Up);
			}
		}

		public Matrix WorldMatrix { get; set; } = Matrix.Identity;

		public EulerCamera(CameraSettings settings, Viewport viewport)
		{
			defaultFov = MathHelper.ToRadians(settings.DefaultFov);
			radiansFov = defaultFov;
			AspectRatio = viewport.AspectRatio;
			Sensitivity = settings.DefaultSensitivity;

			Reset();

			MinFov = 0.5f;
			MaxFov = 2.0f;
		}


		public void Move(Vector3 velocity)
		{
			WorldPosition += velocity;
		}

		public void Rotate(float pitch, float yaw) // In fact, I think these functions should not be directly in the Camera class (I mean with pitch and yaw) // BBMSG indeed you can move them
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

		public void RotateWorld(float pitch, float yaw, Vector3 around)
		{
			const float maxPitch = MathHelper.PiOver2 - 0.01f;
			this.pitch = MathHelper.Clamp(this.pitch - pitch * Sensitivity, -maxPitch, maxPitch);
			this.yaw -= yaw * Sensitivity;

			ScreenPosition = around + new Vector3(
				(float)(Math.Cos(this.pitch) * Math.Cos(this.yaw)),
				(float)Math.Sin(this.pitch),
				(float)(Math.Cos(this.pitch) * Math.Sin(this.yaw)));

			Front = around - ScreenPosition;
		}

		public void Zoom(float percentage)
		{
			radiansFov = MathHelper.Clamp(defaultFov / (percentage / 100.0f), MinFov, MaxFov);
		}

		public void Reset()
		{
			Front = Vector3.UnitZ;
			Up = Vector3.UnitY;
			Right = Vector3.Cross(Front, Up);
		}
	}
}
