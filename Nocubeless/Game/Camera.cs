using Microsoft.Xna.Framework;
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
		public float AspectRatio { get; set; }
		public Vector3 ScreenPosition { get; set; }
		public Vector3 Up { get; set; }
		public abstract Vector3 Target { get; set; }

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

		protected abstract float ZNear { get; }
		protected abstract float ZFar { get; }

		protected float radiansFov;
	}
}
