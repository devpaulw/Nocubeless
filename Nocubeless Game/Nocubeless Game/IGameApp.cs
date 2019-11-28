using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal interface IGameApp // to deprecate
    {
        Game Instance { get; }
        GameSettings Settings { get; set; }
    }
}
