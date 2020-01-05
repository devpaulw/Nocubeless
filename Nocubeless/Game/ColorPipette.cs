using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ColorPipette : NocubelessComponent
    {
        public ColorPipette(Nocubeless nocubeless) : base(nocubeless) { }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasMiddleMouseButtonJustPressed())
            {
                Pick();
            }

            base.Update(gameTime);
        }

        public void Pick()
        {
            // TODO: Put in  separated another class
            var targetCubeCoordinates = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
            var targetCubeColor = Nocubeless.CubeWorld.GetCubeColorAt(targetCubeCoordinates);

            if (targetCubeColor != null)
                Nocubeless.Player.NextColorToLay = targetCubeColor;
        }
    }
}
