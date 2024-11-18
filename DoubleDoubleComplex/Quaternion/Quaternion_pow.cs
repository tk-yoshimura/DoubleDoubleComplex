using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Pow(Quaternion q, ddouble p) {
            if (IsNaN(q) || ddouble.IsNaN(p)) {
                return NaN;
            }

            ddouble qnorm = q.Norm;

            ddouble phi = ddouble.Acos(q.R / qnorm) * p;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Norm;
            if (IsNaN(vnormal)) {
                return ddouble.Pow(q.R, p);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.Pow(qnorm, p);

            return r;
        }

        public static Quaternion Pow(Quaternion q, long n) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Norm;

            ddouble phi = ddouble.Acos(q.R / qnorm) * n;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Norm;
            if (IsNaN(vnormal)) {
                return ddouble.Pow(q.R, n);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.Pow(qnorm, n);

            return r;
        }
    }
}
