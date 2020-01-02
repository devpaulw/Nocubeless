using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerEntity
	{
		public Vector3 Position { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
		public float Speed { get; set; }
		public Vector3 Velocity { get; set; }

		public PlayerEntity(Vector3 position, int width, int height, int length, float speed)
		{
			Position = position;
			Width = width;
			Height = height;
			Length = length;
			Speed = speed;
		}

		public void Move(Vector3 velocity)
		{
			Position += Speed * velocity;
		}

		public Vector3 GetNextPosition(Vector3 velocity)
		{
			return Position + Speed * velocity;
		}
	}
}
