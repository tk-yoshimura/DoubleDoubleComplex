using DoubleDouble;
using System.Diagnostics;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Complex {
        public const char ImaginaryUnit = 'i';

        public readonly ddouble R, I;

        public ddouble Norm => R * R + I * I;

        public ddouble Magnitude => ddouble.Sqrt(Norm);

        public ddouble Phase => ddouble.Atan2(I, R);

        public static Complex Zero { get; } = ddouble.Zero;

        public static Complex NaN { get; } = ddouble.NaN;

        public static Complex One { get; } = (1, 0);
        public static Complex ImaginaryOne { get; } = (0, 1);

        public static Complex Conjugate(Complex c) => new Complex(c.R, -c.I);

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

        public static implicit operator (ddouble r, ddouble i)(Complex v) {
            return (v.R, v.I);
        }

        public static implicit operator Complex(System.Numerics.Complex c) {
            return new Complex(c.Real, c.Imaginary);
        }

        public static explicit operator System.Numerics.Complex(Complex c) {
            return new System.Numerics.Complex((double)c.R, (double)c.I);
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
    }
}
