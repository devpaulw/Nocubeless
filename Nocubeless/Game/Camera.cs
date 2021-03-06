﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	abstract class Camera
	{
		public float Fov {
			get => MathHelper.ToDegrees(radiansFov);
			set => radiansFov = MathHelper.ToRadians(value);
		}
		public float AspectRatio { get; }
		public Vector3 ScreenPosition { get; set; }
		public abstract Vector3 Up { get; protected set; }
		public abstract Vector3 Front { get; protected set; }
		public abstract Vector3 Right { get; protected set; }
		public abstract Vector3 Target { get; protected set; }

		protected abstract float ZNear { get; }
		protected abstract float ZFar { get; }

		protected float radiansFov;

		public Camera(float fov, Viewport viewport)
		{
			Fov = fov;
			AspectRatio = viewport.AspectRatio;
			Up = Vector3.UnitY;
		}

		public Matrix GetView()
		{
			return Matrix.CreateLookAt(ScreenPosition, Target, Up);
		}
		// TODO optimizing
		public Matrix GetProjection()
		{
			return Matrix.CreatePerspectiveFieldOfView(
				radiansFov,
				AspectRatio,
				ZNear, ZFar);
		}
	}
}
