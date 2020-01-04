﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	public class Coordinates : IEquatable<Coordinates> // TODO: Fix equal and use the true operator.
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
		public static Coordinates Zero { get => new Coordinates(0, 0, 0); }

		public Coordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, Y, Z);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Coordinates);
		}

		public bool Equals(Coordinates other)
		{
			// BBMSG i added contracts to avoid some Roslyn warnings
			Contract.Requires(other != null);
			return X == other.X &&
				Y == other.Y &&
				Z == other.Z;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
		}

		public override string ToString()
		{
			return "{X:" + X + "; Y:" + Y + "; Z:" + Z + "}";
		}

		public static bool operator >(Coordinates left, Coordinates right)
		{
			Contract.Requires(left != null && right != null);
			return left.X > right.X
				|| left.Y > right.Y
				|| left.Z > right.Z;
		}
		public static bool operator <(Coordinates left, Coordinates right)
		{
			Contract.Requires(left != null && right != null);
			return left.X < right.X
				|| left.Y < right.Y
				|| left.Z < right.Z;
		}

		// BBMSG Add and Multiply should be private or public ?
		public static Coordinates operator *(float scalar, Coordinates coordinates)
		{
			return Multiply(coordinates, scalar);
		}
		public static Coordinates operator *(Coordinates coordinates, float scalar)
		{
			return Multiply(coordinates, scalar);
		}
		public static Coordinates Multiply(Coordinates coordinates, float scalar)
		{
			Contract.Requires(coordinates != null);
			return new Coordinates((int)(coordinates.X * scalar), (int)(coordinates.Y * scalar), (int)(coordinates.Z * scalar));
		}

		public static Coordinates operator +(Coordinates coordinates1, Coordinates coordinates2)
		{
			return Add(coordinates1, coordinates2);
		}
		public static Coordinates Add(Coordinates coordinates1, Coordinates coordinates2)
		{
			Contract.Requires(coordinates1 != null && coordinates2 != null);
			return new Coordinates(coordinates1.X + coordinates2.X, coordinates1.Y + coordinates2.Y, coordinates1.Z + coordinates2.Z);
		}

		public static Coordinates FromTruncated(Vector3 vector3)
		{
			return new Coordinates((int)vector3.X, (int)vector3.Y, (int)vector3.Z);
		}
	}

	internal static class CoordinateExtension
	{
		// BBMSG a method Coordinates.CreateFromVector3(Vector3) or Coordinates.From(Vector3) or constructor new Coordinates(Vector3) may be cleaner ?
		public static Coordinates ToCubeCoordinate(this Vector3 position) // TODO: This is not clean, put it in the real class.
		{
			return new Coordinates((int)Math.Round(position.X), (int)Math.Round(position.Y), (int)Math.Round(position.Z));
		}
	}
}
