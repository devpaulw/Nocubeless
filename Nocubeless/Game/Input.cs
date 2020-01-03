using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class Input : NocubelessComponent
	{

		public static KeyboardState CurrentKeyboardState { get; private set; }
		public static MouseState CurrentMouseState { get; private set; }

		public static KeyboardState OldKeyboardState { get; private set; }
		public static MouseState OldMouseState { get; private set; }

		public Input(Nocubeless nocubeless) : base(nocubeless)
		{

		}

		public override void Update(GameTime gameTime)
		{
			Mouse.SetPosition(Nocubeless.Window.Center.X, Nocubeless.Window.Center.Y);
		}

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

		#region Mouse input
		public Vector2 GetMouseMovement()
		{
			return Nocubeless.Settings.Camera.MouseSensitivity
				* new Vector2(CurrentMouseState.X - Nocubeless.Window.Center.X, CurrentMouseState.Y - Nocubeless.Window.Center.Y);
		}

		public int GetScrollWheelMovement()
		{
			return CurrentMouseState.ScrollWheelValue - OldMouseState.ScrollWheelValue;
		}
		#endregion

		#region Keyboard input

		public static bool IsReleased(Keys key)
		{
			return OldKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key);
		}
		public static bool IsPressed(Keys key)
		{
			return OldKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key);
		}
		#endregion


	}
}
