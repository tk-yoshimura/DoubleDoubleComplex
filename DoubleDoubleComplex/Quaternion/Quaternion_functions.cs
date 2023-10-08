using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion FromAxisAngle((ddouble x, ddouble y, ddouble z) axis, ddouble angle) {
            ddouble half_angle = angle / 2;
            ddouble c = ddouble.Cos(half_angle), s = ddouble.Sin(half_angle);

            return new Quaternion(c, s * axis.x, s * axis.y, s * axis.z);
        }

        public static Quaternion FromYawPitchRoll(ddouble yaw, ddouble pitch, ddouble roll) {
            ddouble half_yaw = yaw / 2, half_pitch = pitch / 2, half_roll = roll / 2;

            ddouble cy = ddouble.Cos(half_yaw), sy = ddouble.Sin(half_yaw);
            ddouble cp = ddouble.Cos(half_pitch), sp = ddouble.Sin(half_pitch);
            ddouble cr = ddouble.Cos(half_roll), sr = ddouble.Sin(half_roll);

            return new Quaternion(
                cy * cp * cr + sy * sp * sr,
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr
            );
        }

        public static Quaternion VectorPart(Quaternion q) {
            return new Quaternion(ddouble.Zero, q.I, q.J, q.K);
        }

        public static Quaternion RealPart(Quaternion q) {
            return new Quaternion(q.R, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

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

        public static Quaternion Exp(Quaternion q) {
            ddouble vnorm = VectorPart(q).Magnitude;
            ddouble w = ddouble.Sinc(vnorm, normalized: false);

            Quaternion p = new Quaternion(ddouble.Cos(vnorm), w * q.I, w * q.J, w * q.K) * ddouble.Exp(q.R);

            return p;
        }

        public static Quaternion Log(Quaternion q) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;
            ddouble vnorm = VectorPart(q).Magnitude;

            if (IsZero(vnorm)) {
                return Complex.Log(qnorm);
            }

            ddouble w = ddouble.Acos(q.R / qnorm) / vnorm;

            Quaternion p = new(ddouble.Log(qnorm), w * q.I, w * q.J, w * q.K);

            return p;
        }

        public static Quaternion Pow(Quaternion q, ddouble p) {
            if (IsNaN(q) || ddouble.IsNaN(p)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Acos(q.R / qnorm) * p;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Magnitude;
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

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Acos(q.R / qnorm) * n;

            Quaternion vec = VectorPart(q);
            Quaternion vnormal = vec / vec.Magnitude;
            if (IsNaN(vnormal)) {
                return ddouble.Pow(q.R, n);
            }

            ddouble c = ddouble.Cos(phi), s = ddouble.Sin(phi);

            Quaternion r = new Quaternion(c, s * vnormal.I, s * vnormal.J, s * vnormal.K) * ddouble.Pow(qnorm, n);

            return r;
        }

        public static Quaternion Sqrt(Quaternion q) {
            if (IsNaN(q)) {
                return NaN;
            }

            ddouble qnorm = q.Magnitude;

            ddouble phi = ddouble.Acos(q.R / qnorm) / 2;

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

            ddouble phi = ddouble.Acos(q.R / qnorm) / 3;

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
