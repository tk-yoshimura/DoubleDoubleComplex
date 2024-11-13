using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Sqrt(Quaternion q) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Ldexp(ddouble.Acos(q.R / qnorm), -1);

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Magnitude;
            if (IsNaN(vnormal)) {
                return Complex.Sqrt(q.R);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.Sqrt(qnorm);

            return r;
        }

        public static Quaternion Cbrt(Quaternion q) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Acos(q.R / qnorm) / 3d;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Magnitude;
            if (IsNaN(vnormal)) {
                return ddouble.Cbrt(q.R);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.Cbrt(qnorm);

            return r;
        }

        public static Quaternion RootN(Quaternion q, int n) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Acos(q.R / qnorm) / n;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Magnitude;
            if (IsNaN(vnormal)) {
                return ((n & 1) == 0) ? Complex.RootN(q.R, n) : ddouble.RootN(q.R, n);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.RootN(qnorm, n);

            return r;
        }
    }
}
