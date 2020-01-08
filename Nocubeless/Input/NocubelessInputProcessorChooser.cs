using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	// the class that decides which input processor to call
	class NocubelessInputProcessorChooser : NocubelessComponent
	{
		private readonly PlayingProcessorComponent playingInput;
		private readonly EditingProcessorComponent editingInput;

		public NocubelessInputProcessorChooser(Nocubeless nocubeless) : base(nocubeless)
		{
			playingInput = PlayingProcessorComponent.ExplorationGamemode(Nocubeless);
			editingInput = new EditingProcessorComponent(Nocubeless);
		}

		public override void Update(GameTime gameTime)
		{
			ProcessModes();
			SwitchStates();
		}

		void ProcessModes()
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
				playingInput.Process();
			else if (Nocubeless.CurrentState == NocubelessState.Editing)
				editingInput.Process();
		}

		void SwitchStates()
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.ShowColorPicker))
			{
				if (Nocubeless.CurrentState == NocubelessState.Editing)
					Nocubeless.CurrentState = NocubelessState.ColorPicking;
				else if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
					Nocubeless.CurrentState = NocubelessState.Editing;
			}

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.SwitchMode))
			{
				if (Nocubeless.CurrentState == NocubelessState.Playing)
				{
					Nocubeless.Camera.Reset();
					Nocubeless.CurrentState = NocubelessState.Editing;
				}
				else if (Nocubeless.CurrentState == NocubelessState.Editing)
				{
					Nocubeless.CurrentState = NocubelessState.Playing;
					Input.SetMouseInTheMiddle(); // important! Do not ignore
				}
			}
		}
	}
}
