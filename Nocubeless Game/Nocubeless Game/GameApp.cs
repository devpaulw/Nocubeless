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
    internal class GameApp : Game
    {
        private readonly GraphicsDeviceManager graphics;

        public GameSettings Settings { get; private set; }

        public Camera Camera { get; set; }
        public World World { get; set; }
        

        public GameApp()
        {
            Settings = GameSettings.Default;

            graphics = new GraphicsDeviceManager(this);
            Settings.SetGameSettings(this, graphics); // Graphics initialization from Settings

            IsMouseVisible = false;

            Content.RootDirectory = "MGContent";
        }

        protected override void Initialize()
        {
            CameraInputComponent cameraInputComponent;

            Camera = new Camera(this);
            cameraInputComponent = new CameraInputComponent(this);

            World = World.LoadFromTest(this);

            Components.Add(cameraInputComponent);
            Components.Add(World);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // When I'll have content
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
            GraphicsDevice.Clear(new Color(32, 2, 128));
            
            base.Draw(gameTime);
        }
    }
}
