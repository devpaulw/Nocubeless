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
		public Vector3 Position { get; set; }
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
			Position = position;
		}
		public void Update(float deltaTime)
		{
			this.deltaTime = deltaTime;
			screenSpeed = worldSpeed * deltaTime;
		}

		public void Move(Vector3 direction)
		{
			Position += screenSpeed * direction;
		}
	}
}
