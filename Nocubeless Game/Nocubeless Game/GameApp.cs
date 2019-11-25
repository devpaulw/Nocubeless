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
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public Game Instance { get; } // Allow interface to Components using

        public GameSettings Settings { get; set; }

        public GameApp()
        {
            Instance = this as Game;

            Content.RootDirectory = "MGContent";

            Settings = GameSettings.Default;

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Settings.Graphics.SetToGame(this, graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            var scene = new Scene(this);
            var sceneInput = new SceneInputComponent(this, scene);

            Components.Add(scene);
            Components.Add(sceneInput);

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