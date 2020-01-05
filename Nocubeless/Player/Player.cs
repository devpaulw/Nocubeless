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
		// WARNING: THE TruncatedPosition DOES NOT CONTAINS THE POSITION
		public WorldCoordinates TruncatedPosition {
			get; set;
		}
		public Vector3 Position { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
		public float Length { get; set; }
		public float Speed { get; set; }
		private float speed { get; set; } // SDNMSG: we rarely do a get set private statement, make it a normal variable, however a Getter-Setter is never in lower-case ^^'; PS: separate your private and public statements.
		public Camera Camera { get; set; }
		public CubeColor NextColorToLay { get; set; } // SDNMSG: I need you advises to know whether it's a good idea to put that there
		

		public Player(WorldCoordinates position, int width, int height, int length /*SDNMSG: REMOVE These unused parameters*/, float speed, Camera camera) // i don't agree, it's another class that should manage the player and the cube and their interactions // SDN ANSWER: You're right
		{
			Position = position.ToVector3();
			TruncatedPosition = WorldCoordinates.FromTruncated(Position);
			// Dimensions specified in the constructor aren't take into account
			//Width = width;
			//Height = height;
			//Length = length;
			Width = 0.1f;
			Height = 0.1f;
			Length = 0.1f;
			Speed = speed;
			Camera = camera;
		}
		public void UpdateSpeed(float deltaTime)
		{
			speed = Speed * deltaTime;
		}

		// TODO move to another class ? // SDNMSG: Good idea, Physics?
		public bool IsColliding(Cube cube, float ratio) // CHEAT
		{
			const float cubeSize = 0.1f;
			var cubeMiddlePoint = new Vector3(cube.Coordinates.X + cubeSize / 2, cube.Coordinates.Y + cubeSize / 2, cube.Coordinates.Z + cubeSize / 2) / ratio;
			var middlePoint = new Vector3(Position.X + Width / 2, Position.Y + Height / 2, Position.Z + Length / 2);
			Vector3 gap = middlePoint - cubeMiddlePoint;
			gap.X = Math.Abs(gap.X);
			gap.Y = Math.Abs(gap.Y);
			gap.Z = Math.Abs(gap.Z);

			return gap.X <= (Width + cubeSize) / 2
				&& gap.Y <= (Height + cubeSize) / 2
				&& gap.Z <= (Length + cubeSize) / 2;
		}

		public void Move(Vector3 direction)
		{
			Camera.Position += speed * direction;
			Position = Camera.Position;
			TruncatedPosition = WorldCoordinates.FromTruncated(Position);
		}

		public Vector3 GetNextPosition(Vector3 direction)
		{
			return speed * direction + Position;
		}
	}
}
