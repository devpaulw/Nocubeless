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
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private RGBTextBoxes rgbTextBoxes;
        private PickerCube pickerCube;

        public ColorPickerMenu(Nocubeless nocubeless) : base(nocubeless)
        {
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

            rgbTextBoxes = new RGBTextBoxes(Nocubeless);

            pickerCube = new PickerCube(Nocubeless, 0.175f);
            pickerCube.RotateY(45.0f);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(Nocubeless.CurrentState);
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                rgbTextBoxes.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                #region Background
                //Nocubeless.SpriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
                #endregion

                rgbTextBoxes.Draw(gameTime);

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
    }
}