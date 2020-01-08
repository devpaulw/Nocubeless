using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class InputProcessorComponent : InputProcessor
	{
		protected List<InputProcessor> inputProcessors;

		public InputProcessorComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			inputProcessors = new List<InputProcessor>();
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

		public static InputProcessorComponent ExplorationGamemode(Nocubeless nocubeless)
		{
			var explorationGamemode = new InputProcessorComponent(nocubeless);
			explorationGamemode.Add(new CameraInputProcessor(nocubeless));
			explorationGamemode.Add(new PlayerEntityInputProcessor(nocubeless));
			explorationGamemode.Add(new CubeInteractionsInputProcessor(nocubeless));
			return explorationGamemode;
		}
	}
}
