using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class NocubelessInputProcessorSelector : NocubelessComponent // SDNMSG: I renamed it Selector
	{
		private readonly InputProcessorComponent playingInput;
		private readonly InputProcessorComponent editingInput;

		public NocubelessInputProcessorSelector(Nocubeless nocubeless) : base(nocubeless)
		{
			playingInput = InputProcessorComponentFactory.PlayingMode(Nocubeless);
			editingInput = InputProcessorComponentFactory.EditingMode(Nocubeless);
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

		void SwitchStates() // TODO: Make events and enable when game starts
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.ShowColorPicker))
			{
				if (Nocubeless.CurrentState == NocubelessState.Editing)
					Nocubeless.SetState(NocubelessState.ColorPicking);
				else if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
					Nocubeless.SetState(NocubelessState.Editing, NocubelessState.ColorPicking);
					
			}

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.ToggleMode))
			{
				if (Nocubeless.CurrentState == NocubelessState.Playing)
					Nocubeless.SetState(NocubelessState.Editing);			
				else if (Nocubeless.CurrentState == NocubelessState.Editing)
					Nocubeless.SetState(NocubelessState.Playing);
			}
		}
	}
}