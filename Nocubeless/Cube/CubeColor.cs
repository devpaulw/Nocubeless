using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class CubeColor : IEquatable<CubeColor>
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

        public CubeColor(int red, int green, int blue)
        {
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

        public override bool Equals(object obj)
        {
            return Equals(obj as CubeColor);
        }

        public bool Equals(CubeColor other)
        {
            return other != null &&
                   _red == other._red &&
                   _green == other._green &&
                   _blue == other._blue;
        }

        public override int GetHashCode()
        {
            var hashCode = -1558296783;
            hashCode = hashCode * -1521134295 + _red.GetHashCode();
            hashCode = hashCode * -1521134295 + _green.GetHashCode();
            hashCode = hashCode * -1521134295 + _blue.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "{R:" + Red + " G:" + Green + " B:" + Blue + "}";
        }

        //public static bool operator ==(CubeColor left, CubeColor right)
        //{
        //    if (ReferenceEquals(left, null))
        //        return false;
        //    if (ReferenceEquals(right, null))
        //        return false;
        //    if (ReferenceEquals(left, right))
        //        return true;

        //    return left.Equals(right);
        //}

        //public static bool operator !=(CubeColor left, CubeColor right)
        //{
        //    return !(left == right);
        //}
    }
}
