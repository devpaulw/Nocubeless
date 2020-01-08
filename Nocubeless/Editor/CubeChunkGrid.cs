using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeChunkGrid : NocubelessDrawableComponent
    {
        private readonly CubeEffect cubeEffect;

        public CubeChunkGrid(Nocubeless nocubeless) : base(nocubeless)
        {
            cubeEffect = new CubeEffect(Game);
        }

        public override void Draw(GameTime gameTime)
        {


            base.Draw(gameTime);
        }
    }
}
