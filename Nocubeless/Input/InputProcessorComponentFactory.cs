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
