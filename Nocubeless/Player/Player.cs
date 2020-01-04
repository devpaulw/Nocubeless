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
		public Coordinates TruncatedPosition {
			get; set;
		}
		public Vector3 Position { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
		public float Speed { get; set; }
		private float ActualSpeed { get; set; }
		private Camera Camera { get; set; }

		public Player(Coordinates position, int width, int height, int length, float speed, Camera camera) // BBMSG: "A player can know the CubeWorld" => i don't agree, it's another class that should manage the player and the cube and their interactions
		{
			Position = position.ToVector3();
			TruncatedPosition = Coordinates.FromTruncated(Position);
			Width = width;
			Height = height;
			Length = length;
			Speed = speed;
			Camera = camera;
		}
		public void UpdateSpeed(float deltaTime)
		{
			ActualSpeed = Speed * deltaTime;
		}

		public void Move(Vector3 direction)
		{
			Camera.Position += ActualSpeed * direction;
			Position = Camera.Position;
			TruncatedPosition = Coordinates.FromTruncated(Position);
		}

		public Vector3 GetNextPosition(Vector3 direction)
		{
			return ActualSpeed * direction + Position;
		}
	}
}
