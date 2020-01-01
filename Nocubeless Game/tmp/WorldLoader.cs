using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class WorldLoader
	{
		public WorldLoader()
		{
			
		}

		public static List<Cube> LoadArea(int size)
		{
			// Generate as the map is not stored yet
			return WorldGenerator.GenerateArea(size);
		}
	}
}
