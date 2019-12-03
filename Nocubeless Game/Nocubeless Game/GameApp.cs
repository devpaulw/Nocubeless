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
    internal class GameApp : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        public GameSettings Settings { get; set; }

        public GameApp()
        {
            Content.RootDirectory = "MGContent";

            Settings = GameSettings.Default;

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Settings.Graphics.SetToGame(this, graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            var scene = new Scene(this as Game, Settings);

            Components.Add(scene);

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
    }
}

// DOLATER: it's in the long run "to-do list"
// Make the light!
// menu and saves
// online
// extra funcs