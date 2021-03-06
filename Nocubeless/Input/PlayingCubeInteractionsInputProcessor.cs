﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayingCubeInteractionsInputProcessor : InputProcessor
	{
		private bool shouldLayCube = true;

		public PlayingCubeInteractionsInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			Nocubeless.Player.NextColorToLay = new CubeColor(7, 7, 7); // TODO: Manage
		}

		public override void Process()
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.ToggleLayBreak))
			{
				shouldLayCube = !shouldLayCube;
			}

			if (Input.WasMiddleMouseButtonJustPressed())
			{
				Pick();
			}

			if (shouldLayCube)
			{
				CubeCoordinates cubeToPreviewPosition = Nocubeless.CubeWorld.GetTargetedNewCube((PlayingCamera)Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
				Cube cubeToLay = new Cube(Nocubeless.Player.NextColorToLay, cubeToPreviewPosition);

				if (!AreColliding(Nocubeless.Player, cubeToLay))
				{
					Nocubeless.CubeWorld.PreviewCube(cubeToLay);

					if (Input.WasRightMouseButtonJustPressed())
					{
						Nocubeless.CubeWorld.LayCube(cubeToLay);
					}
				}
				else
				{
					Nocubeless.CubeWorld.PreviewCube(null);
				}
			}
			else
			{ // break
				Nocubeless.CubeWorld.PreviewCube(null);

				if (Input.WasLeftMouseButtonJustPressed())
				{
					CubeCoordinates cubeToBreakPosition = Nocubeless.CubeWorld.GetTargetedCube((PlayingCamera)Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
					Nocubeless.CubeWorld.BreakCube(cubeToBreakPosition);
				}
			}
		}

		public void Pick()
		{
			// TODO: Put in  separated another class
			var targetCubeCoordinates = Nocubeless.CubeWorld.GetTargetedCube((PlayingCamera)Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
			var targetCubeColor = Nocubeless.CubeWorld.GetCubeColorAt(targetCubeCoordinates);

			if (targetCubeColor != null)
				Nocubeless.Player.NextColorToLay = targetCubeColor;
		}

		// TODO move to another class
		private bool AreColliding(Player player, Cube cube)
		{
			const float cubeSize = 0.1f;
			var cubeMiddlePoint = new Vector3(cube.Coordinates.X + cubeSize / 2, cube.Coordinates.Y + cubeSize / 2, cube.Coordinates.Z + cubeSize / 2);
			var playerMiddlePoint = new Vector3(player.WorldPosition.X + player.Width / 2, player.WorldPosition.Y + player.Height / 2, player.WorldPosition.Z + player.Length / 2);
			Vector3 gap = playerMiddlePoint - cubeMiddlePoint;
			gap.X = Math.Abs(gap.X);
			gap.Y = Math.Abs(gap.Y);
			gap.Z = Math.Abs(gap.Z);

			return gap.X <= (player.Width + cubeSize) / 2
				&& gap.Y <= (player.Height + cubeSize) / 2
				&& gap.Z <= (player.Length + cubeSize) / 2;
		}
	}
}
