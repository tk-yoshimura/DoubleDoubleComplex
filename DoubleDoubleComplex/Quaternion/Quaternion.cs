using DoubleDouble;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Quaternion : IFormattable {
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public (ddouble x, ddouble y, ddouble z) Vector => (I, J, K);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion Zero { get; } = ddouble.Zero;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion NaN { get; } = ddouble.NaN;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion One { get; } = (1, 0, 0, 0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion IOne { get; } = (0, 1, 0, 0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion JOne { get; } = (0, 0, 1, 0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Quaternion KOne { get; } = (0, 0, 0, 1);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Quaternion Conj => Conjugate(this);

        public static Quaternion Conjugate(Quaternion q) => new(q.R, -q.I, -q.J, -q.K);

        public static Quaternion Normal(Quaternion q) => q / q.Norm;

        public static Quaternion VectorPart(Quaternion q) {
            return new Quaternion(ddouble.Zero, q.I, q.J, q.K);
        }

        public static Quaternion RealPart(Quaternion q) {
            return new Quaternion(q.R, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

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

        public static int ILogB(Quaternion q) {
            return int.Max(
                int.Max(ddouble.ILogB(q.R), ddouble.ILogB(q.I)),
                int.Max(ddouble.ILogB(q.J), ddouble.ILogB(q.K))
            );
        }

        public static Quaternion Ldexp(Quaternion q, int n) {
            return (ddouble.Ldexp(q.R, n), ddouble.Ldexp(q.I, n), ddouble.Ldexp(q.J, n), ddouble.Ldexp(q.K, n));
        }

        public static implicit operator Quaternion(string v) {
            return Parse(v);
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

            StringBuilder str = new();

            if (R != 0d) {
                str.Append($"{R}");
            }
            if (I != 0d) {
                str.Append(I > 0d ? $"+{I}i" : $"{I}i");
            }
            if (J != 0d) {
                str.Append(J > 0d ? $"+{J}j" : $"{J}j");
            }
            if (K != 0d) {
                str.Append(K > 0d ? $"+{K}k" : $"{K}k");
            }

            if (str.Length > 0 && str[0] == '+') {
                str.Remove(0, 1);
            }

            return (str.Length > 0) ? str.ToString() : "0";
        }

        public string ToString([AllowNull] string format, [AllowNull] IFormatProvider formatProvider) {
            if (string.IsNullOrWhiteSpace(format)) {
                return ToString();
            }

            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            StringBuilder str = new();

            if (R != 0d) {
                str.Append($"{R.ToString(format)}");
            }
            if (I != 0d) {
                str.Append(I > 0d ? $"+{I.ToString(format)}i" : $"{I.ToString(format)}i");
            }
            if (J != 0d) {
                str.Append(J > 0d ? $"+{J.ToString(format)}j" : $"{J.ToString(format)}j");
            }
            if (K != 0d) {
                str.Append(K > 0d ? $"+{K.ToString(format)}k" : $"{K.ToString(format)}k");
            }

            if (str.Length > 0 && str[0] == '+') {
                str.Remove(0, 1);
            }

            return (str.Length > 0) ? str.ToString() : "0";
        }

        public string ToString(string format) {
            return ToString(format, null);
        }
    }
}
