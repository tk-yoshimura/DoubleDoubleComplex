
using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion operator +(Quaternion q) {
            return q;
        }

        public static Quaternion operator -(Quaternion q) {
            return new(-q.R, -q.I, -q.J, -q.K);
        }

        public static Quaternion operator +(Quaternion a, Quaternion b) {
            return new(a.R + b.R, a.I + b.I, a.J + b.J, a.K + b.K);
        }

        public static Quaternion operator +(Quaternion a, ddouble b) {
            return new(a.R + b, a.I, a.J, a.K);
        }

        public static Quaternion operator +(ddouble a, Quaternion b) {
            return new(a + b.R, b.I, b.J, b.K);
        }

        public static Quaternion operator -(Quaternion a, Quaternion b) {
            return new(a.R - b.R, a.I - b.I, a.J - b.J, a.K - b.K);
        }

        public static Quaternion operator -(Quaternion a, ddouble b) {
            return new(a.R - b, a.I, a.J, a.K);
        }

        public static Quaternion operator -(ddouble a, Quaternion b) {
            return new(a - b.R, -b.I, -b.J, -b.K);
        }

        public static Quaternion operator *(Quaternion a, Quaternion b) {
            return new(
                a.R * b.R - a.I * b.I - a.J * b.J - a.K * b.K,
                a.I * b.R + a.R * b.I - a.K * b.J + a.J * b.K,
                a.J * b.R + a.K * b.I + a.R * b.J - a.I * b.K,
                a.K * b.R - a.J * b.I + a.I * b.J + a.R * b.K
            );
        }

        public static Quaternion operator *(Quaternion a, ddouble b) {
            return new(a.R * b, a.I * b, a.J * b, a.K * b);
        }

        public static Quaternion operator *(ddouble a, Quaternion b) {
            return new(a * b.R, a * b.I, a * b.J, a * b.K);
        }

        public static Quaternion operator /(Quaternion a, Quaternion b) {
            ddouble s = 1d / b.Norm;

            return new(
                (a.R * b.R + a.I * b.I + a.J * b.J + a.K * b.K) * s,
                (a.I * b.R - a.R * b.I + a.K * b.J - a.J * b.K) * s,
                (a.J * b.R - a.K * b.I - a.R * b.J + a.I * b.K) * s,
                (a.K * b.R + a.J * b.I - a.I * b.J - a.R * b.K) * s
            );
        }

        public static Quaternion operator /(ddouble a, Quaternion b) {
            ddouble s = a / b.Norm;

            return new(b.R * s, -b.I * s, -b.J * s, -b.K * s);
        }

        public static Quaternion operator /(Quaternion a, ddouble b) {
            return a * (1d / b);
        }

        public static Quaternion Inverse(Quaternion q) {
            ddouble s = 1d / q.Norm;

            return new(q.R * s, -q.I * s, -q.J * s, -q.K * s);
        }
    }
}
