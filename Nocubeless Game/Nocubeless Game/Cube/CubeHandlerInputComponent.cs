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
    // TODO: make GameState

    internal class CubeHandlerInputComponent : InputGameComponent
    {
        private ColorPickerMenu colorPickerMenu;
        private Color nextColor;

        private bool @break;

        public CubicWorld CubicWorld { get; set; }
        public Camera Camera { get; set; }
        public CubeHandlerSettings Settings { get; set; }

        public CubeHandlerInputComponent(IGameApp gameApp) : base(gameApp) // New kind of design just tested
        {
            CubicWorld = gameApp.CubicWorld;
            Camera = gameApp.Camera;
            Settings = gameApp.Settings.CubeHandler;

            colorPickerMenu = new ColorPickerMenu(gameApp);
        }

        public override void Initialize()
        {
            Game.Components.Add(colorPickerMenu);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ReloadCurrentStates();

            { // Break/Lay switcher
                if (CurrentKeyboardState.IsKeyDown(KeySettings.SwitchLayBreak) && OldKeyboardState.IsKeyUp(KeySettings.SwitchLayBreak))
                {
                    @break ^= true; // invert value
                }
            }

            { // Show Color Picker Menu
                if (CurrentKeyboardState.IsKeyDown(KeySettings.ShowColorPicker) && OldKeyboardState.IsKeyUp(KeySettings.ShowColorPicker))
                {
                    colorPickerMenu.Show ^= true;
                }
            }

            if (!@break)
            {
                CubeCoordinate previewCubePosition = GetWorldTargetedNewCube();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    CubicWorld.PreviewCube(newCube);
                }
                { // Lay
                    if (CurrentMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released)
                    {
                        Random random = new Random();
                        nextColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                        CubicWorld.LayPreviewedCube();
                    }
                }
            }
            else
            { // Break
                CubicWorld.PreviewCube(null);

                if (CurrentMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released)
                {
                    CubeCoordinate toBreakCube = GetWorldTargetedCube();
                    CubicWorld.BreakCube(toBreakCube); // DESIGN: You know the way
                }
            }

            base.Update(gameTime);
        }



        private CubeCoordinate GetWorldTargetedNewCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Camera.Position * CubicWorld.SceneCubeRatio;

            CubeCoordinate oldPosition = null;
            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Settings.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !CubicWorld.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return oldPosition;
                    else if (actualPosition != null) // Or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
        private CubeCoordinate GetWorldTargetedCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Camera.Position * CubicWorld.SceneCubeRatio;

            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Settings.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition)
                {
                    if (!CubicWorld.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return null;
        }
    }
}
