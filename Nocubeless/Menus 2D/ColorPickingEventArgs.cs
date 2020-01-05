using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    [Obsolete("No longer using events")]
    delegate void ColorPickingEventHandler(object sender, ColorPickingEventArgs e);

    [Obsolete("No longer using events")]
    class ColorPickingEventArgs : EventArgs
    {
        public CubeColor Color { get; set; }
    }
}
