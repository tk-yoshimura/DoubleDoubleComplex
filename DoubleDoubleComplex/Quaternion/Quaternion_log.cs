using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Log(Quaternion q) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Norm;
            ddouble vnorm = VectorPart(q).Norm;

            if (IsZero(vnorm)) {
                return Complex.Log(qnorm);
            }

            ddouble w = ddouble.Acos(q.R / qnorm) / vnorm;

            Quaternion p = new(ddouble.Log(qnorm), w * q.I, w * q.J, w * q.K);

            return p;
        }
    }
}
