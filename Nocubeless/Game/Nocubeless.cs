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
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	class Nocubeless : Game
	{
		private readonly GraphicsDeviceManager graphicsDeviceManager;

		public SpriteBatch SpriteBatch { get; set; }
		public NocubelessSettings Settings { get; set; }
		public NocubelessState CurrentState { get; set; }

		public EulerCamera Camera { get; set; }
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

			Camera = new EulerCamera(Settings.Camera, GraphicsDevice.Viewport);
			CubeWorld = new CubeWorld(Settings.CubeWorld, /*new ShallowCubeWorldHandler()*/ new CubeWorldSaveHandler("save.nclws"));
			Player = new Player(PlayerSettings.Default, WorldCoordinates.Zero);

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
			Components.Add(new ColorPickerMenu(this));
			Components.Add(new NocubelessInputProcessorChooser(this));
			Components.Add(new DynamicEntitiesComponent(this));
			Components.Add(new CubeWorldProcessor(this));
			Components.Add(new InfoDisplayer(this));
			#endregion

			Input.MiddlePoint = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2); // DESIGN

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
			if (CurrentState == NocubelessState.Playing)
				GraphicsDevice.Clear(new Color((int)(149 * intensity), (int)(165 * intensity), (int)(166 * intensity)));
			else
				GraphicsDevice.Clear(Color.CadetBlue);

			SpriteBatch.Begin(blendState: GraphicsDevice.BlendState,
				depthStencilState: GraphicsDevice.DepthStencilState,
				rasterizerState: GraphicsDevice.RasterizerState);

			base.Draw(gameTime);

			SpriteBatch.End();
		}
	}
}