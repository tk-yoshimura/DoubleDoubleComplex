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

        public static Complex SinPI(Complex z) {
            ddouble i_pi = z.I * ddouble.PI;

            return new Complex(
                ddouble.SinPI(z.R) * ddouble.Cosh(i_pi),
                ddouble.CosPI(z.R) * ddouble.Sinh(i_pi)
            );
        }

        public static Complex Cos(Complex z) {
            return new Complex(
                ddouble.Cos(z.R) * ddouble.Cosh(z.I),
                -ddouble.Sin(z.R) * ddouble.Sinh(z.I)
            );
        }

        public static Complex CosPI(Complex z) {
            ddouble i_pi = z.I * ddouble.PI;

            return new Complex(
                ddouble.CosPI(z.R) * ddouble.Cosh(i_pi),
                -ddouble.SinPI(z.R) * ddouble.Sinh(i_pi)
            );
        }

        public static Complex Tan(Complex z) {
            ddouble u = ddouble.Exp(-ddouble.Abs(2d * z.I));
            ddouble norm = 1d + u * (2d * ddouble.Cos(2d * z.R) + u);

            Complex c = new Complex(2d * u * ddouble.Sin(2d * z.R), (u + 1d) * (u - 1d)) / norm;

            if (z.I > 0d) {
                c = Conjugate(c);
            }

            return c; 
        }

        public static Complex TanPI(Complex z) {
            ddouble u = ddouble.Exp(-ddouble.Abs(2d * z.I * ddouble.PI));
            ddouble norm = 1d + u * (2d * ddouble.CosPI(2d * z.R) + u);

            Complex c = new Complex(2d * u * ddouble.SinPI(2d * z.R), (u + 1d) * (u - 1d)) / norm;

            if (z.I > 0d) {
                c = Conjugate(c);
            }

            return c;
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
            ddouble r_sinh = ddouble.Sinh(z.R), r_cosh = ddouble.Cosh(z.R);
            ddouble i_sin = ddouble.Sin(z.I), i_cos = ddouble.Cos(z.I);

            Complex s = new(r_sinh * i_cos, r_cosh * i_sin);
            Complex c = new(r_cosh * i_cos, r_sinh * i_sin);

            return s / c;
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

        public static Complex Sqrt(Complex z) {
            return FromPolarCoordinates(ddouble.Sqrt(z.Magnitude), z.Phase / 2);
        }

        public static Complex Cbrt(Complex z) {
            return FromPolarCoordinates(ddouble.Cbrt(z.Magnitude), z.Phase / 3);
        }

        public static Complex RootN(Complex z, int n) {
            return FromPolarCoordinates(ddouble.RootN(z.Magnitude, n), z.Phase / n);
        }
    }
}