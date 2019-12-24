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
        const int UnitMax = 7;

        private int _red, _green, _blue;

        public int Red {
            get {
                return _red;
            }
            set {
                _red = Normalize(value);
            }
        }
        public int Green {
            get {
                return _green;
            }
            set {
                _green = Normalize(value);
            }
        }
        public int Blue {
            get {
                return _blue;
            }
            set {
                _blue = Normalize(value);
            }
        }

        public static CubeColor Empty
            => default;

        public CubeColor(int red, int green, int blue)
        {
            _red = 0; _green = 0; _blue = 0;

            Red = red;
            Green = green;
            Blue = blue;
        }

        public Vector3 ToVector3()
        {
            float r = Red / (float)UnitMax,
                g = Green / (float)UnitMax,
                b = Blue / (float)UnitMax;

            return new Vector3(r, g, b);
        }

        public static int Normalize(int value)
        {
            if (value > UnitMax) return UnitMax;
            else return value;
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
