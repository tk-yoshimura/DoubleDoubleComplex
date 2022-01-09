using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex FromPolarCoordinates(ddouble magnitude, ddouble phase) {
            return new Complex(ddouble.Cos(phase) * magnitude, ddouble.Sin(phase) * magnitude);
        }

        public static Complex Sin(Complex z) {
            return new Complex(
                ddouble.Sin(z.R) * ddouble.Cosh(z.I),
                ddouble.Cos(z.R) * ddouble.Sinh(z.I)
            );
        }

        public static Complex Cos(Complex z) {
            return new Complex(
                ddouble.Cos(z.R) * ddouble.Cosh(z.I),
                -ddouble.Sin(z.R) * ddouble.Sinh(z.I)
            );
        }

        public static Complex Tan(Complex z) {
            return Sin(z) / Cos(z);
        }

        public static Complex Sinh(Complex z) {
            return new Complex(
                ddouble.Sinh(z.R) * ddouble.Cos(z.I),
                ddouble.Cosh(z.R) * ddouble.Sin(z.I)
            );
        }

        public static Complex Cosh(Complex z) {
            return new Complex(
                ddouble.Cosh(z.R) * ddouble.Cos(z.I),
                ddouble.Sinh(z.R) * ddouble.Sin(z.I)
            );
        }

        public static Complex Tanh(Complex z) {
            return Sinh(z) / Cosh(z);
        }

        public static Complex Asin(Complex z) {
            return -ImaginaryOne * Log(ImaginaryOne * z + Sqrt(1d - z * z));
        }

        public static Complex Acos(Complex z) {
            return -ImaginaryOne * Log(z + ImaginaryOne * Sqrt(1d - z * z));
        }

        public static Complex Atan(Complex z) {
            return ImaginaryOne / 2 * (Log(One - ImaginaryOne * z) - Log(One + ImaginaryOne * z));
        }

        public static Complex Log(Complex z) {
            return new Complex(
                ddouble.Log(z.Magnitude),
                z.Phase
            );
        }

        public static Complex Log2(Complex z) {
            return new Complex(
                ddouble.Log2(z.Magnitude),
                z.Phase * ddouble.LbE
            );
        }

        public static Complex Log10(Complex z) {
            return new Complex(
                ddouble.Log10(z.Magnitude),
                z.Phase / ddouble.Log(10)
            );
        }

        public static Complex Log(Complex z, ddouble b) {
            return Log(z) / ddouble.Log(b);
        }

        public static Complex Exp(Complex z) {
            return new Complex(ddouble.Cos(z.I), ddouble.Sin(z.I)) * ddouble.Exp(z.R);
        }

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

        public static Complex Sqrt(Complex z) {
            return FromPolarCoordinates(ddouble.Sqrt(z.Magnitude), z.Phase / 2);
        }

        public static Complex Cbrt(Complex z) {
            return FromPolarCoordinates(ddouble.Cbrt(z.Magnitude), z.Phase / 3);
        }
    }
}