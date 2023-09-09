using DoubleDouble;
using System.Diagnostics;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Quaternion {
        public readonly ddouble R, I, J, K;

        public ddouble Norm => R * R + I * I + J * J + K * K;

        public ddouble Magnitude { 
            get {
                if (ddouble.IsInfinity(R) || ddouble.IsInfinity(I) || ddouble.IsInfinity(J) || ddouble.IsInfinity(K)) {
                    return ddouble.PositiveInfinity;
                }

                (int scale, (ddouble r, ddouble i, ddouble j, ddouble k)) = ddouble.AdjustScale(0, (R, I, J, K));

                ddouble w = ddouble.Ldexp(ddouble.Sqrt(r * r + i * i + j * j + k * k), -scale);

                return w;
            }
        }

        public (ddouble x, ddouble y, ddouble z) Vector => (I, J, K);

        public static Quaternion Zero { get; } = ddouble.Zero;

        public static Quaternion NaN { get; } = ddouble.NaN;

        public static Quaternion One { get; } = (1, 0, 0, 0);
        public static Quaternion IOne { get; } = (0, 1, 0, 0);
        public static Quaternion JOne { get; } = (0, 0, 1, 0);
        public static Quaternion KOne { get; } = (0, 0, 0, 1);

        public static Quaternion Conjugate(Quaternion q) => new(q.R, -q.I, -q.J, -q.K);

        public static Quaternion Normal(Quaternion q) => q / q.Norm;

        public Quaternion(ddouble r, ddouble i, ddouble j, ddouble k) {
            this.R = r;
            this.I = i;
            this.J = j;
            this.K = k;
        }

        public static implicit operator Quaternion((ddouble r, ddouble i, ddouble j, ddouble k) v) {
            return new(v.r, v.i, v.j, v.k);
        }

        public static implicit operator Quaternion(int v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion(double v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion(ddouble v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion(Complex z) {
            return new(z.R, z.I, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion((ddouble x, ddouble y, ddouble z) v) {
            return new(ddouble.Zero, v.x, v.y, v.z);
        }

        public static implicit operator (ddouble r, ddouble i, ddouble j, ddouble k)(Quaternion v) {
            return (v.R, v.I, v.J, v.K);
        }

        public static implicit operator Quaternion(System.Numerics.Quaternion q) {
            return new Quaternion(q.W, q.X, q.Y, q.Z);
        }

        public static explicit operator System.Numerics.Quaternion(Quaternion q) {
            return new System.Numerics.Quaternion((float)q.I, (float)q.J, (float)q.K, (float)q.R);
        }

        public void Deconstruct(out ddouble r, out ddouble i, out ddouble j, out ddouble k)
            => (r, i, j, k) = (R, I, J, K);

        public override string ToString() {
            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            string str = string.Empty;

            str += R == 0 ? string.Empty : $"{R}";
            str += I == 0 ? string.Empty : I > 0 ? $"+{I}i" : $"{I}i";
            str += J == 0 ? string.Empty : J > 0 ? $"+{J}j" : $"{J}j";
            str += K == 0 ? string.Empty : K > 0 ? $"+{K}k" : $"{K}k";

            str = str.TrimStart('+');

            return (str.Length > 0) ? str : "0";
        }
    }
}
