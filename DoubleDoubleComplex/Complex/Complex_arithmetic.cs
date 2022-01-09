
using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

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
            ddouble s = 1d / b.Norm;

            return new((a.R * b.R + a.I * b.I) * s, (a.I * b.R - a.R * b.I) * s);
        }

        public static Complex operator /(ddouble a, Complex b) {
            ddouble s = a / b.Norm;

            return new(b.R * s, -b.I * s);
        }

        public static Complex operator /(Complex a, ddouble b) {
            return a * (1d / b);
        }
    }
}
