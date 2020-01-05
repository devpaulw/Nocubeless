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
	class Nocubeless : Game
	{
		private readonly GraphicsDeviceManager graphicsDeviceManager;

		public SpriteBatch SpriteBatch { get; set; }
		public NocubelessSettings Settings { get; set; }
		public NocubelessState CurrentState { get; set; }

		public Camera Camera { get; set; }
		public CubeWorld CubeWorld { get; set; }
		public Player Player { get; set;}

		public Nocubeless()
		{
			graphicsDeviceManager = new GraphicsDeviceManager(this);

			Content.RootDirectory = "MGContent"; // DESIGN: Content better handler

			Settings = NocubelessSettings.Default;
			Settings.Graphics.SetToGame(this, graphicsDeviceManager);
		}

		protected override void Initialize()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			Camera = new Camera(Settings.Camera, GraphicsDevice.Viewport);
			CubeWorld = new CubeWorld(Settings.CubeWorld, /*new ShallowCubeWorldHandler()*/ new CubeWorldSaveHandler("save.nclws"));
			// TODO initialize with a PlayerSettings singleton
			Player = new Player(WorldCoordinates.Origin, 1, 3, 1, CubeWorld.GetGraphicsCubeRatio() / 2, Camera);

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
			var playingInput = new PlayingInput(this);
			var cubeWorldProcessor = new CubeWorldProcessor(this);
			var colorPickerMenu = new ColorPickerMenu(this, playingInput.OnColorPicking);
			var coordDisplayer = new InfoDisplayer(this);

			Components.Add(playingInput);
			Components.Add(cubeWorldProcessor);
			Components.Add(colorPickerMenu);
			Components.Add(coordDisplayer);
			#endregion

			base.Initialize();
		}

		protected override void LoadContent()
		{
			if (Settings.Song.MusicEnabled) MediaPlayer.Play(Content.Load<Song>("Music/main_theme")); // I'm nice, I am making only one line for fun by waiting Content Design Update
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
			float intensity = 1.0f;
			GraphicsDevice.Clear(new Color((int)(149 * intensity), (int)(165 * intensity), (int)(166 * intensity)));

			SpriteBatch.Begin(blendState: GraphicsDevice.BlendState,
				depthStencilState: GraphicsDevice.DepthStencilState,
				rasterizerState: GraphicsDevice.RasterizerState);

			base.Draw(gameTime);

			SpriteBatch.End();
		}
	}
}