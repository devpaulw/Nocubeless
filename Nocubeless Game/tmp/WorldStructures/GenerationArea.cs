using Microsoft.Xna.Framework;
using System;

namespace Nocubeless.WorldStructures
{
	public class GenerationArea
	{
		private CubeCoordinate coordinates;
		private int width;
		private int height;
		private int length;

		public int X { get => coordinates.X; }
		public int Y { get => coordinates.Y; }
		public int Z { get => coordinates.Z; }
		public int Width { get => width; }
		public int Length { get => length; }

		internal GenerationArea(CubeCoordinate coordinates, int width, int height, int length)
		{
			this.coordinates = coordinates;
			this.width = width;
			this.height = height;
			this.length = length;
		}


		public Vector2 GetCenter()
		{
			return new Vector2((width + X) / 2, (length + Z) / 2);
		}

	}
}