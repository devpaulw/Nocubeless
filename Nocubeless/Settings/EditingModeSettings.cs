using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class EditingModeSettings
    {
		public float WorldRotationDistance { get; set; }
		public float MoveSpeed { get; set; }
		public float ScrollSpeed { get; set; }

		public static EditingModeSettings Default {
			get {
				return new EditingModeSettings
				{
					WorldRotationDistance = 2.0f,
					MoveSpeed = 1.5f,
					ScrollSpeed = 2.0f
				};
			}
		}
	}
}
