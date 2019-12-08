using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless.WorldStructures
{
	interface WorldStructure
	{
		List<Cube> Generate();
	}
}
