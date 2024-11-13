using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Pow(Complex z, Complex p) {
            if (IsZero(p)) {
                return 1;
            }
            if (IsZero(z)) {
                return Zero;
            }

            ddouble rho = z.Magnitude, theta = z.Phase;
            ddouble phi = p.R * z.Phase + p.I * ddouble.Log(rho);
            ddouble s = ddouble.Pow(rho, p.R) * ddouble.Pow(ddouble.E, -p.I * theta);

            return new Complex(ddouble.Cos(phi) * s, ddouble.Sin(phi) * s);
        }

        public static Complex Pow(Complex z, ddouble p) {
            return FromPolarCoordinates(ddouble.Pow(z.Magnitude, p), z.Phase * p);
        }

        public static Complex Pow(Complex z, long n) {
            return FromPolarCoordinates(ddouble.Pow(z.Magnitude, n), z.Phase * n);
        }

        public static Complex Pow2(Complex z) {
            ddouble phi = z.I * Consts.Pow.Ln2RcpPi;
            ddouble s = ddouble.Pow2(z.R);

            return new Complex(ddouble.CosPi(phi) * s, ddouble.SinPi(phi) * s);
        }

        public static Complex Pow(ddouble x, Complex z) {
            ddouble phi = z.I * ddouble.Log(x);
            ddouble s = ddouble.Pow(x, z.R);

            return new Complex(ddouble.Cos(phi) * s, ddouble.Sin(phi) * s);
        }

        internal static partial class Consts {
            public static class Pow {
                public static readonly ddouble Ln2RcpPi = (+1, -3, 0xE1EE4C7BF4B4DDD9uL, 0x6C10B05CABFDE7BBuL);
            }
        }
    }
}