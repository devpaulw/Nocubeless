using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	static class InputProcessorComponentFactory
	{
		public static InputProcessorComponent PlayingMode(Nocubeless nocubeless)
		{
			// Or otherwise, we could make ONLY these kind of function and use one class instead of many xProcessorComponent class (because for example PlayingProcessorComponent looks like EditingCameraInputProcessor, the functions are the same and I don't see cases when it could be useful)
			// BBMSG indeed it's better !
			var playingMode = new InputProcessorComponent(nocubeless);
			playingMode.Add(new PlayingCameraInputProcessor(nocubeless));
			playingMode.Add(new PlayerEntityInputProcessor(nocubeless));
			playingMode.Add(new PlayingCubeInteractionsInputProcessor(nocubeless));
			return playingMode;
		}
		public static InputProcessorComponent EditingMode(Nocubeless nocubeless)
		{
			var editingMode = new InputProcessorComponent(nocubeless);
			editingMode.Add(new EditingCameraInputProcessor(nocubeless));
			editingMode.Add(new EditingCubeCursorInputProcessor(nocubeless));
			return editingMode;
		}


	}
}
