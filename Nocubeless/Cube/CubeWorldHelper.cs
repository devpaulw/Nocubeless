using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCoordinates = Microsoft.Xna.Framework.Vector3;

namespace Nocubeless
{
	static class CubeWorldHelper
	{
		public static CubeCoordinates GetTargetedCube(this CubeWorld cubeWorld, PlayingCamera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
		{
			Vector3 checkPosition = camera.ScreenPosition * cubeWorld.GetGraphicsCubeRatio(); // Not a beautiful way!

			CubeCoordinates actualPosition = null;
			CubeCoordinates convertedCheckPosition;

			const int checkIntensity = 100;
			float checkIncrement = (float)maxLayingDistance / checkIntensity;

			for (int i = 0; i < checkIntensity; i++)
			{ // in World, is free space
				checkPosition += camera.Front * checkIncrement; // increment check zone
				convertedCheckPosition = CubeCoordinates.FromVector3(checkPosition);

				if (convertedCheckPosition == actualPosition)
				{
					if (!cubeWorld.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
						return convertedCheckPosition;
				}

				actualPosition = convertedCheckPosition;
			}

			return actualPosition;
		}
		public static CubeCoordinates GetTargetedNewCube(this CubeWorld cubeWorld, PlayingCamera camera, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
		{
			Vector3 checkPosition = camera.ScreenPosition * cubeWorld.GetGraphicsCubeRatio();

			CubeCoordinates oldPosition = null;
			CubeCoordinates actualPosition = null;
			CubeCoordinates convertedCheckPosition;

			const int checkIntensity = 100;
			float checkIncrement = (float)maxLayingDistance / checkIntensity;

			for (int i = 0; i < checkIntensity; i++)
			{ // in World, is free space
				checkPosition += camera.Front * checkIncrement; // increment check zone
				convertedCheckPosition = CubeCoordinates.FromVector3(checkPosition);

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


		public static Vector3 GetGraphicsCubePosition(this CubeWorld cubeWorld, CubeCoordinates cubePosition) // cube position in graphics representation.
		{
			return cubePosition.ToVector3() / cubeWorld.GetGraphicsCubeRatio();
		}

		public static CubeCoordinates GetCoordinatesFromGraphics(this CubeWorld cubeWorld, Vector3 position)
		{
			return new CubeCoordinates(position * cubeWorld.GetGraphicsCubeRatio());
		}
		public static float GetGraphicsCubeRatio(this CubeWorld cubeWorld) // how much is a cube smaller/bigger in the graphics representation?
		{
			return 1.0f / (cubeWorld.Settings.HeightOfCubes * 2.0f);
		}

		public static Vector3 ToGraphicsCoordinates(this CubeWorld cubeWorld, WorldCoordinates worldPosition)
		{
			return worldPosition / cubeWorld.GetGraphicsCubeRatio();
		}
	}
}
