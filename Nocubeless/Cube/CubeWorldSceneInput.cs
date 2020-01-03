using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CubeWorldSceneInput : NocubelessComponent
    {
        private CubeColor nextColor;

        private bool @break;


        public CubeWorldSceneInput(Nocubeless nocubeless) : base(nocubeless)
        {
            nextColor = new CubeColor(7, 7, 7);
        }

        public override void Update(GameTime gameTime)
        {
            if (Nocubeless.CurrentState == NocubelessState.Playing)
            {
                { // break/Lay switcher
                    if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.SwitchLayBreak)
                        && Input.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.SwitchLayBreak))
                    {
                        @break = !@break; // invert value
                    }
                }


                if (!@break)
                {
                    Coordinates previewCubePosition = Nocubeless.CubeWorld.GetTargetedNewCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
                    Cube newCube = new Cube(nextColor, previewCubePosition);
                    { // prev
                        Nocubeless.CubeWorld.PreviewCube(newCube);
                    }
                    { // lay
                        if (Input.CurrentMouseState.RightButton == ButtonState.Pressed
                            && Input.OldMouseState.RightButton == ButtonState.Released)
                        {
                            Nocubeless.CubeWorld.LayCube(newCube);
                        }
                    }
                }
                else
                { // break
                    Nocubeless.CubeWorld.PreviewCube(null);

                    if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed
                        && Input.OldMouseState.LeftButton == ButtonState.Released)
                    {
                        Coordinates toBreakCube = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
                        Nocubeless.CubeWorld.BreakCube(toBreakCube); // DESIGN: You know the way
                    }
                }
            }

            base.Update(gameTime);
        }

        public void OnColorPicking(object sender, ColorPickingEventArgs e) // c pas beau ça ?
        {
            nextColor = e.Color;
        }
    }
}
