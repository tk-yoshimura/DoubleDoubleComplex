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
            ddouble phi = z.I * ddouble.Ln2;
            ddouble s = ddouble.Pow2(z.R);

            return new Complex(ddouble.Cos(phi) * s, ddouble.Sin(phi) * s);
        }

        public static Complex Pow(ddouble x, Complex z) {
            ddouble phi = z.I * ddouble.Log(x);
            ddouble s = ddouble.Pow(x, z.R);

            return new Complex(ddouble.Cos(phi) * s, ddouble.Sin(phi) * s);
        }
    }
}