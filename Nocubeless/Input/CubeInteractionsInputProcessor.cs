using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class CubeInteractionsInputProcessor : NocubelessComponent
	{
		private bool shouldLayCube = true;

		public CubeInteractionsInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.SwitchLayBreak))
			{
				shouldLayCube = !shouldLayCube;
			}

			if (Input.WasMiddleMouseButtonJustPressed())
			{
				Pick();
			}

			if (shouldLayCube)
			{
				CubeCoordinates cubeToPreviewPosition = Nocubeless.CubeWorld.GetTargetedNewCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
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
					CubeCoordinates cubeToBreakPosition = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
					Nocubeless.CubeWorld.BreakCube(cubeToBreakPosition);
				}
			}

			base.Update(gameTime);
		}

		public void Pick()
		{
			// TODO: Put in  separated another class
			var targetCubeCoordinates = Nocubeless.CubeWorld.GetTargetedCube(Nocubeless.Camera, Nocubeless.Settings.CubeHandler.MaxLayingDistance);
			var targetCubeColor = Nocubeless.CubeWorld.GetCubeColorAt(targetCubeCoordinates);

			if (targetCubeColor != null)
				Nocubeless.Player.NextColorToLay = targetCubeColor;
		}

		// TODO move to another class
		private bool AreColliding(Player player, Cube cube)
		{
			const float cubeSize = 0.1f;
			var cubeMiddlePoint = new Vector3(cube.Coordinates.X + cubeSize / 2, cube.Coordinates.Y + cubeSize / 2, cube.Coordinates.Z + cubeSize / 2) / Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			var middlePoint = new Vector3(player.ScreenCoordinates.X + player.Width / 2, player.ScreenCoordinates.Y + player.Height / 2, player.ScreenCoordinates.Z + player.Length / 2);
			Vector3 gap = middlePoint - cubeMiddlePoint;
			gap.X = Math.Abs(gap.X);
			gap.Y = Math.Abs(gap.Y);
			gap.Z = Math.Abs(gap.Z);

			return gap.X <= (player.Width + cubeSize) / 2
				&& gap.Y <= (player.Height + cubeSize) / 2
				&& gap.Z <= (player.Length + cubeSize) / 2;
		}
	}
}
