using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    abstract class NocubelessComponent : GameComponent
    {
        public Nocubeless Nocubeless { get; }

        public NocubelessComponent(Nocubeless nocubeless) : base(nocubeless)
        {
            Nocubeless = nocubeless;
        }
    }
}
