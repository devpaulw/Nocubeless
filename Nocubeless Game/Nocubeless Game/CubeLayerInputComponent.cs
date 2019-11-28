using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CubeLayerInputComponent : GameComponent
    {
        private MouseState currentMouseState;

        private bool mouseRightButtonPressed;

        public World World { get; set; }
        public Camera Camera { get; set; }
        public float MaxLayingDistance { get; set; }

        public CubeLayerInputComponent(Game game, World world, Camera camera, float maxLayingDistance) : base(game) // New kind of design just tested
        {
            World = world;
            Camera = camera;
            MaxLayingDistance = maxLayingDistance;
        }

        public override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();

            CubeCoordinate previewCubePosition = GetWorldTargetedPosition();
            { // Prev
                World.PreviewCube(previewCubePosition);
            } // Lay
            {
                if (!mouseRightButtonPressed && currentMouseState.RightButton == ButtonState.Pressed)
                {
                    mouseRightButtonPressed = true;

                    Random random = new Random();
                    Color cubeColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                    Cube newCube = new Cube(cubeColor, previewCubePosition);
                    World.LayCube(newCube);
                }
                else if (currentMouseState.RightButton == ButtonState.Released)
                    mouseRightButtonPressed = false;
            }

            base.Update(gameTime);
        }

        private CubeCoordinate GetWorldTargetedPosition() // Is not 100% trustworthy, and is not powerful, be careful
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
    }
}
