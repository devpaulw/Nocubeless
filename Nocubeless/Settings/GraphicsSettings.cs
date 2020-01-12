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
        public int ChunkViewDistance { get; set; }

        public static GraphicsSettings Default
        {
            get {
                return new GraphicsSettings
                {
                    Framerate = 120,
                    UnlimitedFramerate = true,
                    VSync =  true,
                    FullScreen = false,
                    ChunkViewDistance = 4
                };
            }
        }
    }
}
