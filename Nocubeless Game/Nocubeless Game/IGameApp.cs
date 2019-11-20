using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal interface IGameApp
    {
        GameSettings Settings { get; set; }
        Camera Camera { get; set; }
        World World { get; set; }
    }
}
