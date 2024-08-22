using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

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

            if (u == 1.0) {
                return ddouble.Tan(z.R);
            }

            ddouble n = 1d + u * (2d * ddouble.Cos(2d * z.R) + u);

            if (ddouble.Abs(n) > 0.00390625) {
                ddouble r = 2d * u * ddouble.Sin(2d * z.R) / n;
                ddouble i = (u + 1d) * (u - 1d) / n;
                Complex c = (z.I > 0d) ? (r, -i) : (r, i);

                return c;
            }
            else {
                ddouble x = z.R * ddouble.RcpPI - 0.5d;
                x = (x - ddouble.Round(x)) * ddouble.PI;

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

        public static Complex TanPI(Complex z) {
            ddouble u = ddouble.Exp(-ddouble.Abs(2d * z.I * ddouble.PI));

            if (u == 1.0) {
                return ddouble.TanPI(z.R);
            }

            ddouble n = 1d + u * (2d * ddouble.CosPI(2d * z.R) + u);

            if (ddouble.Abs(n) > 0.00390625) {
                ddouble r = 2d * u * ddouble.SinPI(2d * z.R) / n;
                ddouble i = (u + 1d) * (u - 1d) / n;
                Complex c = (z.I > 0d) ? (r, -i) : (r, i);

                return c;
            }
            else {
                ddouble x = z.R - 0.5d;
                x = (x - ddouble.Round(x)) * ddouble.PI;

                Complex w = (x, z.I * ddouble.PI), w2 = w * w;

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