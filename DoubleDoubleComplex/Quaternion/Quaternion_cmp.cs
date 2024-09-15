
using DoubleDouble;
using System;

namespace DoubleDoubleComplex {

    public partial class Quaternion : IEquatable<Quaternion> {

        public static bool IsNaN(Quaternion q)
            => ddouble.IsNaN(q.R) || ddouble.IsNaN(q.I) || ddouble.IsNaN(q.J) || ddouble.IsNaN(q.K);

        public static bool IsFinite(Quaternion q)
            => ddouble.IsFinite(q.R) && ddouble.IsFinite(q.I) && ddouble.IsFinite(q.J) && ddouble.IsFinite(q.K);

        public static bool IsZero(Quaternion q)
            => ddouble.IsZero(q.R) && ddouble.IsZero(q.I) && ddouble.IsZero(q.J) && ddouble.IsZero(q.K);

        public static bool operator ==(Quaternion a, Quaternion b) {
            return a.R == b.R && a.I == b.I && a.J == b.J && a.K == b.K;
        }

        public static bool operator !=(Quaternion a, Quaternion b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            return (obj is not null && obj is Quaternion q) && q == this;
        }
        public bool Equals(Quaternion other) {
            return this == other;
        }

        public override int GetHashCode() {
            return R.GetHashCode() ^ I.GetHashCode();
        }

    }
}
