using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class Player : DynamicEntity
	{
		// WARNING: THE TruncatedPosition DOES NOT CONTAINS THE POSITION
		public WorldCoordinates TruncatedPosition {
			get; set;
		}
		public float Width { get; set; }
		public float Height { get; set; }
		public float Length { get; set; }
		public PlayerSettings Settings { get; set; }
		public Camera Camera { get; set; }

		public Player(PlayerSettings settings, WorldCoordinates position, Camera camera) : base(position.ToVector3())
		{
			//Position = position.ToVector3();
			TruncatedPosition = WorldCoordinates.FromTruncated(Position);
			Width = settings.Width;
			Height = settings.Height;
			Length = settings.Length;
			Speed = settings.WalkingSpeed;
			Settings = settings;
			Camera = camera;
		}

		// TMP maybe i will use a virtual Move(), the problem would be that Player would be the only class to override it
		public new void Move(Vector3 direction)
		{
			base.Move(direction);
			// TODO move this instruction to another place
			Camera.Position = Position;
		}

		public Vector3 GetNextPosition(Vector3 direction)
		{
			return screenSpeed * direction + Position;
		}
	}
}
