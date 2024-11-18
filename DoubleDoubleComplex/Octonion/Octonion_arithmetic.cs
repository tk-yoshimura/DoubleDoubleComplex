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
            return new(-o.R, -o.I, -o.J, -o.K, -o.W, -o.X, -o.Y, -o.Z);
        }

        public static Octonion operator +(Octonion a, Octonion b) {
            return new(a.R + b.R, a.I + b.I, a.J + b.J, a.K + b.K, a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Octonion operator +(Octonion a, ddouble b) {
            return new(a.R + b, a.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(Octonion a, double b) {
            return new(a.R + b, a.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(Octonion a, Complex b) {
            return new(a.R + b.R, a.I + b.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(Octonion a, Quaternion b) {
            return new(a.R + b.R, a.I + b.I, a.J + b.J, a.K + b.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator +(ddouble a, Octonion b) {
            return new(a + b.R, b.I, b.J, b.K, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator +(double a, Octonion b) {
            return new(a + b.R, b.I, b.J, b.K, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator +(Complex a, Octonion b) {
            return new(a.R + b.R, a.I + b.I, b.J, b.K, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator +(Quaternion a, Octonion b) {
            return new(a.R + b.R, a.I + b.I, a.J + b.J, a.K + b.K, b.W, b.X, b.Y, b.Z);
        }

        public static Octonion operator -(Octonion a, Octonion b) {
            return new(a.R - b.R, a.I - b.I, a.J - b.J, a.K - b.K, a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Octonion operator -(Octonion a, ddouble b) {
            return new(a.R - b, a.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(Octonion a, double b) {
            return new(a.R - b, a.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(Octonion a, Complex b) {
            return new(a.R - b.R, a.I - b.I, a.J, a.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(Octonion a, Quaternion b) {
            return new(a.R - b.R, a.I - b.I, a.J - b.J, a.K - b.K, a.W, a.X, a.Y, a.Z);
        }

        public static Octonion operator -(ddouble a, Octonion b) {
            return new(a - b.R, -b.I, -b.J, -b.K, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator -(double a, Octonion b) {
            return new(a - b.R, -b.I, -b.J, -b.K, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator -(Complex a, Octonion b) {
            return new(a.R - b.R, a.I - b.I, -b.J, -b.K, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator -(Quaternion a, Octonion b) {
            return new(a.R - b.R, a.I - b.I, a.J - b.J, a.K - b.K, -b.W, -b.X, -b.Y, -b.Z);
        }

        public static Octonion operator *(Octonion a, Octonion b) {
            ddouble r = a.R * b.R - a.I * b.I - a.J * b.J - a.K * b.K - a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
            ddouble i = a.R * b.I + a.I * b.R + a.J * b.K - a.K * b.J + a.W * b.X - a.X * b.W - a.Y * b.Z + a.Z * b.Y;
            ddouble j = a.R * b.J - a.I * b.K + a.J * b.R + a.K * b.I + a.W * b.Y + a.X * b.Z - a.Y * b.W - a.Z * b.X;
            ddouble k = a.R * b.K + a.I * b.J - a.J * b.I + a.K * b.R + a.W * b.Z - a.X * b.Y + a.Y * b.X - a.Z * b.W;
            ddouble w = a.R * b.W - a.I * b.X - a.J * b.Y - a.K * b.Z + a.W * b.R + a.X * b.I + a.Y * b.J + a.Z * b.K;
            ddouble x = a.R * b.X + a.I * b.W - a.J * b.Z + a.K * b.Y - a.W * b.I + a.X * b.R - a.Y * b.K + a.Z * b.J;
            ddouble y = a.R * b.Y + a.I * b.Z + a.J * b.W - a.K * b.X - a.W * b.J + a.X * b.K + a.Y * b.R - a.Z * b.I;
            ddouble z = a.R * b.Z - a.I * b.Y + a.J * b.X + a.K * b.W - a.W * b.K - a.X * b.J + a.Y * b.I + a.Z * b.R;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Octonion a, ddouble b) {
            ddouble r = a.R * b;
            ddouble i = a.I * b;
            ddouble j = a.J * b;
            ddouble k = a.K * b;
            ddouble w = a.W * b;
            ddouble x = a.X * b;
            ddouble y = a.Y * b;
            ddouble z = a.Z * b;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Octonion a, double b) {
            ddouble r = a.R * b;
            ddouble i = a.I * b;
            ddouble j = a.J * b;
            ddouble k = a.K * b;
            ddouble w = a.W * b;
            ddouble x = a.X * b;
            ddouble y = a.Y * b;
            ddouble z = a.Z * b;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(ddouble a, Octonion b) {
            ddouble r = a * b.R;
            ddouble i = a * b.I;
            ddouble j = a * b.J;
            ddouble k = a * b.K;
            ddouble w = a * b.W;
            ddouble x = a * b.X;
            ddouble y = a * b.Y;
            ddouble z = a * b.Z;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(double a, Octonion b) {
            ddouble r = a * b.R;
            ddouble i = a * b.I;
            ddouble j = a * b.J;
            ddouble k = a * b.K;
            ddouble w = a * b.W;
            ddouble x = a * b.X;
            ddouble y = a * b.Y;
            ddouble z = a * b.Z;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Octonion a, Complex b) {
            ddouble r = a.R * b.R - a.I * b.I;
            ddouble i = a.R * b.I + a.I * b.R;
            ddouble j = a.J * b.R + a.K * b.I;
            ddouble k = -a.J * b.I + a.K * b.R;
            ddouble w = a.W * b.R + a.X * b.I;
            ddouble x = -a.W * b.I + a.X * b.R;
            ddouble y = a.Y * b.R - a.Z * b.I;
            ddouble z = a.Y * b.I + a.Z * b.R;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Complex a, Octonion b) {
            ddouble r = a.R * b.R - a.I * b.I;
            ddouble i = a.R * b.I + a.I * b.R;
            ddouble j = a.R * b.J - a.I * b.K;
            ddouble k = a.R * b.K + a.I * b.J;
            ddouble w = a.R * b.W - a.I * b.X;
            ddouble x = a.R * b.X + a.I * b.W;
            ddouble y = a.R * b.Y + a.I * b.Z;
            ddouble z = a.R * b.Z - a.I * b.Y;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Octonion a, Quaternion b) {
            ddouble r = a.R * b.R - a.I * b.I - a.J * b.J - a.K * b.K;
            ddouble i = a.R * b.I + a.I * b.R + a.J * b.K - a.K * b.J;
            ddouble j = a.R * b.J - a.I * b.K + a.J * b.R + a.K * b.I;
            ddouble k = a.R * b.K + a.I * b.J - a.J * b.I + a.K * b.R;
            ddouble w = +a.W * b.R + a.X * b.I + a.Y * b.J + a.Z * b.K;
            ddouble x = -a.W * b.I + a.X * b.R - a.Y * b.K + a.Z * b.J;
            ddouble y = -a.W * b.J + a.X * b.K + a.Y * b.R - a.Z * b.I;
            ddouble z = -a.W * b.K - a.X * b.J + a.Y * b.I + a.Z * b.R;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator *(Quaternion a, Octonion b) {
            ddouble r = a.R * b.R - a.I * b.I - a.J * b.J - a.K * b.K;
            ddouble i = a.R * b.I + a.I * b.R + a.J * b.K - a.K * b.J;
            ddouble j = a.R * b.J - a.I * b.K + a.J * b.R + a.K * b.I;
            ddouble k = a.R * b.K + a.I * b.J - a.J * b.I + a.K * b.R;
            ddouble w = a.R * b.W - a.I * b.X - a.J * b.Y - a.K * b.Z;
            ddouble x = a.R * b.X + a.I * b.W - a.J * b.Z + a.K * b.Y;
            ddouble y = a.R * b.Y + a.I * b.Z + a.J * b.W - a.K * b.X;
            ddouble z = a.R * b.Z - a.I * b.Y + a.J * b.X + a.K * b.W;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(Octonion a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = 1d / b.SquareNorm;

            ddouble r = (+a.R * b.R + a.I * b.I + a.J * b.J + a.K * b.K + a.W * b.W + a.X * b.X + a.Y * b.Y + a.Z * b.Z) * n;
            ddouble i = (-a.R * b.I + a.I * b.R - a.J * b.K + a.K * b.J - a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y) * n;
            ddouble j = (-a.R * b.J + a.I * b.K + a.J * b.R - a.K * b.I - a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X) * n;
            ddouble k = (-a.R * b.K - a.I * b.J + a.J * b.I + a.K * b.R - a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W) * n;
            ddouble w = (-a.R * b.W + a.I * b.X + a.J * b.Y + a.K * b.Z + a.W * b.R - a.X * b.I - a.Y * b.J - a.Z * b.K) * n;
            ddouble x = (-a.R * b.X - a.I * b.W + a.J * b.Z - a.K * b.Y + a.W * b.I + a.X * b.R + a.Y * b.K - a.Z * b.J) * n;
            ddouble y = (-a.R * b.Y - a.I * b.Z - a.J * b.W + a.K * b.X + a.W * b.J - a.X * b.K + a.Y * b.R + a.Z * b.I) * n;
            ddouble z = (-a.R * b.Z + a.I * b.Y - a.J * b.X - a.K * b.W + a.W * b.K + a.X * b.J - a.Y * b.I + a.Z * b.R) * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(ddouble a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (ddouble.Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = a / b.SquareNorm;

            ddouble r = +b.R * n;
            ddouble i = -b.I * n;
            ddouble j = -b.J * n;
            ddouble k = -b.K * n;
            ddouble w = -b.W * n;
            ddouble x = -b.X * n;
            ddouble y = -b.Y * n;
            ddouble z = -b.Z * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(double a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (double.ScaleB(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = a / b.SquareNorm;

            ddouble r = +b.R * n;
            ddouble i = -b.I * n;
            ddouble j = -b.J * n;
            ddouble k = -b.K * n;
            ddouble w = -b.W * n;
            ddouble x = -b.X * n;
            ddouble y = -b.Y * n;
            ddouble z = -b.Z * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(Octonion a, ddouble b) {
            return a * (1d / b);
        }

        public static Octonion operator /(Octonion a, double b) {
            return (a.R / b, a.I / b, a.J / b, a.K / b, a.W / b, a.X / b, a.Y / b, a.Z / b);
        }

        public static Octonion operator /(Octonion a, Complex b) {
            if (Complex.IsFinite(b) && !Complex.IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Complex.Ldexp(b, -exp));
            }

            ddouble n = 1d / b.SquareNorm;

            ddouble r = (+a.R * b.R + a.I * b.I) * n;
            ddouble i = (-a.R * b.I + a.I * b.R) * n;
            ddouble j = (a.J * b.R - a.K * b.I) * n;
            ddouble k = (a.J * b.I + a.K * b.R) * n;
            ddouble w = (a.W * b.R - a.X * b.I) * n;
            ddouble x = (a.W * b.I + a.X * b.R) * n;
            ddouble y = (+a.Y * b.R + a.Z * b.I) * n;
            ddouble z = (-a.Y * b.I + a.Z * b.R) * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(Complex a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Complex.Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = 1d / b.SquareNorm;

            ddouble r = (+a.R * b.R + a.I * b.I) * n;
            ddouble i = (-a.R * b.I + a.I * b.R) * n;
            ddouble j = (-a.R * b.J + a.I * b.K) * n;
            ddouble k = (-a.R * b.K - a.I * b.J) * n;
            ddouble w = (-a.R * b.W + a.I * b.X) * n;
            ddouble x = (-a.R * b.X - a.I * b.W) * n;
            ddouble y = (-a.R * b.Y - a.I * b.Z) * n;
            ddouble z = (-a.R * b.Z + a.I * b.Y) * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(Octonion a, Quaternion b) {
            if (Quaternion.IsFinite(b) && !Quaternion.IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Ldexp(a, -exp), Quaternion.Ldexp(b, -exp));
            }

            ddouble n = 1d / b.SquareNorm;

            ddouble r = (+a.R * b.R + a.I * b.I + a.J * b.J + a.K * b.K) * n;
            ddouble i = (-a.R * b.I + a.I * b.R - a.J * b.K + a.K * b.J) * n;
            ddouble j = (-a.R * b.J + a.I * b.K + a.J * b.R - a.K * b.I) * n;
            ddouble k = (-a.R * b.K - a.I * b.J + a.J * b.I + a.K * b.R) * n;
            ddouble w = (a.W * b.R - a.X * b.I - a.Y * b.J - a.Z * b.K) * n;
            ddouble x = (a.W * b.I + a.X * b.R + a.Y * b.K - a.Z * b.J) * n;
            ddouble y = (a.W * b.J - a.X * b.K + a.Y * b.R + a.Z * b.I) * n;
            ddouble z = (a.W * b.K + a.X * b.J - a.Y * b.I + a.Z * b.R) * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion operator /(Quaternion a, Octonion b) {
            if (IsFinite(b) && !IsZero(b)) {
                int exp = ILogB(b);
                (a, b) = (Quaternion.Ldexp(a, -exp), Ldexp(b, -exp));
            }

            ddouble n = 1d / b.SquareNorm;

            ddouble r = (+a.R * b.R + a.I * b.I + a.J * b.J + a.K * b.K) * n;
            ddouble i = (-a.R * b.I + a.I * b.R - a.J * b.K + a.K * b.J) * n;
            ddouble j = (-a.R * b.J + a.I * b.K + a.J * b.R - a.K * b.I) * n;
            ddouble k = (-a.R * b.K - a.I * b.J + a.J * b.I + a.K * b.R) * n;
            ddouble w = (-a.R * b.W + a.I * b.X + a.J * b.Y + a.K * b.Z) * n;
            ddouble x = (-a.R * b.X - a.I * b.W + a.J * b.Z - a.K * b.Y) * n;
            ddouble y = (-a.R * b.Y - a.I * b.Z - a.J * b.W + a.K * b.X) * n;
            ddouble z = (-a.R * b.Z + a.I * b.Y - a.J * b.X - a.K * b.W) * n;

            return new(r, i, j, k, w, x, y, z);
        }

        public static Octonion Inverse(Octonion o) {
            int exp = 0;
            if (IsFinite(o) && !IsZero(o)) {
                exp = ILogB(o);
                o = Ldexp(o, -exp);
            }

            ddouble n = 1d / o.SquareNorm;

            ddouble r = +o.R * n;
            ddouble i = -o.I * n;
            ddouble j = -o.J * n;
            ddouble k = -o.K * n;
            ddouble w = -o.W * n;
            ddouble x = -o.X * n;
            ddouble y = -o.Y * n;
            ddouble z = -o.Z * n;

            return Ldexp(new(r, i, j, k, w, x, y, z), -exp);
        }
    }
}
