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
		public float Speed
		{
			get => worldSpeed;
			set
			{
				worldSpeed = value;
				screenSpeed = worldSpeed * deltaTime;
			}
		}
		private float worldSpeed;
		private float deltaTime = 0;
		protected float screenSpeed; // TODO rename

		public DynamicEntity(Vector3 position)
		{
			ScreenCoordinates = position;
		}
		public void Update(float deltaTime)
		{
			this.deltaTime = deltaTime;
			screenSpeed = worldSpeed * deltaTime;
		}

		public void Move(Vector3 direction)
		{
			ScreenCoordinates += screenSpeed * direction;
		}

		public Vector3 GetNextGraphicalPosition(Vector3 direction)
		{
			return screenSpeed * direction + ScreenCoordinates;
		}
	}
}
