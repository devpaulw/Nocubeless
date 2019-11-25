using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class Scene : DrawableGameComponent
    {
        public CubeEffect Effect { get; }
        public Camera Camera { get; set; }
        public World World { get; set; }

        public Scene(IGameApp gameApp) : base(gameApp.Instance)
        {
            Effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect"));
            Camera = new Camera(gameApp.Settings.Camera, Game.GraphicsDevice.Viewport);
            World = new World(gameApp, Effect);
        }

        public override void Initialize()
        {
            Game.Components.Add(World);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            Effect.View = Camera.ViewMatrix;
            Effect.Projection = Camera.ProjectionMatrix;

            base.Draw(gameTime);
        }
    }
}
