// That is an idea

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//namespace Nocubeless
//{
//	class NocubelessApp : Nocubeless
//	{
//		public NocubelessApp()
//		{

//		}

//		protected override void Initialize()
//		{
//			base.Initialize();

//			var playingInput = new PlayingInput(this);
//			var cubeWorldProcessor = new CubeWorldProcessor(this);
//			var colorPickerMenu = new ColorPickerMenu(this, playingInput.OnColorPicking);
//			var coordDisplayer = new InfoDisplayer(this);

//			Components.Add(playingInput);
//			Components.Add(cubeWorldProcessor);
//			Components.Add(colorPickerMenu);
//			Components.Add(coordDisplayer);
//		}

//		protected override void LoadContent()
//		{
//			if (Settings.Song.MusicEnabled) MediaPlayer.Play(Content.Load<Song>("Music/main_theme")); // I'm nice, I am making only one line for fun by waiting Content Design Update
//		}

//		protected override void Draw(GameTime gameTime)
//		{
//			float intensity = 1.0f;
//			GraphicsDevice.Clear(new Color((int)(149 * intensity), (int)(165 * intensity), (int)(166 * intensity)));

//			base.Draw(gameTime);
//		}
//	}
//}