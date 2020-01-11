using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	public class CubeCoordinates : IEquatable<CubeCoordinates>
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
		public CubeCoordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public CubeCoordinates(Vector3 vector3)
		{
			X = (int)Math.Round(vector3.X);
			Y = (int)Math.Round(vector3.Y);
			Z = (int)Math.Round(vector3.Z);
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, Y, Z);
		}

		public static CubeCoordinates FromVector3(Vector3 vector3)
			=> new CubeCoordinates(vector3);

		#region Formulas
		internal static CubeCoordinates Abs(CubeCoordinates coordinates)
		{
			return new CubeCoordinates(Math.Abs(coordinates.X), Math.Abs(coordinates.Y), Math.Abs(coordinates.Z));
		}

		public static CubeCoordinates Add(CubeCoordinates coordinates1, CubeCoordinates coordinates2)
		{
			if (coordinates1 == null || coordinates2 == null)
				throw new NullReferenceException();

			return new CubeCoordinates(coordinates1.X + coordinates2.X, coordinates1.Y + coordinates2.Y, coordinates1.Z + coordinates2.Z);
		}

		public static CubeCoordinates Subtract(CubeCoordinates coordinates1, CubeCoordinates coordinates2)
		{
			if (coordinates1 == null || coordinates2 == null)
				throw new NullReferenceException();

			return new CubeCoordinates(coordinates1.X - coordinates2.X, coordinates1.Y - coordinates2.Y, coordinates1.Z - coordinates2.Z);
		}

		public static CubeCoordinates Multiply(CubeCoordinates coordinates, float scalar)
		{
			if (coordinates == null)
				throw new NullReferenceException();

			return new CubeCoordinates((int)(coordinates.X * scalar), (int)(coordinates.Y * scalar), (int)(coordinates.Z * scalar));
		}
		#endregion

		#region Operators
		public static bool operator ==(CubeCoordinates left, CubeCoordinates right)
		{
			return !(left is null) &&
				left.Equals(right);
		}

		public static bool operator !=(CubeCoordinates left, CubeCoordinates right)
		{
			return !(left == right);
		}

		public static bool operator >(CubeCoordinates left, CubeCoordinates right)
		{
			if (left == null || right == null)
				throw new NullReferenceException();

			return left.X > right.X
				|| left.Y > right.Y
				|| left.Z > right.Z;
		}

		public static bool operator <(CubeCoordinates left, CubeCoordinates right)
		{
			if (left == null || right == null)
				throw new NullReferenceException(); // TODO: must be rather ArgumentNullException

			return left.X < right.X
				|| left.Y < right.Y
				|| left.Z < right.Z;
		}

		public static CubeCoordinates operator *(float scalar, CubeCoordinates coordinates)
		{
			return Multiply(coordinates, scalar);
		}

		public static CubeCoordinates FromTruncated(Vector3 vector3)
		{
			// TODO can remove Math.floor
			return new CubeCoordinates((int)Math.Floor(vector3.X), (int)Math.Floor(vector3.Y), (int)Math.Floor(vector3.Z));
		}

		public static CubeCoordinates operator *(CubeCoordinates coordinates, float scalar)
		{
			return Multiply(coordinates, scalar);
		}

		public static CubeCoordinates operator +(CubeCoordinates coordinates1, CubeCoordinates coordinates2)
		{
			return Add(coordinates1, coordinates2);
		}
		public static CubeCoordinates operator -(CubeCoordinates coordinates1, CubeCoordinates coordinates2)
		{
			return Subtract(coordinates1, coordinates2);
		}
		#endregion

		#region Object
		public override bool Equals(object obj)
		{
			return Equals(obj as CubeCoordinates);
		}

		public bool Equals(CubeCoordinates other)
		{
			return !(other is null) &&
				X == other.X &&
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
		#endregion
	}
}
