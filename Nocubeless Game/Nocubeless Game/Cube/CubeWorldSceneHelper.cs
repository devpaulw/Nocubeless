using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    static class CubeWorldSceneHelper
    {
        public static Coordinates GetTargetedCube(this CubeWorldScene scene, Camera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = camera.Position * scene.World.GetGraphicsCubeRatio();

            Coordinates actualPosition = null;
            Coordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)maxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // in World, is free space
                checkPosition += camera.Front * checkIncrement; // increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition)
                {
                    if (!scene.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return null;
        }
        public static Coordinates GetTargetedNewCube(this CubeWorldScene scene, Camera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = camera.Position * scene.World.GetGraphicsCubeRatio();

            Coordinates oldPosition = null;
            Coordinates actualPosition = null;
            Coordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)maxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // in World, is free space
                checkPosition += camera.Front * checkIncrement; // increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // perf maintainer
                {
                    if (oldPosition != null && !scene.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                        return oldPosition;
                    else if (actualPosition != null) // or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }
    }
}
