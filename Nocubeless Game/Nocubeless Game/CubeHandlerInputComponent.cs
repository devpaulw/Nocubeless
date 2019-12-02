using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    // TODO: InputGameComponent upd, Settings, break, IGameApp byebye, equals, break cubes with effect (same for lay), Design Up
    // 

    internal class CubeHandlerInputComponent : InputGameComponent
    {
        private Color nextColor;

        private bool @break;

        public World World { get; set; }
        public Camera Camera { get; set; }
        public float MaxLayingDistance { get; set; }

        public CubeHandlerInputComponent(Game game, InputKeySettings keySettings, World world, Camera camera, float maxLayingDistance) : base(game, keySettings) // New kind of design just tested
        {
            World = world;
            Camera = camera;
            MaxLayingDistance = maxLayingDistance;
        }

        public override void Update(GameTime gameTime)
        {
            CurrentMouseState = Mouse.GetState(); // to encapsulate
            CurrentKeyboardState = Keyboard.GetState();

            { // Break/Lay switcher
                if (CurrentKeyboardState.IsKeyDown(KeySettings.SwitchLayBreak) && OldKeyboardState.IsKeyUp(KeySettings.SwitchLayBreak))
                {
                    @break ^= true; // invert value
                }
            }

            if (!@break)
            {
                CubeCoordinate previewCubePosition = GetNewWorldTargetedPosition();
                Cube newCube = new Cube(nextColor, previewCubePosition);
                { // Prev
                    World.PreviewCube(newCube);
                }
                { // Lay
                    if (CurrentMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released)
                    {
                        Random random = new Random();
                        nextColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                        World.LayCube(newCube);
                    }
                }
            }
            else
            { // Break
                World.PreviewCube(null);

                if (CurrentMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released)
                {
                    World.BreakCube(GetTrueWorldTargetedPosition()); // DESIGN: You know the way
                }
            }

            base.Update(gameTime);
        }

        private CubeCoordinate GetNewWorldTargetedPosition() // Is not 100% trustworthy, and is not powerful, be careful
        {
            float sceneCubeRatio = 1.0f / World.Settings.HeightOfCubes / 2.0f; // Because a cube is x times smaller/bigger compared to the scene representation
            // cube ratio in world

            Vector3 checkPosition = Camera.Position * sceneCubeRatio;

            CubeCoordinate oldPosition = null;
            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 80;
            float checkIncrement = MaxLayingDistance / checkIntensity;

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

        private CubeCoordinate GetTrueWorldTargetedPosition() // Is not 100% trustworthy, and is not powerful, be careful
        {
            float sceneCubeRatio = 1.0f / World.Settings.HeightOfCubes / 2.0f; // Because a cube is x times smaller/bigger compared to the scene representation
            // cube ratio in world

            Vector3 checkPosition = Camera.Position * sceneCubeRatio;

            CubeCoordinate convertedCheckPosition;

            const int checkIntensity = 80;
            float checkIncrement = MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (!World.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                    return convertedCheckPosition;
            }

            return null;
        }
    }
}
