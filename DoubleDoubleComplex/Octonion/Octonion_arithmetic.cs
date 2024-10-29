using DoubleDouble;
using System.Numerics;

namespace DoubleDoubleComplex {

    public partial class Octonion :
        IAdditionOperators<Octonion, Octonion, Octonion>,
        ISubtractionOperators<Octonion, Octonion, Octonion>,
        IMultiplyOperators<Octonion, Octonion, Octonion>,
        IDivisionOperators<Octonion, Octonion, Octonion>,

        IUnaryPlusOperators<Octonion, Octonion>,
        IUnaryNegationOperators<Octonion, Octonion>,

        IEqualityOperators<Octonion, Octonion, bool> {

        public static Octonion operator +(Octonion o) {
            return o;
        }

        public static Octonion operator -(Octonion o) {
            return new(-o.R, -o.S, -o.T, -o.U, -o.W, -o.X, -o.Y, -o.Z);
        }

        public static Octonion operator +(Octonion a, Octonion b) {
            return new(a.R + b.R, a.S + b.S, a.T + b.T, a.U + b.U, a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Octonion operator +(Octonion a, ddouble b) {
            return new(a.R + b, a.S, a.T, a.U, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(Octonion a, Complex b) {
            return new(a.R + b.R, a.S + b.I, a.T, a.U, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(Octonion a, Quaternion b) {
            return new(a.R + b.R, a.S + b.I, a.T + b.J, a.U + b.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(ddouble a, Octonion b) {
            return new(a + b.R, b.S, b.T, b.U, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator +(Complex a, Octonion b) {
            return new(a.R + b.R, a.I + b.S, b.T, b.U, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator +(Quaternion a, Octonion b) {
            return new(a.R + b.R, a.I + b.S, a.J + b.T, a.K + b.U, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator -(Octonion a, Octonion b) {
            return new(a.R - b.R, a.S - b.S, a.T - b.T, a.U - b.U, a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Octonion operator -(Octonion a, ddouble b) {
            return new(a.R - b, a.S, a.T, a.U, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(Octonion a, Complex b) {
            return new(a.R - b.R, a.S - b.I, a.T, a.U, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(Octonion a, Quaternion b) {
            return new(a.R - b.R, a.S - b.I, a.T - b.J, a.U - b.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(ddouble a, Octonion b) {
            return new(a - b.R, -b.S, -b.T, -b.U, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator -(Complex a, Octonion b) {
            return new(a.R - b.R, a.I - b.S, -b.T, -b.U, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator -(Quaternion a, Octonion b) {
            return new(a.R - b.R, a.I - b.S, a.J - b.T, a.K - b.U, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator *(Octonion a, Octonion b) {
            ddouble r = a.R * b.R - a.S * b.S - a.T * b.T - a.U * b.U - a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
            ddouble s = a.R * b.S + a.S * b.R + a.T * b.U - a.U * b.T + a.W * b.X - a.X * b.W - a.Y * b.Z + a.Z * b.Y;
            ddouble t = a.R * b.T - a.S * b.U + a.T * b.R + a.U * b.S + a.W * b.Y + a.X * b.Z - a.Y * b.W - a.Z * b.X;
            ddouble u = a.R * b.U + a.S * b.T - a.T * b.S + a.U * b.R + a.W * b.Z - a.X * b.Y + a.Y * b.X - a.Z * b.W;
            ddouble w = a.R * b.W - a.S * b.X - a.T * b.Y - a.U * b.Z + a.W * b.R + a.X * b.S + a.Y * b.T + a.Z * b.U;
            ddouble x = a.R * b.X + a.S * b.W - a.T * b.Z + a.U * b.Y - a.W * b.S + a.X * b.R - a.Y * b.U + a.Z * b.T;
            ddouble y = a.R * b.Y + a.S * b.Z + a.T * b.W - a.U * b.X - a.W * b.T + a.X * b.U + a.Y * b.R - a.Z * b.S;
            ddouble z = a.R * b.Z - a.S * b.Y + a.T * b.X + a.U * b.W - a.W * b.U - a.X * b.T + a.Y * b.S + a.Z * b.R;

            return new(r, s, t, u, w, x, y, z);
        }

        public static Octonion operator *(Octonion a, ddouble b) {
            ddouble r = a.R * b;
            ddouble s = a.S * b;
            ddouble t = a.T * b;
            ddouble u = a.U * b;
            ddouble w = a.W * b;
            ddouble x = a.X * b;
            ddouble y = a.Y * b;
            ddouble z = a.Z * b;

            return new(r, s, t, u, w, x, y, z);
        }

        public static Octonion operator *(ddouble a, Octonion b) {
            ddouble r = a * b.R;
            ddouble s = a * b.S;
            ddouble t = a * b.T;
            ddouble u = a * b.U;
            ddouble w = a * b.W;
            ddouble x = a * b.X;
            ddouble y = a * b.Y;
            ddouble z = a * b.Z;

            return new(r, s, t, u, w, x, y, z);
        }

        public static Octonion operator /(Octonion a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = 1d / b.Norm;

            ddouble r = (+a.R * b.R + a.S * b.S + a.T * b.T + a.U * b.U + a.W * b.W + a.X * b.X + a.Y * b.Y + a.Z * b.Z) * n;
            ddouble s = (-a.R * b.S + a.S * b.R - a.T * b.U + a.U * b.T - a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y) * n;
            ddouble t = (-a.R * b.T + a.S * b.U + a.T * b.R - a.U * b.S - a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X) * n;
            ddouble u = (-a.R * b.U - a.S * b.T + a.T * b.S + a.U * b.R - a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W) * n;
            ddouble w = (-a.R * b.W + a.S * b.X + a.T * b.Y + a.U * b.Z + a.W * b.R - a.X * b.S - a.Y * b.T - a.Z * b.U) * n;
            ddouble x = (-a.R * b.X - a.S * b.W + a.T * b.Z - a.U * b.Y + a.W * b.S + a.X * b.R + a.Y * b.U - a.Z * b.T) * n;
            ddouble y = (-a.R * b.Y - a.S * b.Z - a.T * b.W + a.U * b.X + a.W * b.T - a.X * b.U + a.Y * b.R + a.Z * b.S) * n;
            ddouble z = (-a.R * b.Z + a.S * b.Y - a.T * b.X - a.U * b.W + a.W * b.U + a.X * b.T - a.Y * b.S + a.Z * b.R) * n;

            return new(r, s, t, u, w, x, y, z);
        }

        public static Octonion operator /(ddouble a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (ddouble.Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = a / b.Norm;

            ddouble r = +b.R * n;
            ddouble s = -b.S * n;
            ddouble t = -b.T * n;
            ddouble u = -b.U * n;
            ddouble w = -b.W * n;
            ddouble x = -b.X * n;
            ddouble y = -b.Y * n;
            ddouble z = -b.Z * n;

            return new(r, s, t, u, w, x, y, z);
        }

        public static Octonion operator /(Octonion a, ddouble b) {
            return a * (1d / b);
        }

        public static Octonion Inverse(Octonion o) {
            int exp = 0;
            if (IsFinite(o) && !IsZero(o)) {
                exp = ILogB(o);
                o = Ldexp(o, -exp);
            }

            ddouble n = 1d / o.Norm;

            ddouble r = +o.R * n;
            ddouble s = -o.S * n;
            ddouble t = -o.T * n;
            ddouble u = -o.U * n;
            ddouble w = -o.W * n;
            ddouble x = -o.X * n;
            ddouble y = -o.Y * n;
            ddouble z = -o.Z * n;

            return Ldexp(new(r, s, t, u, w, x, y, z), -exp);
        }
    }
}
