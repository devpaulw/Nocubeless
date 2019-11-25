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
            {
                World.LayCube(new Cube(
                    new Color(0f, 1f, 0f, 1f),
                    new CubeCoordinate(-1, -1, -1)));
                World.LayCube(new Cube(
                    new Color(1f, 0.2f, 0f, 1f),
                    new CubeCoordinate(0, 0, 0)));
                World.LayCube(new Cube(
                    new Color(0f, 0.2f, 1f, 1f),
                    new CubeCoordinate(2, 0, 0)));
                World.LayCube(new Cube(
                    new Color(0f, 1f, 1f, 1f),
                    new CubeCoordinate(-3, 2, 1)));
                World.LayCube(new Cube(
                    new Color(1f, 1f, 0f, 1f),
                    new CubeCoordinate(2, 3, 4)));
            }
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
