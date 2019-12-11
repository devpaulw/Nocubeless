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

        private readonly CubeDrawer cubeDrawer;

        public event ColorPickingEventHandler OnColorPicking;

        public ColorPickerMenu(Nocubeless nocubeless, ColorPickingEventHandler onColorPicking) : base(nocubeless)
        {
            OnColorPicking += onColorPicking;

            cubeDrawer = new CubeDrawer(Nocubeless, 0.1f);

            test3dpicker();
        }

#pragma warning disable IDE1006 // Naming Styles
        void test3dpicker()
#pragma warning restore IDE1006 // Naming Styles
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int z = 0; z < 8; z++)
                    {
                        CubeWorldCoordinates newPosition = new CubeWorldCoordinates(x, y, z);
                        CubeColor newColor = new CubeColor(x, y, z);
                        Cube newCube = new Cube(newColor.ToVector3(), newPosition);
                        Nocubeless.CubeWorld.LayCube(newCube);
                    }
                }
            }
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

            #region temp test zone
            if (Nocubeless.Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.ShowColorPicker)
                && Nocubeless.Input.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.ShowColorPicker))
            {
                Random random = new Random();
                float r = random.Next(0, 8) / 7.0f,
                    g = random.Next(0, 8) / 7.0f,
                    b = random.Next(0, 8) / 7.0f;

                Vector3 newColor = new Vector3(r, g, b);

                OnColorPicking(this, new ColorPickingEventArgs() { Color = newColor });
            }
            #endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.ColorPicking)
            {
                EffectMatrices effectMatrices = new EffectMatrices(Nocubeless.Camera.ProjectionMatrix, Matrix.Identity, Matrix.Identity);

                Nocubeless.SpriteBatch.Draw(menuBaseTexture, menuBaseDestination, Color.Black);

                cubeDrawer.Draw(Vector3.Zero, Color.Gold.ToVector3(), effectMatrices);


                // coming soon message temp
                //                Nocubeless.SpriteBatch.DrawString(menuFont,
                //#pragma warning disable CA1303 // Do not pass literals as localized parameters
                //                    "Color Picker Menu\nComing soon... \n\nIn the meantime, \na random color have been\ngenerated.\n\n...Press C again.",
                //#pragma warning restore CA1303 // Do not pass literals as localized parameters
                //                    new Vector2(menuBaseDestination.Left, menuBaseDestination.Top), 
                //                    Color.LightBlue);
            }

            base.Draw(gameTime);
        }
    }

    delegate void ColorPickingEventHandler(object sender, ColorPickingEventArgs e);
}
