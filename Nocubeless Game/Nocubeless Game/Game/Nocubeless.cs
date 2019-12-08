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
    class Nocubeless : Game // Mediator MAIN CLASS
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public NocubelessInput Input { get; set; }
        public NocubelessSettings Settings { get; set; }
        public NocubelessState CurrentState { get; set; }

        public Camera Camera { get; set; }
        public CubeWorld CubicWorld { get; set; }

        public Nocubeless()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "MGContent"; // DESIGN: Content better handler

            Input = new NocubelessInput();
            Settings = NocubelessSettings.Default;

            Settings.Graphics.SetToGame(this, graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            Camera = new Camera(Settings.Camera, GraphicsDevice.Viewport);
            CubicWorld = new CubeWorld(this);

            #region Graphics Config
            var rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            GraphicsDevice.RasterizerState = rasterizerState;
            #endregion

            #region Components Linking
            var cameraInput = new CameraInputComponent(this);
            var cubeHandler = new CubeWorldHandler(this);
            var colorPickerMenu = new ColorPickerMenu(this, cubeHandler.OnColorPicking);

            Components.Add(CubicWorld);
            Components.Add(cameraInput);
            Components.Add(cubeHandler);
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

            base.Draw(gameTime);
        }
    }
}

// DOLATER: it's in the long run "to-do list"
// menu and saves
// online
// extra funcs