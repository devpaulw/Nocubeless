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
        private SpriteBatch spriteBatch;
        private Texture2D menuBaseTexture;
        private Rectangle menuBaseDestination;

        public event ColorPickingEventHandler OnColorPicking;

        public ColorPickerMenu(Nocubeless nocubeless, ColorPickingEventHandler onColorPicking) : base(nocubeless)
        {
            OnColorPicking += onColorPicking;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuBaseTexture = Game.Content.Load<Texture2D>(@"Menus/Color Picker/Base");
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
            if (Nocubeless.Input.CurrentKeyboardState.IsKeyDown(Keys.L)
                && Nocubeless.Input.OldKeyboardState.IsKeyUp(Keys.L))
            {
                OnColorPicking(this, new ColorPickingEventArgs() { CubeColor = Color.Gold });
            }
            // end test zone
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, 
                    GraphicsDevice.BlendState,
                    null,
                    GraphicsDevice.DepthStencilState,
                    GraphicsDevice.RasterizerState);

                spriteBatch.Draw(menuBaseTexture, menuBaseDestination, Color.Black);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }

    delegate void ColorPickingEventHandler(object sender, ColorPickingEventArgs e);
}
