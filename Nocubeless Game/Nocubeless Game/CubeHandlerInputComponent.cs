using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    // TODO: break, break cubes with effect (same for lay), Design Up
    // 

    internal class CubeHandlerInputComponent : InputGameComponent
    {
        private Color nextColor;

        private bool @break;

        public World World { get; set; }
        public Camera Camera { get; set; }
        public CubeHandlerSettings Settings { get; set; }

        public CubeHandlerInputComponent(Game game, InputKeySettings keySettings, CubeHandlerSettings settings, World world, Camera camera) : base(game, keySettings) // New kind of design just tested
        {
            World = world;
            Camera = camera;
            Settings = settings;
        }

        public override void Update(GameTime gameTime)
        {
            ReloadStates();

            { // Break/Lay switcher
                if (CurrentKeyboardState.IsKeyDown(KeySettings.SwitchLayBreak) && OldKeyboardState.IsKeyUp(KeySettings.SwitchLayBreak))
                {
                    @break ^= true; // invert value
                }
            }

            if (!@break)
            {
                CubeCoordinate previewCubePosition = GetNewWorldTargetedCube();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    World.PreviewCube(newCube);
                }
                { // Lay
                    if (CurrentMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released)
                    {
                        Random random = new Random();
                        nextColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                        World.LayPreviewedCube();
                    }
                }
            }
            else
            { // Break
                World.PreviewCube(null);

                if (CurrentMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released)
                {
                    CubeCoordinate toBreakCube = GetWorldTargetedCube();
                    World.BreakCube(toBreakCube); // DESIGN: You know the way
                }
            }

            base.Update(gameTime);
        }

        private CubeCoordinate GetNewWorldTargetedCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Camera.Position * World.SceneCubeRatio;

            CubeCoordinate oldPosition = null;
            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 80;
            float checkIncrement = (float)Settings.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !World.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
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
            Vector3 checkPosition = Camera.Position * World.SceneCubeRatio;

            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 80;
            float checkIncrement = (float)Settings.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition)
                {
                    if (!World.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return null;
        }
    }
}
