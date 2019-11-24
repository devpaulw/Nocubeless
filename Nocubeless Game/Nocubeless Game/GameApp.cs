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
    internal class GameApp : Game, IGameApp
    {
        private readonly GraphicsDeviceManager graphics;

        public Game Instance { get; } // Allow interface to Components using

        public GameSettings Settings { get; set; }

        public World World { get; set; }

        public GameApp()
        {
            Instance = this as Game;

            Settings = GameSettings.Default;

            graphics = new GraphicsDeviceManager(this);
            Settings.SetGameSettings(this, graphics); // Graphics initialization from Settings

            IsMouseVisible = true;

            Content.RootDirectory = "MGContent";
        }

        protected override void Initialize()
        {
            CameraInputComponent cameraInputComponent;

            
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

        private bool pressed = false; // WTF, why "kio"

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive) // Don't take in care when window is not focused
                return;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Random rnd = new Random();

            if (!pressed && Mouse.GetState().RightButton == ButtonState.Pressed)// WTF, why "kio"
            {
                pressed = true;
                World.LayCube(new Cube(new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), World.GetAvailableSpaceFromCamera(Camera)));
            }
            else if (Mouse.GetState().RightButton == ButtonState.Released)// WTF, why "kio"
                pressed = false;// WTF, why "kio"

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            World.PreviewCube(new Cube(Color.PaleVioletRed, World.GetAvailableSpaceFromCamera(Camera)));

            base.Draw(gameTime);
        }
    }
}

// UNDONE:
// Make the light!
// be able to lay cubes and perceive which cube we are pointing for
// gravity and collisions
// song
// menu and saves
// online
// extra funcs