/* Parallel SpydotNet branch */

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
    class Nocubeless : Game // mediator MAIN CLASS
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public NocubelessInput Input { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public NocubelessSettings Settings { get; set; }
        public NocubelessState CurrentState { get; set; }

        public Camera Camera { get; set; }
        public CubeWorld CubeWorld { get; set; }

        public Nocubeless()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "MGContent"; // DESIGN: Content better handler

            Settings = NocubelessSettings.Default;
            Settings.Graphics.SetToGame(this, graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            Input = new NocubelessInput();
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Camera = new Camera(Settings.Camera, GraphicsDevice.Viewport);
            CubeWorld = new CubeWorld(Settings.CubeWorld /*TODO: To rename CubeWorld*/);
            
            #region Graphics Config
            var blendState = BlendState.AlphaBlend;
            var rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            GraphicsDevice.BlendState = blendState;
            GraphicsDevice.RasterizerState = rasterizerState;
            #endregion

            #region Components Linking
            var cameraInput = new CameraInputComponent(this);
            var cubeWorldScene = new CubeWorldScene(this);
            var cubeWorldSceneHandler = new CubeWorldSceneHandler(this, cubeWorldScene);
            var colorPickerMenu = new ColorPickerMenu(this, cubeWorldSceneHandler.OnColorPicking);

            Components.Add(cameraInput);
            Components.Add(cubeWorldScene);
            Components.Add(cubeWorldSceneHandler);
            Components.Add(colorPickerMenu);
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

            Input.ReloadCurrentStates();

            if (Input.CurrentKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            Input.ReloadOldStates();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            SpriteBatch.Begin(SpriteSortMode.Deferred,
                    GraphicsDevice.BlendState,
                    null,
                    GraphicsDevice.DepthStencilState,
                    GraphicsDevice.RasterizerState);

            base.Draw(gameTime);

            SpriteBatch.End();
        }
    }
}

// DOLATER: it's in the long run "to-do list"
// color picker
// menu and saves
// online
// extra funcs