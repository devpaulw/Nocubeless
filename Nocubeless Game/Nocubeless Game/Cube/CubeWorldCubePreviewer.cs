using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Nocubeless
{
    class CubeWorldCubePreviewer : CubeWorldDrawer // chelou
    {
        private Cube previewableCube;

        public CubeWorldCubePreviewer(Nocubeless nocubeless, 
            ColorPickingEventHandler colorPickingEventHandler) : base(nocubeless)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            if (previewableCube != null)
                DrawCube(previewableCube, 0.5f);

            base.Draw(gameTime);
        }

        private void OnColorPicking(object sender, ColorPickingEventArgs e)
        {

        }
    }
}
