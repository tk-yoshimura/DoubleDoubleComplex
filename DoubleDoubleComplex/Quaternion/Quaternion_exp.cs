using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Exp(Quaternion q) {
            ddouble vnorm = VectorPart(q).Norm;
            ddouble w = ddouble.Sinc(vnorm, normalized: false);

            Quaternion p = new Quaternion(ddouble.Cos(vnorm), w * q.I, w * q.J, w * q.K) * ddouble.Exp(q.R);

            return p;
        }
    }
}
