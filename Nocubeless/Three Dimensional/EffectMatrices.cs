using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class EffectMatrices : IEffectMatrices
    {
        public Matrix Projection { get; set; }
        public Matrix View { get; set; }
        public Matrix World { get; set; }

        public EffectMatrices(Matrix projection, Matrix view, Matrix world)
        {
            Projection = projection;
            View = view;
            World = world;
        }
    }
}
