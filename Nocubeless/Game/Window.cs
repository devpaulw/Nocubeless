using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class Window
	{
		public GraphicsDevice GraphicsDevice { get; private set; }
		public Point Center { get; private set; }
		public Window(GraphicsDevice graphicsDevice)
		{
			GraphicsDevice = graphicsDevice;
			Center = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
		}
	}
}
