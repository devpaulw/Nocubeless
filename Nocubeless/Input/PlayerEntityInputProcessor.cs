﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class PlayerEntityInputProcessor : InputProcessor
	{
		public PlayerEntityInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
		}

		public override void Process()
		{
			if (Input.WasJustPressed(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.RunningSpeed;
			}
			else if (Input.WasJustReleased(Nocubeless.Settings.Keys.Run))
			{
				Nocubeless.Player.Speed = Nocubeless.Player.Settings.WalkingSpeed;
			}

			var direction = Vector3.Zero;
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
			{
				direction = ((PlayingCamera)Nocubeless.Camera).Right; // SDNMSG: You see, I'm often using this because I know in this case it will always be the Playing Camera
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction = Vector3.Zero;
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
			{
				direction = -((PlayingCamera)Nocubeless.Camera).Right;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction = Vector3.Zero;
				}
			}

			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveForward))
			{
				direction += (Nocubeless.Camera as PlayingCamera).Front; // SDNMSG: This is probably a better convention, right? If you have other ideas to handle the Hybrid Camera system in Nocubeless, share it

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction -= ((PlayingCamera)Nocubeless.Camera).Front;
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
			{
				direction -= ((PlayingCamera)Nocubeless.Camera).Front;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction += ((PlayingCamera)Nocubeless.Camera).Front;
				}
			}

			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
			{
				direction += Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction -= Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed; // SDNMSG: Bug reported, it's faster/slower varying with many PCs
				}
			}
			if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveDown))
			{
				direction -= Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;
				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(direction).ToCubeCoordinates()))
				{
					direction += Nocubeless.Camera.Up * Nocubeless.Player.Settings.FlyingSpeed;
				}
			}

			if (!IsTargetingCubeIntersection(direction))
			{
				Nocubeless.Player.Move(direction);
				Nocubeless.Camera.ScreenPosition = Nocubeless.CubeWorld.ToGraphicsCoordinates(Nocubeless.Player.WorldPosition);
			}
		}

		// TMP move to another place
		private bool IsTargetingCubeIntersection(Vector3 direction)
		{
			// prevent the user from passing through two diagonal blocks
			// horizontal verification
			Vector3 directionTmp = direction;
			directionTmp.X = 0;

			if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(directionTmp).ToCubeCoordinates()))
			{
				directionTmp.X = direction.X;
				directionTmp.Z = 0;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(directionTmp).ToCubeCoordinates()))
				{
					return true;
				}
			}

			// vertical verification
			directionTmp.X = direction.X;
			directionTmp.Y = 0;
			directionTmp.Z = direction.Z;

			if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(directionTmp).ToCubeCoordinates()))
			{
				directionTmp.X = 0;
				directionTmp.Y = direction.Y;
				directionTmp.Z = 0;

				if (!Nocubeless.CubeWorld.IsFreeSpace(Nocubeless.Player.GetNextWorldPositionTowards(directionTmp).ToCubeCoordinates()))
				{
					return true;
				}
			}

			return false;
		}
	}
}
