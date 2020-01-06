using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	internal class CameraSettings
	{
		public float DefaultFov { get; set; }
		public float MoveSpeed { get; set; }
		public float DefaultSensitivity { get; set; }
		public int ZoomPercentage { get; set; }
		public float SensitivityWhenZooming { get; set; }

		public static CameraSettings Default
		{
			get
			{
				return new CameraSettings
				{
					DefaultFov = 100,
					ZoomPercentage = 200,
					// BBMSG renamed to Sensitivity instead of MouseSensitivity because it's not really related to Mouse
					DefaultSensitivity = 0.175f,
					SensitivityWhenZooming = 0.075f
				};
			}
		}

	}
}
