using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    sealed class NocubelessInput
    {
        public KeyboardState CurrentKeyboardState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public KeyboardState OldKeyboardState { get; private set; }
        public MouseState OldMouseState { get; private set; }

        public NocubelessInput() { }

        public void ReloadCurrentStates()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        public void ReloadOldStates()
        {
            OldKeyboardState = CurrentKeyboardState;
            OldMouseState = CurrentMouseState;
        }
    }
}
