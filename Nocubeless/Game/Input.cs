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

		public static bool WasJustReleased(Keys key)
		{
			return OldKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key);
		}
		public static bool WasJustPressed(Keys key)
		{
			return OldKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key);
		}

		public static bool WasRightMouseButtonJustPressed()
		{
			return CurrentMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released;
		}

		public static bool WasLeftMouseButtonJustPressed()
		{
			return CurrentMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released;
		}
		public static bool WasMiddleMouseButtonJustPressed()
		{
			return CurrentMouseState.MiddleButton == ButtonState.Pressed && OldMouseState.MiddleButton == ButtonState.Released;
		}
	}
}
