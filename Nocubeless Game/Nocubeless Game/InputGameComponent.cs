using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal abstract class InputGameComponent : GameComponent
    {
        protected KeyboardState CurrentKeyboardState { get; set; }
        protected MouseState CurrentMouseState { get; set; }

        protected KeyboardState OldKeyboardState { get; private set; }
        protected MouseState OldMouseState { get; private set; }

        public InputKeySettings KeySettings { get; set; }

        public InputGameComponent(Game game, InputKeySettings keySettings) : base(game)
        {
            KeySettings = keySettings;
        }

        public override void Update(GameTime gameTime)
        {
            OldKeyboardState = CurrentKeyboardState;
            OldMouseState = CurrentMouseState;
            base.Update(gameTime);
        }
    }
}
