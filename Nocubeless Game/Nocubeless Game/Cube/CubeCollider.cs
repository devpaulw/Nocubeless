using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless.Cube
{
	static class CubeCollider
	{
		public static bool IsColliding(this Cube cube1, Cube cube2)
		{
			return cube1.Position.X > cube2.Position.X && cube1.Position.X < cube1.Position.X + cube1.Position.W
				&& cube1.Position.Y > cube2.Position.Y && cube1.Position.Y < cube2.PositionY + cube2.Position.Height
				&& cube1.Position.Z > cube2.Position.Z && cube1.Position.Z < cube2.Position.Z + cube2.Position.Height;
		}
	}
}
