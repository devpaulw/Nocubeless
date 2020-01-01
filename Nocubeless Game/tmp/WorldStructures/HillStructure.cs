using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless.WorldStructures
{
	class HillStructure : WorldStructure
	{
		private GenerationArea area;

		public HillStructure(GenerationArea area)
		{
			this.area = area;
		}

		public List<Cube> Generate()
		{
			const float SpawnFactor = 0.6f;
			const float SpawnFactorCubesUp = 0.6f;
			List<Cube> cubes = new List<Cube>();

			int maxX = area.Width + area.X;
			int maxZ = area.Length + area.Z;

			Random r = new Random();
			Vector2 center = area.GetCenter();
			float max = center.Length();
			List<List<Cube>> cubesUp = new List<List<Cube>>();

			// Pop up a cube according to the probability to be pop up for each locations
			for (int z = area.Z; z < maxZ; z++)
			{
				for (int x = area.X; x < maxX; x++)
				{
					var t = new Vector2(x, z) - center;
					float p = (1 - (t.Length() / max)) * SpawnFactor;
					if (r.NextDouble() < p)
					{
						cubes.Add(new Cube(Color.Blue, new CubeCoordinate(x, area.Y, z)));
						int y = 0;
						while (r.NextDouble() < p * SpawnFactorCubesUp)
						{
							if (y >= cubesUp.Count)
							{
								cubesUp.Add(new List<Cube>());
							}
							 
							y++;
							cubesUp.ElementAt(y - 1).Add(new Cube(Color.Yellow, new CubeCoordinate(x, area.Y + y, z)));
							p *= SpawnFactorCubesUp;
						}
					}
				}
			}

			// Pop up cubes in locations that have at least 3 neighbors
			for (int z = area.Z; z < maxZ; z++)
			{
				for (int x = area.X; x < maxX; x++)
				{
					int countNeighbours = 0;
					int idxCurrentCube = cubes.FindIndex(cube => cube.Position.X == x && cube.Position.Z == z && cube.Position.Y == area.Y);
					for (int i = 0; i <= cubes.Count; i++)
					{
						if (/*idxCurrentCube + */i >= 0 && /*idxCurrentCube + */i < cubes.Count)
						{
							Cube cube = cubes.ElementAt(/*idxCurrentCube + */i);
							if (cube.Position.X == x - 1 && cube.Position.Z == z
								|| cube.Position.X == x && cube.Position.Z == z - 1
								|| cube.Position.X == x + 1 && cube.Position.Z == z
								|| cube.Position.X == x && cube.Position.Z == z + 1)
							{
								countNeighbours++;
							}
						}
					}

					if (countNeighbours >= 3)
					{
						cubes.Add(new Cube(Color.Purple, new CubeCoordinate(x, area.Y, z)));
					}
				}
			}

			for (int i = 0; i < cubesUp.Count; i++)
			{
				cubes.AddRange(cubesUp.ElementAt(i));
			}
			return cubes;
		}
	}
}
