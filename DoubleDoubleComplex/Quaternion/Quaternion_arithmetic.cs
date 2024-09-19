using DoubleDouble;
using System.Numerics;

namespace DoubleDoubleComplex {

    public partial class Quaternion :
        IAdditionOperators<Quaternion, Quaternion, Quaternion>,
        ISubtractionOperators<Quaternion, Quaternion, Quaternion>,
        IMultiplyOperators<Quaternion, Quaternion, Quaternion>,
        IDivisionOperators<Quaternion, Quaternion, Quaternion>,

        IUnaryPlusOperators<Quaternion, Quaternion>,
        IUnaryNegationOperators<Quaternion, Quaternion>,

        IEqualityOperators<Quaternion, Quaternion, bool> {

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
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble s = 1d / b.Norm;

            return new(
                (a.R * b.R + a.I * b.I + a.J * b.J + a.K * b.K) * s,
                (a.I * b.R - a.R * b.I + a.K * b.J - a.J * b.K) * s,
                (a.J * b.R - a.K * b.I - a.R * b.J + a.I * b.K) * s,
                (a.K * b.R + a.J * b.I - a.I * b.J - a.R * b.K) * s
            );
        }

        public static Quaternion operator /(ddouble a, Quaternion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (ddouble.Ldexp(a, -exp), Ldexp(b, -exp));
            }
            
            ddouble s = a / b.Norm;

            return new(b.R * s, -b.I * s, -b.J * s, -b.K * s);
        }

        public static Quaternion operator /(Quaternion a, ddouble b) {
            return a * (1d / b);
        }

        public static Quaternion Inverse(Quaternion q) {
            int exp = 0;
            if (IsFinite(q) && !IsZero(q)) {
                exp = ILogB(q);
                q = Ldexp(q, -exp);
            }

            ddouble s = 1d / q.Norm;

            return Ldexp(new(q.R * s, -q.I * s, -q.J * s, -q.K * s), -exp);
        }
    }
}
