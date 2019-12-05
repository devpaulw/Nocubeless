using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Nocubeless
{
    class GameApp : Game, IGameApp // Mediator MAIN CLASS
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public Game Instance { get; set; }
        public GameSettings Settings { get; set; }
        public GameState ActualState { get; set; }

        public Camera Camera { get; set; }
        public CubicWorld CubicWorld { get; set; }

        public GameApp()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "MGContent"; // DESIGN: Content better handler

            Instance = this as Game;
            Settings = GameSettings.Default;

            Settings.Graphics.SetToGame(this, graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            Camera = new Camera(Settings.Camera, GraphicsDevice.Viewport);
            CubicWorld = new CubicWorld(this);

            #region Graphics Config
            var rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            GraphicsDevice.RasterizerState = rasterizerState;
            #endregion

            #region Components Linking
            var cameraInput = new CameraInputComponent(this);
            var cubeHandlerInput = new CubeHandlerInputComponent(this);

            Components.Add(CubicWorld);
            Components.Add(cameraInput);
            Components.Add(cubeHandlerInput);
            #endregion
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (Settings.Song.MusicEnabled) MediaPlayer.Play(Content.Load<Song>("main_theme")); // I'm nice, I am making only one line for fun by waiting Content Design Update
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive) // Don't take in care when window is not focused
                return;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
    }
}

// DOLATER: it's in the long run "to-do list"
// Make the light!
// menu and saves
// online
// extra funcs