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
        public GameSettings GameSettings { get; set; }
        public CubeEffect Effect { get; }
        public Camera Camera { get; set; }
        public World World { get; set; }

        public Scene(IGameApp gameApp) : base(gameApp.Instance)
        {
            Effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DESIGN: Content better handler
            Camera = new Camera(gameApp.Settings.Camera, Game.GraphicsDevice.Viewport);
            World = new World(gameApp, Effect, Camera);
            GameSettings = gameApp.Settings;

            #region Graphics Config
            var rasterizerState = new RasterizerState()
            {
                CullMode = CullMode.CullClockwiseFace
            };
            GraphicsDevice.RasterizerState = rasterizerState;
            #endregion
        }

        public override void Initialize()
        {
            var cameraInput = new CameraInputComponent(Game, Camera, GameSettings);
            var cubeLayerInput = new CubeLayerInputComponent(Game, World, Camera, GameSettings.Camera.MaxLayingDistance);

            Game.Components.Add(cameraInput);
            Game.Components.Add(World);
            Game.Components.Add(cubeLayerInput);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
    }
}
