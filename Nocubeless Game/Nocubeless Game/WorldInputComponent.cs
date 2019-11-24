using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class WorldInputComponent : GameComponent
    {
        public InputKeySettings InputKeys { get; set; }
        public World World { get; set; }
        public Camera Camera { get; set; }
        public

        public WorldInputComponent(IGameApp game) : base(game.Instance)
        {
            InputKeys = game.Settings.InputKeys;
            Camera = game.Camera;
            World = game.World;

            Camera = new Camera(game);
        }

        public override void Initialize()
        {
            // Settings
            // Max laying distance: where?
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public CubeCoordinate GetAvailableSpaceFromCamera(Camera camera) // Is not 100% trustworthy, and is not powerful, be careful
        {
            float sceneCubeRatio = 1.0f / World.Settings.HeightOfCubes / 2.0f; // Because a cube is x times smaller/bigger compared to the scene representation
            // cube ratio in world

            Vector3 checkPosition = camera.Position * sceneCubeRatio;

            CubeCoordinate oldPosition = null;
            CubeCoordinate actualPosition = null;
            CubeCoordinate convertedCheckPosition;

            int checkIntensity = 40;
            float checkIncrement = 4 / (float)checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += camera.Front /*Fix design there*/ * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !cubes.IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
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
