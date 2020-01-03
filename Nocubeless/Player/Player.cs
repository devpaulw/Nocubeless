using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class Player
	{
		public Vector3 Position { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
		public float Speed { get; set; }
		private float ActualSpeed { get; set; }
		public Vector3 Velocity { get; set; }

		public Player(Vector3 position, int width, int height, int length, float speed)
		{
			Position = position;
			Width = width;
			Height = height;
			Length = length;
			Speed = speed;
		}
		// TODO: utiliser un pointeur pour suivre le delta time
		public void UpdateSpeed(float deltaTime)
		{
			ActualSpeed = Speed * deltaTime;
		}

		public void Move(Vector3 velocity)
		{
			Position += ActualSpeed * velocity;
		}

		public Vector3 GetNextPosition(Vector3 velocity)
		{
			return Position + ActualSpeed * velocity;
		}
	}
}
