﻿using Microsoft.Xna.Framework;
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
					Nocubeless.CurrentState = NocubelessState.ColorPicking;
				else if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
					Nocubeless.CurrentState = NocubelessState.Editing;
					
			}

			if (Input.WasJustPressed(Nocubeless.Settings.Keys.SwitchMode))
			{
				if (Nocubeless.CurrentState == NocubelessState.Playing)
				{
					Nocubeless.CurrentState = NocubelessState.Editing;
					Nocubeless.Camera = new EditingCamera(Nocubeless.Settings.Camera, Game.GraphicsDevice.Viewport);
					Nocubeless.CubeWorld.PreviewableCube.Coordinates =
						Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.ScreenPosition);
				}
				else if (Nocubeless.CurrentState == NocubelessState.Editing)
				{
					Nocubeless.CurrentState = NocubelessState.Playing;
					Nocubeless.Camera = new PlayingCamera(Nocubeless.Settings.Camera, Game.GraphicsDevice.Viewport);
					Input.SetMouseInTheMiddle(); // important! Do not ignore
				}
			}
		}
	}
}