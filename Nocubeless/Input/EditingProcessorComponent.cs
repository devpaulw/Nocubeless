using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class EditingProcessorComponent : InputProcessor
	{
		protected List<InputProcessor> inputProcessors;

		public EditingProcessorComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			inputProcessors = new List<InputProcessor>();

			Add(new EditingCameraInputProcessor(Nocubeless));
		}

		public void Add(InputProcessor inputProcessor)
		{
			inputProcessors.Add(inputProcessor);
		}

		public override void Process()
		{
			foreach (var inputProcessor in inputProcessors)
			{
				inputProcessor.Process();
			}
		}
	}
}
