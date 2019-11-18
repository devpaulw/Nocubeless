using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Nocubeless
{
    class NocubelessApp : Game
    {
        GraphicsDeviceManager graphics;

        Camera camera;
        CubeRenderer cubeRenderer;

        public NocubelessApp()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;

            camera = new Camera();

            Content.RootDirectory = "MGContent";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            CubeEffect cubeEffect = new CubeEffect(Content.Load<Effect>("CubeEffect"));
            cubeRenderer = new CubeRenderer(GraphicsDevice, cubeEffect, 0.1F);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            camera.MoveFromKeyboard(keyboard, gameTime.ElapsedGameTime.);
            camera.RotateFromMouse(mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 2, 128));

            cubeRenderer.Draw(camera, Color.Gold);

            base.Draw(gameTime);
        }
    }
}
