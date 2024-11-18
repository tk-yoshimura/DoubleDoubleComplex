using DoubleDouble;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Complex : IFormattable {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const char ImaginaryUnit = 'i';

        public readonly ddouble R, I;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble SquareNorm => R * R + I * I;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Norm => ddouble.Hypot(R, I);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Magnitude => Norm;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Phase => ddouble.Atan2(I, R);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Complex Zero { get; } = ddouble.Zero;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Complex NaN { get; } = ddouble.NaN;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Complex One { get; } = (1, 0);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Complex ImaginaryOne { get; } = (0, 1);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Conj => Conjugate(this);

        public static Complex Conjugate(Complex c) => new(c.R, -c.I);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Normal => this / Norm;

        public Complex(ddouble r, ddouble i) {
            this.R = r;
            this.I = i;
        }

        public static implicit operator Complex((ddouble r, ddouble i) v) {
            return new(v.r, v.i);
        }

        public static implicit operator Complex(int v) {
            return new(v, ddouble.Zero);
        }

        public static implicit operator Complex(double v) {
            return new(v, ddouble.Zero);
        }

        public static implicit operator Complex(ddouble v) {
            return new(v, ddouble.Zero);
        }

        public static implicit operator Complex(string v) {
            return Parse(v);
        }

        public static implicit operator (ddouble r, ddouble i)(Complex v) {
            return (v.R, v.I);
        }

        public static implicit operator Complex(System.Numerics.Complex c) {
            return new Complex(c.Real, c.Imaginary);
        }

        public static explicit operator System.Numerics.Complex(Complex c) {
            return new System.Numerics.Complex((double)c.R, (double)c.I);
        }

        public static int ILogB(Complex c) {
            return int.Max(ddouble.ILogB(c.R), ddouble.ILogB(c.I));
        }

        public static Complex Ldexp(Complex c, int n) {
            return (ddouble.Ldexp(c.R, n), ddouble.Ldexp(c.I, n));
        }

        public static bool AlmostReal(Complex c) {
            return double.Abs((double)c.I) <= double.Abs((double)c.R) * 5e-31;
        }

        public static bool AlmostImag(Complex c) {
            return double.Abs((double)c.R) <= double.Abs((double)c.I) * 5e-31;
        }

        public void Deconstruct(out ddouble r, out ddouble i) => (r, i) = (R, I);

        public override string ToString() {
            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            if (R != 0d) {
                if (I == 0d) {
                    return $"{R}";
                }

                return (I > 0d) ? $"{R}+{I}{ImaginaryUnit}" : $"{R}{I}{ImaginaryUnit}";
            }
            else {
                return (I != 0d) ? $"{I}{ImaginaryUnit}" : "0";
            }
        }

        public string ToString([AllowNull] string format, [AllowNull] IFormatProvider formatProvider) {
            if (string.IsNullOrWhiteSpace(format)) {
                return ToString();
            }

            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            if (R != 0d) {
                if (I == 0d) {
                    return $"{R.ToString(format)}";
                }

                return (I > 0d)
                    ? $"{R.ToString(format)}+{I.ToString(format)}{ImaginaryUnit}"
                    : $"{R.ToString(format)}{I.ToString(format)}{ImaginaryUnit}";
            }
            else {
                return (I != 0d) ? $"{I.ToString(format)}{ImaginaryUnit}" : "0";
            }
        }

        public string ToString(string format) {
            return ToString(format, null);
        }
    }
}
