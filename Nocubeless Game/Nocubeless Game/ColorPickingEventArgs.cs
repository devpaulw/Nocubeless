using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ColorPickingEventArgs : EventArgs
    {
        public Color CubeColor { get; set; }
    }
}
