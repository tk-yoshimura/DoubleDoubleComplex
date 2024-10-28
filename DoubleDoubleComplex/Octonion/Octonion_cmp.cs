
using DoubleDouble;
using System;

namespace DoubleDoubleComplex {

    public partial class Octonion : IEquatable<Octonion> {

        public static bool IsNaN(Octonion o)
            => ddouble.IsNaN(o.R) || ddouble.IsNaN(o.S) || ddouble.IsNaN(o.T) || ddouble.IsNaN(o.U) ||
               ddouble.IsNaN(o.W) || ddouble.IsNaN(o.X) || ddouble.IsNaN(o.Y) || ddouble.IsNaN(o.Z);

        public static bool IsFinite(Octonion o)
            => ddouble.IsFinite(o.R) && ddouble.IsFinite(o.S) && ddouble.IsFinite(o.T) && ddouble.IsFinite(o.U) &&
               ddouble.IsFinite(o.W) && ddouble.IsFinite(o.X) && ddouble.IsFinite(o.Y) && ddouble.IsFinite(o.Z);

        public static bool IsZero(Octonion o)
            => ddouble.IsZero(o.R) && ddouble.IsZero(o.S) && ddouble.IsZero(o.T) && ddouble.IsZero(o.U) &&
               ddouble.IsZero(o.W) && ddouble.IsZero(o.X) && ddouble.IsZero(o.Y) && ddouble.IsZero(o.Z);

        public static bool operator ==(Octonion a, Octonion b) {
            return a.R == b.R && a.S == b.S && a.T == b.T && a.U == b.U && a.W == b.W && a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Octonion a, Octonion b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            return (obj is not null && obj is Octonion q) && q == this;
        }
        public bool Equals(Octonion other) {
            return this == other;
        }

        public override int GetHashCode() {
            return R.GetHashCode() ^ S.GetHashCode();
        }

    }
}
