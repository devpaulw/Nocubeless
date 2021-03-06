﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	class Player : DynamicEntity
	{
		public float Width { get; set; }
		public float Height { get; set; }
		public float Length { get; set; }
		public PlayerSettings Settings { get; set; }
		public CubeColor NextColorToLay { get; set; }

		public Player(PlayerSettings settings, WorldCoordinates position) : base(position)
		{
			Width = settings.Width;
			Height = settings.Height;
			Length = settings.Length;
			Settings = settings;
		}
	}
}
