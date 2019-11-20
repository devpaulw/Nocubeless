using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class GameSettings
    {
        #region Inputs
        public GameInputKeys InputKeys { get; set; }
        public float MoveSpeed { get; set; }
        public float MouseSensitivity { get; set; }
        #endregion

        #region Graphics
        public double Framerate { get; set; }
        public bool UnlimitedFramerate { get; set; }
        public bool VSync { get; set; }
        public bool FullScreen { get; set; }

        public float CameraFov { get; set; }
        #endregion

        public void SetGameSettings(Game game, GraphicsDeviceManager graphics)
        {
            graphics.IsFullScreen = FullScreen;
            graphics.SynchronizeWithVerticalRetrace = VSync;
            if (FullScreen) // Make the fullscreen mode consistent with window resolution
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            game.IsFixedTimeStep = !UnlimitedFramerate;
            game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / Framerate); // Set framerate
        }

        public static GameSettings Default
        {
            get {
                return new GameSettings
                {
                    InputKeys = GameInputKeys.DefaultFrench,
                    MoveSpeed = 0.75f,
                    MouseSensitivity = 0.175f,

                    Framerate = 120,
                    UnlimitedFramerate = false,
                    VSync = true,
                    FullScreen = true,
                    CameraFov = 70
                };
            }
        }
    }
}
