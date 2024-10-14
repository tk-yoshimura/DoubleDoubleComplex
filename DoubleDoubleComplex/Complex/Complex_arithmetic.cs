using DoubleDouble;
using System.Numerics;

namespace DoubleDoubleComplex {

    public partial class Complex :
        IAdditionOperators<Complex, Complex, Complex>,
        ISubtractionOperators<Complex, Complex, Complex>,
        IMultiplyOperators<Complex, Complex, Complex>,
        IDivisionOperators<Complex, Complex, Complex>,

        IUnaryPlusOperators<Complex, Complex>,
        IUnaryNegationOperators<Complex, Complex>,

        IEqualityOperators<Complex, Complex, bool> {

        public static Complex operator +(Complex c) {
            return c;
        }

        public static Complex operator -(Complex c) {
            return new(-c.R, -c.I);
        }

        public static Complex operator +(Complex a, Complex b) {
            return new(a.R + b.R, a.I + b.I);
        }

        public static Complex operator +(Complex a, ddouble b) {
            return new(a.R + b, a.I);
        }

        public static Complex operator +(ddouble a, Complex b) {
            return new(a + b.R, b.I);
        }

        public static Complex operator -(Complex a, Complex b) {
            return new(a.R - b.R, a.I - b.I);
        }

        public static Complex operator -(Complex a, ddouble b) {
            return new(a.R - b, a.I);
        }

        public static Complex operator -(ddouble a, Complex b) {
            return new(a - b.R, -b.I);
        }

        public static Complex operator *(Complex a, Complex b) {
            return new(a.R * b.R - a.I * b.I, a.I * b.R + a.R * b.I);
        }

        public static Complex operator *(Complex a, ddouble b) {
            return new(a.R * b, a.I * b);
        }

        public static Complex operator *(ddouble a, Complex b) {
            return new(a * b.R, a * b.I);
        }

        public static Complex operator /(Complex a, Complex b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble s = 1d / b.Norm;

            return new((a.R * b.R + a.I * b.I) * s, (a.I * b.R - a.R * b.I) * s);
        }

        public static Complex operator /(ddouble a, Complex b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (ddouble.Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble s = a / b.Norm;

            return new(b.R * s, -b.I * s);
        }

        public static Complex operator /(Complex a, ddouble b) {
            return a * (1d / b);
        }

        public static Complex Inverse(Complex z) {
            int exp = 0;
            if (IsFinite(z) && !IsZero(z)) {
                exp = ILogB(z);
                z = Ldexp(z, -exp);
            }

            ddouble s = 1d / z.Norm;

            return Ldexp(new(z.R * s, -z.I * s), -exp);
        }

        private static Complex MulI(Complex z) {
            return new(-z.I, z.R);
        }

        private static Complex MulMinusI(Complex z) {
            return new(z.I, -z.R);
        }
    }
}
