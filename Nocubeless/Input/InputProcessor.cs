using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	abstract class InputProcessor
	{
		protected Nocubeless Nocubeless { get; set; }
		protected InputProcessor(Nocubeless nocubeless)
		{
			Nocubeless = nocubeless;
		}
		public abstract void Process();
	}
}
