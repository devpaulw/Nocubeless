/* Parallel SpydotNet branch */

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
	class Nocubeless : Game // mediator MAIN CLASS
	{

		private readonly GraphicsDeviceManager graphicsDeviceManager;
		// test spydotnet
		public SpriteBatch SpriteBatch { get; set; }
		public NocubelessSettings Settings { get; set; }
		public NocubelessState CurrentState { get; set; }

		public Window Window { get; set; }
		public Camera Camera { get; set; }
		public CubeWorld CubeWorld { get; set; }
		public CubeWorldScene Scene { get; set; }
		public Player Player { get; set;}
		public Input Input { get; set; }

		public Nocubeless()
		{
			graphicsDeviceManager = new GraphicsDeviceManager(this);

			Content.RootDirectory = "MGContent"; // DESIGN: Content better handler

			Settings = NocubelessSettings.Default;
			Settings.Graphics.SetToGame(this, graphicsDeviceManager);

			#region Test Zone
			//var testSave = new CubeWorldSaveHandler(@"save.nws");

			//var rnd = new Random();

			//var testChunk = new CubeChunk(new Coordinates(-8, -8, -8));
			///*tmp*/
			//for (int i = 0; i < CubeChunk.TotalSize; i++)
			//    testChunk[i] = new CubeColor(rnd.Next(0, 8), rnd.Next(0, 8), rnd.Next(0, 8));

			//testSave.SetChunk(testChunk);

			//testSave.GetChunkAt(new Coordinates(0, 0, 0));

			#endregion
		}

		protected override void Initialize()
		{
			Window = new Window(GraphicsDevice);
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			Camera = new Camera(Settings.Camera, GraphicsDevice.Viewport);
			CubeWorld = new CubeWorld(Settings.CubeWorld, /*new ShallowCubeWorldHandler()*/ new CubeWorldSaveHandler("save.nclws"));
			Scene = new CubeWorldScene(this);
			Player = new Player(new Vector3(0, 0, 0), 1, 3, 1, CubeWorld.GetGraphicsCubeRatio() / 2);
			Input = new Input(this);
			//Window = new Window(this);

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
			var playerInputProcessing = new PlayerInputProcessor(this);
			var cubeWorldSceneInput = new CubeWorldSceneInput(this, Scene);
			var colorPickerMenu = new ColorPickerMenu(this, cubeWorldSceneInput.OnColorPicking);
			var coordDisplayer = new InfoDisplayer(this);

			Components.Add(Input);
			Components.Add(playerInputProcessing);
			Components.Add(Scene);
			Components.Add(cubeWorldSceneInput);
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