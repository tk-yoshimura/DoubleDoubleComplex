using DoubleDouble;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Octonion : IFormattable {
        public readonly ddouble R, S, T, U, W, X, Y, Z;

        public ddouble Norm => R * R + S * S + T * T + U * U + W * W + X * X + Y * Y + Z * Z;

        public ddouble Magnitude {
            get {
                if (ddouble.IsInfinity(R) || ddouble.IsInfinity(S) || ddouble.IsInfinity(T) || ddouble.IsInfinity(U) ||
                    ddouble.IsInfinity(W) || ddouble.IsInfinity(X) || ddouble.IsInfinity(Y) || ddouble.IsInfinity(Z)) {

                    return ddouble.PositiveInfinity;
                }

                int exp = ILogB(this);

                if (exp <= int.MinValue) {
                    return 0d;
                }

                Octonion o = Ldexp(this, -exp);

                ddouble m = ddouble.Ldexp(ddouble.Sqrt(
                    o.R * o.R + o.S * o.S + o.T * o.T + o.U * o.U +
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

        public static Octonion Conjugate(Octonion q) => new(q.R, -q.S, -q.T, -q.U, -q.W, -q.X, -q.Y, -q.Z);

        public static Octonion Normal(Octonion q) => q / q.Norm;

        public Octonion(ddouble r, ddouble s, ddouble t, ddouble u, ddouble w, ddouble x, ddouble y, ddouble z) {
            this.R = r;
            this.S = s;
            this.T = t;
            this.U = u;
            this.W = w;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static implicit operator Octonion((ddouble r, ddouble s, ddouble t, ddouble u, ddouble w, ddouble x, ddouble y, ddouble z) v) {
            return new(v.r, v.s, v.t, v.u, v.w, v.x, v.y, v.z);
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

        public static implicit operator (ddouble a, ddouble b, ddouble c, ddouble d, ddouble e, ddouble f, ddouble g, ddouble h)(Octonion v) {
            return (v.R, v.S, v.T, v.U, v.W, v.X, v.Y, v.X);
        }

        public static int ILogB(Octonion o) {
            return int.Max(
                int.Max(
                    int.Max(ddouble.ILogB(o.R), ddouble.ILogB(o.S)),
                    int.Max(ddouble.ILogB(o.T), ddouble.ILogB(o.U))
                ),
                int.Max(
                    int.Max(ddouble.ILogB(o.W), ddouble.ILogB(o.X)),
                    int.Max(ddouble.ILogB(o.Y), ddouble.ILogB(o.Z))
                )
            );
        }

        public static Octonion Ldexp(Octonion o, int n) {
            return (
                ddouble.Ldexp(o.R, n), ddouble.Ldexp(o.S, n), ddouble.Ldexp(o.T, n), ddouble.Ldexp(o.U, n),
                ddouble.Ldexp(o.W, n), ddouble.Ldexp(o.X, n), ddouble.Ldexp(o.Y, n), ddouble.Ldexp(o.Z, n)
            );
        }

        public static implicit operator Octonion(string v) {
            return Parse(v);
        }

        public void Deconstruct(out ddouble a, out ddouble b, out ddouble c, out ddouble d, out ddouble e, out ddouble f, out ddouble g, out ddouble h)
            => (a, b, c, d, e, f, g, h) = (R, S, T, U, W, X, Y, Z);

        public override string ToString() {
            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            StringBuilder str = new();

            if (R != 0d) {
                str.Append($"{R}");
            }
            if (S != 0d) {
                str.Append(S > 0d ? $"+{S}s" : $"{S}s");
            }
            if (T != 0d) {
                str.Append(T > 0d ? $"+{T}t" : $"{T}t");
            }
            if (U != 0d) {
                str.Append(U > 0d ? $"+{U}u" : $"{U}u");
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
            if (S != 0d) {
                str.Append(S > 0d ? $"+{S.ToString(format)}s" : $"{S.ToString(format)}s");
            }
            if (T != 0d) {
                str.Append(T > 0d ? $"+{T.ToString(format)}t" : $"{T.ToString(format)}t");
            }
            if (U != 0d) {
                str.Append(U > 0d ? $"+{U.ToString(format)}u" : $"{U.ToString(format)}u");
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
