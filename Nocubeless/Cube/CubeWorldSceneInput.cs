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

        public CubeWorldScene Scene { get; set; }

        public CubeWorldSceneInput(Nocubeless nocubeless, CubeWorldScene scene) : base(nocubeless)
        {
            Scene = scene;

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
                    Coordinates previewCubePosition = Scene.GetTargetedNewCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
                    Cube newCube = new Cube(nextColor, previewCubePosition);
                    { // prev
                        Scene.PreviewCube(newCube);
                    }
                    { // lay
                        if (Input.CurrentMouseState.RightButton == ButtonState.Pressed
                            && Input.OldMouseState.RightButton == ButtonState.Released)
                        {
                            Scene.LayCube(newCube);
                        }
                    }
                }
                else
                { // break
                    Scene.PreviewCube(null);

                    if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed
                        && Input.OldMouseState.LeftButton == ButtonState.Released)
                    {
                        Coordinates toBreakCube = Scene.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
                        Scene.BreakCube(toBreakCube); // DESIGN: You know the way
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
