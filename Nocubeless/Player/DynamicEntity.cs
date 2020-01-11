using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	abstract class DynamicEntity
	{
		public WorldCoordinates WorldPosition { get; set; }
		//public float Speed
		//{
		//	get => worldSpeed;
		//	set
		//	{
		//		worldSpeed = value;
		//		screenSpeed = worldSpeed * deltaTime;
		//	}
		//}
		public float Speed
		{
			get => worldSpeed;
			set {
				worldSpeed = value;
				UpdateSpeed();
			}
		}
		private float worldSpeed;
		private float actualSpeed;

		private float deltaTime = 0;
		public float ratio { get; set; } // TMP CHEAT

		public DynamicEntity(WorldCoordinates position)
		{
			WorldPosition = position;
		}

		public void Update(float deltaTime)
		{
			this.deltaTime = deltaTime;
			UpdateSpeed();
		}

		private void UpdateSpeed()
		{
			actualSpeed = worldSpeed * deltaTime;
		}

		public void Move(Vector3 direction)
		{
			WorldPosition += actualSpeed * direction;
		}

		public WorldCoordinates GetNextWorldPositionTowards(Vector3 direction)
		{
			return actualSpeed * direction + WorldPosition;
		}
	}
}
