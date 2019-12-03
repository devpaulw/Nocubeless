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

        public Scene(Game game, GameSettings gameSettings) : base(game)
        {
            GameSettings = gameSettings;

            Effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DESIGN: Content better handler
            Camera = new Camera(GameSettings.Camera, Game.GraphicsDevice.Viewport);
            World = new World(game, GameSettings.World, Effect, Camera);
            
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
            var cubeHandlerInput = new CubeHandlerInputComponent(Game, GameSettings.InputKeys, GameSettings.CubeHandler, World, Camera);

            Game.Components.Add(cameraInput);
            Game.Components.Add(World);
            Game.Components.Add(cubeHandlerInput);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
    }
}
