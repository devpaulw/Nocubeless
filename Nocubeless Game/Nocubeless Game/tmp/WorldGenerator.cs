using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nocubeless.WorldStructures;

namespace Nocubeless
{
	class WorldGenerator
	{
		private static List<Cube> cubes = new List<Cube>();

		internal static List<Cube> GenerateArea(int size)
		{
			// Generate floor
			for (int z = 0; z < size; z++)
			{
				for (int x = 0; x < size; x++)
				{
					cubes.Add(new Cube(Color.Green, new CubeCoordinate(x, 0, z)));
				}
			}

			// Generate hills
			HillStructure h = new HillStructure(new GenerationArea(new CubeCoordinate(0, 1, 0), 20, 20, 20));
			cubes.AddRange(h.Generate());

			return cubes;
		}
	}
}
