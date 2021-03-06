﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class EditingCameraInputProcessor : InputProcessor
	{
		Vector3 rotateAround = Vector3.Zero;

		public EditingCameraInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}

		public override void Process()
		{
			if (Input.WasMiddleMouseButtonJustPressed())
			{
				rotateAround = Nocubeless.Camera.Target;
			}

			if (Input.CurrentMouseState.MiddleButton == ButtonState.Pressed)
			{
				const float cameraRotationRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.OldMouseState.Y,
					deltaX = Input.OldMouseState.X - Input.CurrentMouseState.X;

				((EditingCamera)Nocubeless.Camera).RotateAround(
					cameraRotationRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity,
					cameraRotationRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity,
					rotateAround);
			}

			if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
			{
				const float cameraMoveRatio = 1f / 57f;

				int deltaY = Input.CurrentMouseState.Y - Input.OldMouseState.Y,
					deltaX = Input.OldMouseState.X - Input.CurrentMouseState.X;

				(Nocubeless.Camera as EditingCamera).Move(
					cameraMoveRatio * deltaX * Nocubeless.Settings.Camera.DefaultSensitivity * Nocubeless.Settings.EditingMode.MoveSpeed, // TODO: Separate both of these
					cameraMoveRatio * deltaY * Nocubeless.Settings.Camera.DefaultSensitivity * Nocubeless.Settings.EditingMode.MoveSpeed);
			}

			float scrollWheelMovement = Input.GetScrollWheelMovement();
			if (scrollWheelMovement != 0)
			{
				scrollWheelMovement /= Math.Abs(scrollWheelMovement);
				scrollWheelMovement /= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
				scrollWheelMovement *= Nocubeless.Settings.EditingMode.ScrollSpeed;

				(Nocubeless.Camera as EditingCamera).Zoom(scrollWheelMovement);
			}
		}
	}
}
