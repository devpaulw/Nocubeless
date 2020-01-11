using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	abstract class DynamicEntity
	{
		public Vector3 ScreenCoordinates { get; set; }
		public Vector3 Exp_WordCoordinates { get; set; }
		public WorldCoordinates WorldCoordinates { get; set; }
		public float Speed
		{
			get => worldSpeed;
			set
			{
				worldSpeed = value;
				ScreenSpeed = worldSpeed * deltaTime;
			}
		}
		private float worldSpeed;
		private float deltaTime = 0;
		public float ScreenSpeed { get; set; } // TODO make private

		public DynamicEntity(WorldCoordinates coordinates)
		{
			WorldCoordinates = coordinates;
			Exp_WordCoordinates = new Vector3(coordinates.X, coordinates.Y, coordinates.Z);
			//ScreenCoordinates = position;
		}
		public float ratio { get; set; }// TMP

		public void Update(float deltaTime)
		{
			this.deltaTime = deltaTime;
			ScreenSpeed = worldSpeed * deltaTime;
		}

		public void Move(Vector3 direction)
		{
			WorldCoordinates += ScreenSpeed * direction * ratio;
			//Console.WriteLine(WorldCoordinates);
			Exp_WordCoordinates += ScreenSpeed * direction * ratio;
			//Console.WriteLine(WorldCoordinates);
		}

		public WorldCoordinates GetWorldPositionTowards(Vector3 direction)
		{
			return ScreenSpeed * direction * ratio + WorldCoordinates;
		}
		public Vector3 GetNextGraphicalPosition(Vector3 direction)
		{
			return ScreenSpeed * direction * ratio + ScreenCoordinates;
		}
	}
}
