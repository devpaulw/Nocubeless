using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	// the class that decides which input processor to call
	class NocubelessInputProcessor : NocubelessComponent
	{
		InputProcessorComponent explorationGamemodeInput;
		public NocubelessInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			explorationGamemodeInput = InputProcessorComponent.ExplorationGamemode(Nocubeless);
		}

		public override void Update(GameTime gameTime)
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.ShowColorPicker))
			{
				if (Nocubeless.CurrentState == NocubelessState.Playing)
				{
					Nocubeless.CurrentState = NocubelessState.ColorPicking;
				}
				else
				{
					Nocubeless.CurrentState = NocubelessState.Playing;
				}
			}

			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				explorationGamemodeInput.Update(gameTime);
			}
		}
	}
}
