using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    delegate void ColorPickingEventHandler(object sender, ColorPickingEventArgs e);

    class ColorPickingEventArgs : EventArgs
    {
        public CubeColor Color { get; set; }
    }
}
