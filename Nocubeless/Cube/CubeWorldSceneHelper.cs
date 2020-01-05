using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    static class CubeWorldHelper
    {
        public static WorldCoordinates GetTargetedCube(this CubeWorld cubeWorld, Camera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = camera.Position * cubeWorld.GetGraphicsCubeRatio(); // Not a beautiful way!

            WorldCoordinates actualPosition = null;
            WorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)maxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // in World, is free space
                checkPosition += camera.Front * checkIncrement; // increment check zone
                convertedCheckPosition = new WorldCoordinates(checkPosition);

                if (convertedCheckPosition == actualPosition)
                {
                    if (!cubeWorld.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
        public static WorldCoordinates GetTargetedNewCube(this CubeWorld cubeWorld, Camera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = camera.Position * cubeWorld.GetGraphicsCubeRatio();

            WorldCoordinates oldPosition = null;
            WorldCoordinates actualPosition = null;
            WorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)maxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // in World, is free space
                checkPosition += camera.Front * checkIncrement; // increment check zone
                convertedCheckPosition = new WorldCoordinates(checkPosition);

                if (convertedCheckPosition == actualPosition) // perf maintainer
                {
                    if (!(oldPosition is null) && !cubeWorld.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                        return oldPosition;
                    else if (!(actualPosition is null)) // or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
    }
}
