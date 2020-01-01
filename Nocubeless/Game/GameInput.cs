using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    static class GameInput
    {
        public static KeyboardState CurrentKeyboardState { get; private set; }
        public static MouseState CurrentMouseState { get; private set; }

        public static KeyboardState OldKeyboardState { get; private set; }
        public static MouseState OldMouseState { get; private set; }

        //public GameInput() { }

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
    }
}
