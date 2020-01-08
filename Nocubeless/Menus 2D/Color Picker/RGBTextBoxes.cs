using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class RGBTextBoxes : NocubelessDrawableComponent
    {
        private TextBox textBoxR, textBoxG, textBoxB;
        //private GuiSystem guiSystem;

        public ViewportAdapter ViewportAdapter { get; private set; }

        public RGBTextBoxes(Nocubeless nocubeless) : base(nocubeless)
        {
            LoadContent();

            var textBoxFont = Game.Content.Load<SpriteFont>(@"Menus/Color Picker/InputTextBoxFont"); // DESIGN: not in the right place

            const int maxLength = 1;
            int spacing = 20;
            int width = 45, height = 45;
            var basePosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 3 * width + 2 * spacing,
                Game.GraphicsDevice.Viewport.Height / 2);
            var backColor = Color.White;
            var borderColor = Color.Red;
            int borderSize = 5;
            var fontColor = Color.Black;

            var textBoxRPosition = new Vector2(basePosition.X + 0 * width + 0 * spacing,
                basePosition.Y);
            textBoxR = new TextBox(Nocubeless,
                width, height,
                textBoxRPosition,
                backColor,
                borderColor, borderSize,
                fontColor, textBoxFont,
                false, maxLength);

            var textBoxGPosition = new Vector2(basePosition.X + 1 * width + 1 * spacing,
                basePosition.Y);
            textBoxG = new TextBox(Nocubeless,
                width, height,
                textBoxGPosition,
                backColor,
                borderColor, borderSize,
                fontColor, textBoxFont,
                false, maxLength);

            var textBoxBPosition = new Vector2(basePosition.X + 2 * width + 2 * spacing,
                basePosition.Y);
            textBoxB = new TextBox(Nocubeless,
                width, height,
                textBoxBPosition,
                backColor,
                borderColor, borderSize,
                fontColor, textBoxFont,
                false, maxLength);
        }

        protected override void LoadContent()
        {
            //ViewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
            //var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);
            //var font = Game.Content.Load<BitmapFont>("Menus/Color Picker/Sensation");
            //BitmapFont.UseKernings = false;
            //Skin.CreateDefault(font);

            //var demoScreen = new Screen
            //{
            //    Content = new StackPanel
            //    {
            //        Margin = 5,
            //        Orientation = Orientation.Vertical,
            //        Items =
            //        {
            //        new Label("Buttons") { Margin = 5 },
            //        new Label("TextBox") { Margin = 5 },
            //        new MonoGame.Extended.Gui.Controls.TextBox { Text = "TextBox" },
            //        }
            //    }
            //};

            //guiSystem = new GuiSystem(ViewportAdapter, guiRenderer) { ActiveScreen = demoScreen };

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //guiSystem.Update(gameTime);

            var provider = CultureInfo.CurrentCulture;

            // sync 
            textBoxR.Text = Nocubeless.Player.NextColorToLay.Red.ToString(provider);
            textBoxG.Text = Nocubeless.Player.NextColorToLay.Green.ToString(provider);
            textBoxB.Text = Nocubeless.Player.NextColorToLay.Blue.ToString(provider);

            #region Text Boxes
            textBoxR.Update(gameTime);
            textBoxG.Update(gameTime);
            textBoxB.Update(gameTime);

            try // textBox Picker Event
            {
                int r = Convert.ToInt32(textBoxR.Text.ToString(provider), provider),
                g = Convert.ToInt32(textBoxG.Text.ToString(provider), provider),
                b = Convert.ToInt32(textBoxB.Text.ToString(provider), provider);

                var newColor = new CubeColor(r, g, b);

                Nocubeless.Player.NextColorToLay = newColor;
            }
            catch (FormatException) { } // conversion error (e.g. is not a number)
            #endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //guiSystem.Draw(gameTime);

            textBoxR.Draw(gameTime);
            textBoxG.Draw(gameTime);
            textBoxB.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
