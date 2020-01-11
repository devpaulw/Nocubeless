using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	static class WorldCoordinatesHelper
	{
		public static CubeCoordinates ToCubeCoordinates(this WorldCoordinates coordinates)
		{
			return new CubeCoordinates(
				(int)Math.Round(coordinates.X),
				(int)Math.Round(coordinates.Y),
				(int)Math.Round(coordinates.Z));
		}
	}
}
