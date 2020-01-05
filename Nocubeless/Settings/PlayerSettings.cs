using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerSettings
	{
		public float Width { get; set; }
		public float Height { get; set; }
		public float Length { get; set; }
		public float WalkingSpeed { get; set; }
		public float RunningSpeed { get; set; }
		public float FlyingSpeed { get; set; }

		public static PlayerSettings Default
		{
			get
			{
				return new PlayerSettings
				{
					Width = 0.1f,
					Height = 0.1f,
					Length = 0.1f,
					WalkingSpeed = 2.5f,
					RunningSpeed = 5.0f
				};
			}
		}
	}
}
