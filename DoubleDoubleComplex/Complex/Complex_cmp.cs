
using DoubleDouble;
using System;

namespace DoubleDoubleComplex {

    public partial class Complex : IEquatable<Complex> {

        public static bool IsNaN(Complex c) => ddouble.IsNaN(c.R) || ddouble.IsNaN(c.I);

        public static bool IsFinite(Complex c) => ddouble.IsFinite(c.R) && ddouble.IsFinite(c.I);

        public static bool IsZero(Complex c) => ddouble.IsZero(c.R) && ddouble.IsZero(c.I);

        public static bool operator ==(Complex a, Complex b) {
            return a.R == b.R && a.I == b.I;
        }

        public static bool operator !=(Complex a, Complex b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            return (obj is not null && obj is Complex c) && c == this;
        }

        public bool Equals(Complex other) {
            return this == other;
        }

        public override int GetHashCode() {
            return R.GetHashCode() ^ I.GetHashCode();
        }

    }
}
