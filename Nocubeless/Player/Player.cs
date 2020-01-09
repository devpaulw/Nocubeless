using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldPosition = Microsoft.Xna.Framework.Vector3; // SDNMSG: An alias can be useful sometimes when we don't want name convention, it might be useful (why creating a new class WorldPosition when Vector3 is better)

namespace Nocubeless
{
	class Player : DynamicEntity
	{
		public CubeCoordinates WorldPosition { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
		public float Length { get; set; }
		public PlayerSettings Settings { get; set; }
		public CubeColor NextColorToLay { get; set; }

		public Player(PlayerSettings settings, CubeCoordinates position) : base(position.ToVector3())
		{
			//Position = position.ToVector3();
			WorldPosition = position;
			Width = settings.Width;
			Height = settings.Height;
			Length = settings.Length;
			Speed = settings.WalkingSpeed;
			Settings = settings;
		}
	}
}
