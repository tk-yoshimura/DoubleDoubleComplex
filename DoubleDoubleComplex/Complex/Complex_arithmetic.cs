using System;
using System.Diagnostics;

namespace DoubleDouble.Complex {

    public partial class Complex {
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
            return new(a.R * b.R - a.I * b.I, a.R * b.I + a.I * b.R);
        }

        public static Complex operator *(Complex a, ddouble b) {
            return new(a.R * b, a.I * b);
        }

        public static Complex operator *(ddouble a, Complex b) {
            return new(a * b.R, a * b.I);
        }
    }
}
