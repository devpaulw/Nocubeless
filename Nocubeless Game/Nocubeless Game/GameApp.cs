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
        // TO-DO: elimate IGameApp and pass Game and settings instead.
        public Game Instance { get; } // Allow interface to Components using

        public GameSettings Settings { get; set; }

        public Scene Scene { get; set; }

        public GameApp()
        {
            Instance = this as Game;

            Settings = GameSettings.Default;
            Settings.Graphics.SetToGame(this);

            Content.RootDirectory = "MGContent";
        }

        protected override void Initialize()
        {


            SceneInputComponent cameraInputComponent;

            
            cameraInputComponent = new SceneInputComponent(this);

            World = World.LoadFromTest(this);

            Components.Add(cameraInputComponent);
            Components.Add(Scene);

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