using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	public class WorldCoordinates : IEquatable<WorldCoordinates>
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
		public static WorldCoordinates Origin { get => new WorldCoordinates(0, 0, 0); }
		public WorldCoordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public WorldCoordinates(Vector3 vector3)
		{
			X = (int)Math.Round(vector3.X);
			Y = (int)Math.Round(vector3.Y);
			Z = (int)Math.Round(vector3.Z);
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, Y, Z);
		}

		// a method Coordinates.CreateFromVector3(Vector3) or Coordinates.From(Vector3) or constructor new Coordinates(Vector3) may be cleaner ?
		// SDNMSG: Exact;
		public static WorldCoordinates FromVector3(Vector3 vector3) 
			=> new WorldCoordinates(vector3);

		#region Formulas
		internal static WorldCoordinates Abs(WorldCoordinates coordinates)
		{
			return new WorldCoordinates(Math.Abs(coordinates.X), Math.Abs(coordinates.Y), Math.Abs(coordinates.Z));
		}

		//  Add and Multiply should be private or public ? 
		// SDNMSG ANSWER: Don't need same function both, I think, private, or EVEN inside operators! ... (but why internal? xD)
		internal static WorldCoordinates Add(WorldCoordinates coordinates1, WorldCoordinates coordinates2)
		{
			return new WorldCoordinates(coordinates1.X + coordinates2.X, coordinates1.Y + coordinates2.Y, coordinates1.Z + coordinates2.Z);
		}

		// THE FUNCTION DOES NOT WORK BECAUSE IT DOESN'T TAKE INTO ACCOUNT THE CUBE RATIO
		public static WorldCoordinates FromTruncated(Vector3 vector3)
		{
			return new WorldCoordinates((int)vector3.X, (int)vector3.Y, (int)vector3.Z);
		}

		internal static WorldCoordinates Subtract(WorldCoordinates coordinates1, WorldCoordinates coordinates2)
		{
			return new WorldCoordinates(coordinates1.X - coordinates2.X, coordinates1.Y - coordinates2.Y, coordinates1.Z - coordinates2.Z);
		}

		internal static WorldCoordinates Multiply(WorldCoordinates coordinates, float scalar)
		{
			return new WorldCoordinates((int)(coordinates.X * scalar), (int)(coordinates.Y * scalar), (int)(coordinates.Z * scalar));
		}
		#endregion

		#region Operators
		public static bool operator ==(WorldCoordinates left, WorldCoordinates right)
		{
			return !(left is null) && 
				left.Equals(right);
		}

		public static bool operator !=(WorldCoordinates left, WorldCoordinates right)
		{
			return !(left == right);
		}

		public static bool operator >(WorldCoordinates left, WorldCoordinates right)
		{
			if (left == null || right == null)
				throw new NullReferenceException();

			return left.X > right.X
				|| left.Y > right.Y
				|| left.Z > right.Z;
		}

		public static bool operator <(WorldCoordinates left, WorldCoordinates right)
		{
			if (left == null || right == null)
				throw new NullReferenceException(); // TODO: must be rather ArgumentNullException

			return left.X < right.X
				|| left.Y < right.Y
				|| left.Z < right.Z;
		}

		public static WorldCoordinates operator *(float scalar, WorldCoordinates coordinates)
		{
			if (coordinates == null)
				throw new NullReferenceException();

			return Multiply(coordinates, scalar);
		}
		public static WorldCoordinates operator *(WorldCoordinates coordinates, float scalar)
		{
			if (coordinates == null)
				throw new NullReferenceException();

			return Multiply(coordinates, scalar);
		}

		public static WorldCoordinates operator +(WorldCoordinates coordinates1, WorldCoordinates coordinates2)
		{
			if (coordinates1 == null || coordinates2 == null)
				throw new NullReferenceException();

			return Add(coordinates1, coordinates2);
		}
		public static WorldCoordinates operator -(WorldCoordinates coordinates1, WorldCoordinates coordinates2)
		{
			if (coordinates1 == null || coordinates2 == null)
				throw new NullReferenceException();

			return Subtract(coordinates1, coordinates2);
		}
		#endregion

		#region Object
		public override bool Equals(object obj)
		{
			return Equals(obj as WorldCoordinates);
		}

		public bool Equals(WorldCoordinates other)
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
