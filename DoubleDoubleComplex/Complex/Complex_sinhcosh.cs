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
            return MulMinusI(Tan(MulI(z)));
        }
    }
}