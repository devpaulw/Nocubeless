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

    internal class CubeHandlerInputComponent : NocubelessInputComponent
    {
        private readonly ColorPickerMenu colorPickerMenu;
        private Color nextColor;

        private bool @break;

        public CubeHandlerInputComponent(Nocubeless nocubeless) : base(nocubeless)
        {
            colorPickerMenu = new ColorPickerMenu(nocubeless);
        }

        public override void Initialize()
        {
            Nocubeless.Components.Add(colorPickerMenu);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ReloadCurrentStates();

            { // Show Color Picker Menu
                if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.ShowColorPicker)
                    && OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.ShowColorPicker))
                {
                    if (Nocubeless.CurrentState == NocubelessState.Playing)
                        Nocubeless.CurrentState = NocubelessState.ColorPicking;
                    else
                    {
                        Nocubeless.CurrentState = NocubelessState.Playing;
                        SetMouseInTheMiddle(); // Don't disturb Camera new position
                    }
                }
            }

            { // Break/Lay switcher
                if (CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.SwitchLayBreak) 
                    && OldKeyboardState.IsKeyUp(Nocubeless.Settings.Keys.SwitchLayBreak))
                {
                    @break = !@break; // invert value
                }
            }

            if (!@break)
            {
                WorldCoordinates previewCubePosition = GetWorldTargetedNewCube();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    Nocubeless.CubicWorld.PreviewCube(newCube);
                }
                { // Lay
                    if (CurrentMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released)
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

                if (CurrentMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released)
                {
                    WorldCoordinates toBreakCube = GetWorldTargetedCube();
                    Nocubeless.CubicWorld.BreakCube(toBreakCube); // DESIGN: You know the way
                }
            }

            base.Update(gameTime);
        }



        private WorldCoordinates GetWorldTargetedNewCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubicWorld.SceneCubeRatio;

            WorldCoordinates oldPosition = null;
            WorldCoordinates actualPosition = null;
            WorldCoordinates convertedCheckPosition;

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
        private WorldCoordinates GetWorldTargetedCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubicWorld.SceneCubeRatio;

            WorldCoordinates actualPosition = null;
            WorldCoordinates convertedCheckPosition;

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
