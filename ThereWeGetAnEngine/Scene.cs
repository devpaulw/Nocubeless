using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThereWeGetAnEngine
{
    public class Nocubeless : Game
    {
        GraphicsDeviceManager graphics;

        Camera camera;
        CubeRenderer cube;

        public Nocubeless()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            camera = new Camera();
            cube = new CubeRenderer(graphics.GraphicsDevice);
            //Content.RootDirectory = "";
            DrawableGameComponent
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            cube.Draw(camera, Color.Gold, Content.Load<Effect>("Content/CubeEffect"));

            base.Draw(gameTime);
        }
    }
}
