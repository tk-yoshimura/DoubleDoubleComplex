using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Sin(Quaternion q) {
            ddouble vnorm = VectorPart(q).Magnitude;
            ddouble w = ddouble.Cos(q.R) * ddouble.Sinhc(vnorm);

            Quaternion p = new(ddouble.Sin(q.R) * ddouble.Cosh(vnorm), w * q.I, w * q.J, w * q.K);

            return p;
        }

        public static Quaternion Cos(Quaternion q) {
            ddouble vnorm = VectorPart(q).Magnitude;
            ddouble w = -ddouble.Sin(q.R) * ddouble.Sinhc(vnorm);

            Quaternion p = new(ddouble.Cos(q.R) * ddouble.Cosh(vnorm), w * q.I, w * q.J, w * q.K);

            return p;
        }

        public static Quaternion Tan(Quaternion q) {
            ddouble vnorm = VectorPart(q).Magnitude;

            ddouble sinhc = ddouble.Sinhc(vnorm), cosh = ddouble.Cosh(vnorm);
            ddouble cos = ddouble.Cos(q.R), sin = ddouble.Sin(q.R);

            ddouble wcos = cos * sinhc, wsin = -sin * sinhc;

            Quaternion qsin = new(sin * cosh, wcos * q.I, wcos * q.J, wcos * q.K);
            Quaternion qcos = new(cos * cosh, wsin * q.I, wsin * q.J, wsin * q.K);

            return qsin / qcos;
        }
    }
}
