using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    // TODO: break cubes with effect (same for lay), Design Up
    // On Color Picking event (with color EventArgs) -> World.NextPreviewCube
    // -> CubeHandler can use it

    // OnLayCube Event? OnBreakCube?

    internal class CubeWorldSceneHandler : NocubelessComponent // DESIGN: Does handler is really a good meaning?
    { 
        private Vector3 nextColor;

        private bool @break;

        public CubeWorldScene Scene { get; set; }

        public CubeWorldSceneHandler(Nocubeless nocubeless, CubeWorldScene scene) : base(nocubeless)
        {
            Scene = scene;
        }

        public override void Update(GameTime gameTime)
        {
            { // Break/Lay switcher
                if (GameInput.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.SwitchLayBreak) 
                    && GameInput.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.SwitchLayBreak))
                {
                    @break = !@break; // invert value
                }
            }

            if (!@break)
            {
                CubeWorldCoordinates previewCubePosition = Scene.GetTargetedNewCube();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    Scene.PreviewCube(newCube);
                }
                { // Lay
                    if (GameInput.CurrentMouseState.RightButton == ButtonState.Pressed 
                        && GameInput.OldMouseState.RightButton == ButtonState.Released)
                    {
                        Scene.LayPreviewedCube();
                    }
                }
            }
            else
            { // Break
                Scene.PreviewCube(null);

                if (GameInput.CurrentMouseState.LeftButton == ButtonState.Pressed 
                    && GameInput.OldMouseState.LeftButton == ButtonState.Released)
                {
                    CubeWorldCoordinates toBreakCube = Scene.GetTargetedCube();
                    Nocubeless.CubeWorld.BreakCube(toBreakCube); // DESIGN: You know the way
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
