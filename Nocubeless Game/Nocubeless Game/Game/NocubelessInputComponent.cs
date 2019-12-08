using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    abstract class NocubelessInputComponent : NocubelessComponent
    {
        protected Point MiddlePoint { get; }

        protected KeyboardState CurrentKeyboardState { get; set; }
        protected MouseState CurrentMouseState { get; set; }

        protected KeyboardState OldKeyboardState { get; private set; }
        protected MouseState OldMouseState { get; private set; }

        public NocubelessInputComponent(Nocubeless nocubeless) : base(nocubeless)
        {
            MiddlePoint = new Point(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            OldKeyboardState = CurrentKeyboardState;
            OldMouseState = CurrentMouseState;

            base.Update(gameTime);
        }

        protected void ReloadCurrentStates()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        protected void SetMouseInTheMiddle()
        {
            Mouse.SetPosition(MiddlePoint.X, MiddlePoint.Y);
        }
    }
}
