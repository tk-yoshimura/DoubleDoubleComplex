
using DoubleDouble;
using System;

namespace DoubleDoubleComplex {

    public partial class Octonion : IEquatable<Octonion> {

        public static bool IsNaN(Octonion o)
            => ddouble.IsNaN(o.R) || ddouble.IsNaN(o.I) || ddouble.IsNaN(o.J) || ddouble.IsNaN(o.K) ||
               ddouble.IsNaN(o.W) || ddouble.IsNaN(o.X) || ddouble.IsNaN(o.Y) || ddouble.IsNaN(o.Z);

        public static bool IsFinite(Octonion o)
            => ddouble.IsFinite(o.R) && ddouble.IsFinite(o.I) && ddouble.IsFinite(o.J) && ddouble.IsFinite(o.K) &&
               ddouble.IsFinite(o.W) && ddouble.IsFinite(o.X) && ddouble.IsFinite(o.Y) && ddouble.IsFinite(o.Z);

        public static bool IsZero(Octonion o)
            => ddouble.IsZero(o.R) && ddouble.IsZero(o.I) && ddouble.IsZero(o.J) && ddouble.IsZero(o.K) &&
               ddouble.IsZero(o.W) && ddouble.IsZero(o.X) && ddouble.IsZero(o.Y) && ddouble.IsZero(o.Z);

        public static bool operator ==(Octonion a, Octonion b) {
            return a.R == b.R && a.I == b.I && a.J == b.J && a.K == b.K && a.W == b.W && a.X == b.X && a.Y == b.Y && a.Z == b.Z;
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
            return R.GetHashCode() ^ I.GetHashCode();
        }

    }
}
