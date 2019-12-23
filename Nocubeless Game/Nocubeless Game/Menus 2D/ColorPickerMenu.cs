using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ColorPickerMenu : NocubelessDrawableComponent
    {
        #region Events
        public event ColorPickingEventHandler OnColorPicking;
        #endregion

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private TextBox textBoxR, textBoxG, textBoxB;
        private PickerCube pickerCube;

        public ColorPickerMenu(Nocubeless nocubeless, ColorPickingEventHandler onColorPicking) : base(nocubeless)
        {
            OnColorPicking += onColorPicking;
        }

        protected override void LoadContent()
        {
            #region Background Set-Up
            var backgroundWidth = Game.GraphicsDevice.Viewport.Width;
            var backgroundHeight = Game.GraphicsDevice.Viewport.Height;
            var backgroundColor = new Color(Color.Black, 100);

            backgroundPosition = Vector2.Zero;

            // background texture instancing
            backgroundTexture = new Texture2D(GraphicsDevice,
                   backgroundWidth, backgroundHeight);

            // color to background texture setting up
            Color[] colorData = new Color[backgroundWidth * backgroundHeight];
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = backgroundColor;
            }
            backgroundTexture.SetData(colorData);
            #endregion

            SetupRGBTextBoxes(out textBoxR, out textBoxG, out textBoxB);

            pickerCube = new PickerCube(Nocubeless, 0.175f);
            pickerCube.RotateY(45.0f);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            #region Show Color Picker Switch
            if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.ShowColorPicker)
                && GameInput.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.ShowColorPicker))
            {
                if (Nocubeless.CurrentState == NocubelessState.Playing)
                    Nocubeless.CurrentState = NocubelessState.ColorPicking;
                else
                {
                    Nocubeless.CurrentState = NocubelessState.Playing;
                    Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
                }
            }
            #endregion

            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                #region Text Boxes
                textBoxR.Update(gameTime);
                textBoxG.Update(gameTime);
                textBoxB.Update(gameTime);

                try // textBox Picker Event
                {
                    int r = Convert.ToInt32(textBoxR.Text.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture),
                    g = Convert.ToInt32(textBoxG.Text.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture),
                    b = Convert.ToInt32(textBoxB.Text.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);

                    CubeColor newColor = new CubeColor(r, g, b);

                    OnColorPicking(this, new ColorPickingEventArgs() { Color = newColor });
                }
                catch (FormatException) { } // conversion error
                #endregion
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                #region Background
                Nocubeless.SpriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
                #endregion

                #region Text Boxes
                textBoxR.Draw(gameTime);
                textBoxG.Draw(gameTime);
                textBoxB.Draw(gameTime);
                #endregion

                #region Picker Cube // TEMP
                
                pickerCube.RotateY(50.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                pickerCube.RotateX(50.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                //pickerCube.RotateZ(50.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                pickerCube.Draw(new Vector2(0.0f, 0.0f));
                #endregion

                #region TEMP, c'est pour le style
#pragma warning disable CA1303 // Do not pass literals as localized parameters 
                Nocubeless.SpriteBatch.DrawString(Game.Content.Load<SpriteFont>(@"Menus/Color Picker/InputTextBoxFont"),
                    "Color Picker V1\n Click on the Text Boxes to set-up your RGB Color\n  (r,g,b)E[0, 7] please!",
                    new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2 + 100), 
                    Color.Gold);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                #endregion
            }

            base.Draw(gameTime);
        }

        private void SetupRGBTextBoxes(out TextBox textBoxR, out TextBox textBoxG, out TextBox textBoxB) // DESIGN: Should be seperated into a RGB Text Boxes class
        {
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
    }
}