using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	static class Input
	{

		public static KeyboardState CurrentKeyboardState { get; private set; }
		public static MouseState CurrentMouseState { get; private set; }

		public static KeyboardState OldKeyboardState { get; private set; }
		public static MouseState OldMouseState { get; private set; }

		public static void ReloadCurrentStates()
		{
			CurrentKeyboardState = Keyboard.GetState();
			CurrentMouseState = Mouse.GetState();
		}

		public static void ReloadOldStates()
		{
			OldKeyboardState = CurrentKeyboardState;
			OldMouseState = CurrentMouseState;
		}

		public static bool IsReleased(Keys key) // SDNMSG: "WasJustReleased" is a better name
		{
			return OldKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key);
		}
		public static bool IsPressed(Keys key) // SDNMSG: "WasJustPressed" is a better name
		{
			return OldKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key);
		}
	}
}
