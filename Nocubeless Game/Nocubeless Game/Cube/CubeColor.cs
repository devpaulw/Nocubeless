using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public struct CubeColor : IEquatable<CubeColor>
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public CubeColor(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Vector3 ToVector3()
        {
            float r = Red / 7.0f,
                g = Green / 7.0f,
                b = Blue / 7.0f;

            return new Vector3(r, g, b);
        }

        #region Object
        public override bool Equals(object obj)
        {
            if (!(obj is CubeColor))
                return false;

            var other = (CubeColor)obj;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Red.GetHashCode() + Green.GetHashCode() + Blue.GetHashCode();
        }

        public static bool operator ==(CubeColor left, CubeColor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CubeColor left, CubeColor right)
        {
            return !(left == right);
        }

        public bool Equals(CubeColor other)
        {
            return Red == other.Red &&
                Green == other.Green &&
                Blue == other.Blue;
        }

        public override string ToString()
        {
            return "{R:" + Red + " G:" + Green + " B:" + Blue + "}";
        }
        #endregion
    }
}
