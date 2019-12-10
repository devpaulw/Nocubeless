using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ColorPickerMenu : NocubelessDrawableComponent
    {
        private Texture2D menuBaseTexture;
        private SpriteFont menuFont;
        private Rectangle menuBaseDestination;

        public event ColorPickingEventHandler OnColorPicking;

        public ColorPickerMenu(Nocubeless nocubeless, ColorPickingEventHandler onColorPicking) : base(nocubeless)
        {
            OnColorPicking += onColorPicking;
        }

        protected override void LoadContent()
        {
            menuBaseTexture = Game.Content.Load<Texture2D>(@"Menus/Color Picker/Base");
            menuFont = Game.Content.Load<SpriteFont>(@"Menus/Font");
            menuBaseDestination = new Rectangle(Game.GraphicsDevice.Viewport.Width / 2 - menuBaseTexture.Width / 2, 
                Game.GraphicsDevice.Viewport.Height / 2 - menuBaseTexture.Height / 2, 
                menuBaseTexture.Width, menuBaseTexture.Height);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Nocubeless.Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.ShowColorPicker)
                && Nocubeless.Input.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.ShowColorPicker))
            {
                if (Nocubeless.CurrentState == NocubelessState.Playing)
                    Nocubeless.CurrentState = NocubelessState.ColorPicking;
                else
                {
                    Nocubeless.CurrentState = NocubelessState.Playing;
                    Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
                }
            }

            // temp test zone
            if (Nocubeless.Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.ShowColorPicker)
                && Nocubeless.Input.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.ShowColorPicker))
            {
                Random random = new Random();
                int r = (int)Math.Round(random.Next(0, 256) / 17.0f) * 17, 
                    g = (int)Math.Round(random.Next(0, 256) / 17.0f) * 17, 
                    b = (int)Math.Round(random.Next(0, 256) / 17.0f) * 17;

                Color newColor = new Color(r, g, b);

                OnColorPicking(this, new ColorPickingEventArgs() { CubeColor = newColor });
            }
            // end test zone
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                Nocubeless.SpriteBatch.Draw(menuBaseTexture, menuBaseDestination, Color.Black);
                // comming soon message temp

                Nocubeless.SpriteBatch.DrawString(menuFont,
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    "Color Picker Menu\nComming soon... \n\nIn the meantime, \na random color have been\ngenerated.\n\n...Press C again.",
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                    new Vector2(menuBaseDestination.Left, menuBaseDestination.Top), 
                    Color.LightBlue);
            }

            base.Draw(gameTime);
        }
    }

    delegate void ColorPickingEventHandler(object sender, ColorPickingEventArgs e);
}
