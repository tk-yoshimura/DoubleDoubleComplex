using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

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
    }
}