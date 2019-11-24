using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class GraphicsSettings
    {
        public double Framerate { get; set; }
        public bool UnlimitedFramerate { get; set; }
        public bool VSync { get; set; }
        public bool FullScreen { get; set; }

        

        public void SetToGame(Game game)
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(game);

            graphics.IsFullScreen = FullScreen;
            graphics.SynchronizeWithVerticalRetrace = VSync;

            if (FullScreen | true) // Make the fullscreen mode consistent with window resolution
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            game.IsFixedTimeStep = !UnlimitedFramerate;
            game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / Framerate); // Set framerate
            game.IsMouseVisible = true;
        }

        public static GraphicsSettings Default
        {
            get {
                return new GraphicsSettings
                {
                    Framerate = 120,
                    UnlimitedFramerate = false,
                    VSync =  true,
                    FullScreen = true,
                };
            }
        }
    }
}
