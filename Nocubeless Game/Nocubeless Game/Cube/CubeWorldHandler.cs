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

    internal class CubeWorldHandler : NocubelessComponent
    { 
        private Color nextColor;

        private bool @break;

        public CubeWorldHandler(Nocubeless nocubeless) : base(nocubeless) { }

        public override void Update(GameTime gameTime)
        {
            { // Break/Lay switcher
                if (Nocubeless.Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.SwitchLayBreak) 
                    && Nocubeless.Input.OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.SwitchLayBreak))
                {
                    @break = !@break; // invert value
                }
            }

            if (!@break)
            {
                CubeWorldCoordinates previewCubePosition = GetWorldTargetedNewCube();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    Nocubeless.CubicWorld.PreviewCube(newCube);
                }
                { // Lay
                    if (Nocubeless.Input.CurrentMouseState.RightButton == ButtonState.Pressed 
                        && Nocubeless.Input.OldMouseState.RightButton == ButtonState.Released)
                    {
                        Random random = new Random();
                        nextColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                        Nocubeless.CubicWorld.LayPreviewedCube();
                    }
                }
            }
            else
            { // Break
                Nocubeless.CubicWorld.PreviewCube(null);

                if (Nocubeless.Input.CurrentMouseState.LeftButton == ButtonState.Pressed 
                    && Nocubeless.Input.OldMouseState.LeftButton == ButtonState.Released)
                {
                    CubeWorldCoordinates toBreakCube = GetWorldTargetedCube();
                    Nocubeless.CubicWorld.BreakCube(toBreakCube); // DESIGN: You know the way
                }
            }

            base.Update(gameTime);
        }

        public void OnColorPicking(object sender, ColorPickingEventArgs e) // c pas beau ça ?
        {
            nextColor = e.CubeColor;
        }

        private CubeWorldCoordinates GetWorldTargetedNewCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubicWorld.SceneCubeRatio;

            CubeWorldCoordinates oldPosition = null;
            CubeWorldCoordinates actualPosition = null;
            CubeWorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Nocubeless.Settings.CubeHandler.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Nocubeless.Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !Nocubeless.CubicWorld.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return oldPosition;
                    else if (actualPosition != null) // Or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
        private CubeWorldCoordinates GetWorldTargetedCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubicWorld.SceneCubeRatio;

            CubeWorldCoordinates actualPosition = null;
            CubeWorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Nocubeless.Settings.CubeHandler.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Nocubeless.Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition)
                {
                    if (!Nocubeless.CubicWorld.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return null;
        }
    }
}
