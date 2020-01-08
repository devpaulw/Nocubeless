using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayingProcessorComponent : InputProcessor
	{
		protected List<InputProcessor> inputProcessors;

		public PlayingProcessorComponent(Nocubeless nocubeless) : base(nocubeless)
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

		public static PlayingProcessorComponent ExplorationGamemode(Nocubeless nocubeless) // SDNMSG: Why a singleton?, there is already the constructor, not true? Have you checked the Singleton usability?
		{ // SDNMSG: Or otherwise, we could make ONLY these kind of function and use one class instead of many xProcessorComponent class (because for example PlayingProcessorComponent looks like EditingCameraInputProcessor, the functions are the same and I don't see cases when it could be useful)
			var explorationGamemode = new PlayingProcessorComponent(nocubeless);
			explorationGamemode.Add(new PlayingCameraInputProcessor(nocubeless));
			explorationGamemode.Add(new PlayerEntityInputProcessor(nocubeless));
			explorationGamemode.Add(new PlayingCubeInteractionsInputProcessor(nocubeless));
			return explorationGamemode;
		}
	}
}
