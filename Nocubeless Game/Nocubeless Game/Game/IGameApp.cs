using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    interface IGameApp
    {
        Game Instance { get; set; }
        GameSettings Settings { get; set; }
        GameState ActualState { get; set; }

        Camera Camera { get; set; }
        CubicWorld CubicWorld { get; set; }
    }
}
