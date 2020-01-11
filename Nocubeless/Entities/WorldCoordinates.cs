using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class WorldCoordinates : CubeCoordinates
	{
		public Vector3 PositionOnCurrentCube { get; set; }

		public WorldCoordinates(CubeCoordinates coordinates, Vector3 positionOnCube)
			: base(coordinates.X, coordinates.Y, coordinates.Z)
		{
			PositionOnCurrentCube = positionOnCube;
		}

		//public static WorldCoordinates Multiply(WorldCoordinates coordinates, float scalar)
		//{
		//	Contract.Requires(coordinates != null);
		//	var newPosition = scalar * GetPositionOnCurrentCube(coordinates.PositionOnCurrentCube);

		//	return new WorldCoordinates(
		//		new CubeCoordinates(
		//			coordinates.X + coordinates.X,
		//			coordinates.Y + coordinates.Y,
		//			coordinates.Z + coordinates.Z),
		//		scalar * GetPositionOnCurrentCube(coordinates.PositionOnCurrentCube));
		//}


		public static WorldCoordinates operator +(WorldCoordinates coordinates, Vector3 positionOnCurrentCube)
		{
			return Add(coordinates, positionOnCurrentCube);
		}

		public static WorldCoordinates operator +(Vector3 positionOnCurrentCube, WorldCoordinates coordinates)
		{
			return Add(coordinates, positionOnCurrentCube);
		}

		public static WorldCoordinates Add(WorldCoordinates coordinates, Vector3 vector3)
		{
			Contract.Requires(coordinates != null);
			Vector3 movement = coordinates.PositionOnCurrentCube + vector3;

			return new WorldCoordinates(
				new CubeCoordinates(
					coordinates.X + (int)movement.X - GetNegativeRemainder(movement.X),
					coordinates.Y + (int)movement.Y - GetNegativeRemainder(movement.Y),
					coordinates.Z + (int)movement.Z - GetNegativeRemainder(movement.Z)),
				new Vector3(
					Math.Abs(1 + movement.X) % 1,
					Math.Abs(1 + movement.Y) % 1,
					Math.Abs(1 + movement.Z) % 1));
		}

		private static int GetNegativeRemainder(float f)
		{
			if (f < 0 && f % 1 != 0)
				return 1;
			return 0;
		}

		private static Vector3 GetPositionOnCurrentCube(Vector3 vector3)
		{
			// get only the decimal part of the vector3
			return new Vector3(
				vector3.X % 1,
				vector3.Y % 1,
				vector3.Z % 1);
		}

		public override string ToString()
		{
			return base.ToString() + "\n(" + PositionOnCurrentCube.X +" ; " + PositionOnCurrentCube.Y + " ; " + PositionOnCurrentCube.Z + ")";
		}
	}
}
