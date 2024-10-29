using DoubleDouble;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Octonion : IFormattable {
        public readonly ddouble R, I, J, K, W, X, Y, Z;

        public ddouble Norm => R * R + I * I + J * J + K * K + W * W + X * X + Y * Y + Z * Z;

        public ddouble Magnitude {
            get {
                if (ddouble.IsInfinity(R) || ddouble.IsInfinity(I) || ddouble.IsInfinity(J) || ddouble.IsInfinity(K) ||
                    ddouble.IsInfinity(W) || ddouble.IsInfinity(X) || ddouble.IsInfinity(Y) || ddouble.IsInfinity(Z)) {

                    return ddouble.PositiveInfinity;
                }

                int exp = ILogB(this);

                if (exp <= int.MinValue) {
                    return 0d;
                }

                Octonion o = Ldexp(this, -exp);

                ddouble m = ddouble.Ldexp(ddouble.Sqrt(
                    o.R * o.R + o.I * o.I + o.J * o.J + o.K * o.K +
                    o.W * o.W + o.X * o.X + o.Y * o.Y + o.Z * o.Z), exp
                );

                return m;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Octonion Zero { get; } = ddouble.Zero;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Octonion NaN { get; } = ddouble.NaN;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Octonion One { get; } = (1, 0, 0, 0, 0, 0, 0, 0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Octonion Conj => Conjugate(this);

        public static Octonion Conjugate(Octonion q) => new(q.R, -q.I, -q.J, -q.K, -q.W, -q.X, -q.Y, -q.Z);

        public static Octonion Normal(Octonion q) => q / q.Norm;

        public Octonion(ddouble r, ddouble i, ddouble j, ddouble k, ddouble w, ddouble x, ddouble y, ddouble z) {
            this.R = r;
            this.I = i;
            this.J = j;
            this.K = k;
            this.W = w;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static implicit operator Octonion((ddouble r, ddouble i, ddouble j, ddouble k, ddouble w, ddouble x, ddouble y, ddouble z) v) {
            return new(v.r, v.i, v.j, v.k, v.w, v.x, v.y, v.z);
        }

        public static implicit operator Octonion(int v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Octonion(double v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Octonion(ddouble v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Octonion(Complex z) {
            return new(z.R, z.I, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Octonion(Quaternion q) {
            return new(q.R, q.I, q.J, q.K, ddouble.Zero, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator (ddouble r, ddouble i, ddouble j, ddouble k, ddouble w, ddouble x, ddouble y, ddouble z)(Octonion v) {
            return (v.R, v.I, v.J, v.K, v.W, v.X, v.Y, v.X);
        }

        public static int ILogB(Octonion o) {
            return int.Max(
                int.Max(
                    int.Max(ddouble.ILogB(o.R), ddouble.ILogB(o.I)),
                    int.Max(ddouble.ILogB(o.J), ddouble.ILogB(o.K))
                ),
                int.Max(
                    int.Max(ddouble.ILogB(o.W), ddouble.ILogB(o.X)),
                    int.Max(ddouble.ILogB(o.Y), ddouble.ILogB(o.Z))
                )
            );
        }

        public static Octonion Ldexp(Octonion o, int n) {
            return (
                ddouble.Ldexp(o.R, n), ddouble.Ldexp(o.I, n), ddouble.Ldexp(o.J, n), ddouble.Ldexp(o.K, n),
                ddouble.Ldexp(o.W, n), ddouble.Ldexp(o.X, n), ddouble.Ldexp(o.Y, n), ddouble.Ldexp(o.Z, n)
            );
        }

        public static implicit operator Octonion(string v) {
            return Parse(v);
        }

        public void Deconstruct(out ddouble r, out ddouble i, out ddouble j, out ddouble k, out ddouble w, out ddouble x, out ddouble y, out ddouble z)
            => (r, i, j, k, w, x, y, z) = (R, I, J, K, W, X, Y, Z);

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
            if (W != 0d) {
                str.Append(W > 0d ? $"+{W}w" : $"{W}w");
            }
            if (X != 0d) {
                str.Append(X > 0d ? $"+{X}x" : $"{X}x");
            }
            if (Y != 0d) {
                str.Append(Y > 0d ? $"+{Y}y" : $"{Y}y");
            }
            if (Z != 0d) {
                str.Append(Z > 0d ? $"+{Z}z" : $"{Z}z");
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
            if (W != 0d) {
                str.Append(W > 0d ? $"+{W.ToString(format)}w" : $"{W.ToString(format)}w");
            }
            if (X != 0d) {
                str.Append(X > 0d ? $"+{X.ToString(format)}x" : $"{X.ToString(format)}x");
            }
            if (Y != 0d) {
                str.Append(Y > 0d ? $"+{Y.ToString(format)}y" : $"{Y.ToString(format)}y");
            }
            if (Z != 0d) {
                str.Append(Z > 0d ? $"+{Z.ToString(format)}z" : $"{Z.ToString(format)}z");
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
