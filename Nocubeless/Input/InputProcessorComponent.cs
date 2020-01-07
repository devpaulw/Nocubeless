using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class InputProcessorComponent : NocubelessComponent, IInputProcessor
	{
		protected List<IInputProcessor> inputProcessors;

		public InputProcessorComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			inputProcessors = new List<IInputProcessor>();
		}

		public void Add(IInputProcessor inputProcessor)
		{
			inputProcessors.Add(inputProcessor);
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var inputProcessor in inputProcessors)
			{
				inputProcessor.Update(gameTime);
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
