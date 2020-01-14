/* DEVELOP BRANCH */

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
	partial class Nocubeless : Game
	{
		private readonly GraphicsDeviceManager graphicsDeviceManager;

		public SpriteBatch SpriteBatch { get; set; }
		public NocubelessSettings Settings { get; set; }
		public NocubelessState CurrentState { get; set; }

		public Camera Camera { get; set; }
		public CubeWorld CubeWorld { get; set; }
		public Player Player { get; set;}

		public void SetState(NocubelessState state, NocubelessState? fromState = null)
		{
			CurrentState = state;

			switch (state)
			{
				case NocubelessState.Playing:
					{
						Camera = new PlayingCamera(Settings.Camera, GraphicsDevice.Viewport);
						Input.SetMouseInTheMiddle(); // important! Do not ignore

						clearColor = new Color(149, 165, 166);

						if (Settings.Song.MusicEnabled) MediaPlayer.Play(Content.Load<Song>("Music/playing_theme")); // I'm nice, I am making only one line for fun by waiting Content Design Update
					}
					break;

				case NocubelessState.Editing:
					{
						if (fromState != NocubelessState.ColorPicking)
						{
							CubeWorld.PreviewableCube.Coordinates =
								CubeWorld.GetCoordinatesFromGraphics(Camera.ScreenPosition);

							Camera = new EditingCamera(Settings.Camera, GraphicsDevice.Viewport, Settings.EditingMode.WorldRotationDistance);
							
							clearColor = Color.CadetBlue;

							if (Settings.Song.MusicEnabled) MediaPlayer.Play(Content.Load<Song>("Music/editing_theme")); // I'm nice, I am making only one line for fun by waiting Content Design Update
						}
					}
					break;
			}
		}

		private void SetGraphicsSettings()
		{
			graphicsDeviceManager.IsFullScreen = Settings.Graphics.FullScreen;
			graphicsDeviceManager.SynchronizeWithVerticalRetrace = Settings.Graphics.VSync;

			if (Settings.Graphics.FullScreen | true) // Make the fullscreen mode consistent with window resolution
			{
				graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}

			IsFixedTimeStep = !Settings.Graphics.UnlimitedFramerate;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / Settings.Graphics.Framerate); // Set framerate
			IsMouseVisible = true;
		}
	}
}