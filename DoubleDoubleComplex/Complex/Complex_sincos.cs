using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Sin(Complex z) {
            return new Complex(
                ddouble.Sin(z.R) * ddouble.Cosh(z.I),
                ddouble.Cos(z.R) * ddouble.Sinh(z.I)
            );
        }

        public static Complex SinPi(Complex z) {
            ddouble i_pi = z.I * ddouble.Pi;

            return new Complex(
                ddouble.SinPi(z.R) * ddouble.Cosh(i_pi),
                ddouble.CosPi(z.R) * ddouble.Sinh(i_pi)
            );
        }

        public static Complex Cos(Complex z) {
            return new Complex(
                ddouble.Cos(z.R) * ddouble.Cosh(z.I),
                -ddouble.Sin(z.R) * ddouble.Sinh(z.I)
            );
        }

        public static Complex CosPi(Complex z) {
            ddouble i_pi = z.I * ddouble.Pi;

            return new Complex(
                ddouble.CosPi(z.R) * ddouble.Cosh(i_pi),
                -ddouble.SinPi(z.R) * ddouble.Sinh(i_pi)
            );
        }

        public static Complex Tan(Complex z) {
            ddouble u = ddouble.Exp(-ddouble.Abs(ddouble.Ldexp(z.I, 1)));

            if (u == 1.0) {
                return ddouble.Tan(z.R);
            }

            ddouble n = 1d + u * (ddouble.Ldexp(ddouble.Cos(ddouble.Ldexp(z.R, 1)), 1) + u);

            if (ddouble.Abs(n) > 0.00390625) {
                ddouble r = ddouble.Ldexp(u * ddouble.Sin(ddouble.Ldexp(z.R, 1)) / n, 1);
                ddouble i = (u + 1d) * (u - 1d) / n;
                Complex c = ddouble.IsPositive(z.I) ? (r, -i) : (r, i);

                return c;
            }
            else {
                ddouble x = z.R * ddouble.RcpPi - 0.5d;
                x = (x - ddouble.Round(x)) * ddouble.Pi;

                Complex w = (x, z.I), w2 = w * w;

                Complex c = -1856156927625d /
                    (w * (1856156927625d + w2 * (618718975875d
                    + w2 * (247487590350d + w2 * (100173548475d
                    + w2 * (40593202650d + w2 * (16451556030d
                    + w2 * (6667553340d + w2 * (2702257083d
                    + w2 * (1095183522d + w2 * 443861162d))))))))));

                return c;
            }
        }

        public static Complex TanPi(Complex z) {
            ddouble u = ddouble.Exp(-ddouble.Abs(ddouble.Ldexp(z.I * ddouble.Pi, 1)));

            if (u == 1.0) {
                return ddouble.TanPi(z.R);
            }

            ddouble n = 1d + u * (ddouble.Ldexp(ddouble.CosPi(ddouble.Ldexp(z.R, 1)), 1) + u);

            if (ddouble.Abs(n) > 0.00390625) {
                ddouble r = ddouble.Ldexp(u * ddouble.SinPi(ddouble.Ldexp(z.R, 1)) / n, 1);
                ddouble i = (u + 1d) * (u - 1d) / n;
                Complex c = ddouble.IsPositive(z.I) ? (r, -i) : (r, i);

                return c;
            }
            else {
                ddouble x = z.R - 0.5d;
                x = (x - ddouble.Round(x)) * ddouble.Pi;

                Complex w = (x, z.I * ddouble.Pi), w2 = w * w;

                Complex c = -1856156927625d /
                    (w * (1856156927625d + w2 * (618718975875d
                    + w2 * (247487590350d + w2 * (100173548475d
                    + w2 * (40593202650d + w2 * (16451556030d
                    + w2 * (6667553340d + w2 * (2702257083d
                    + w2 * (1095183522d + w2 * 443861162d))))))))));

                return c;
            }
        }
    }
}