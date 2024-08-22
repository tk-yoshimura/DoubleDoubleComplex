using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Exp(Complex z) {
            return new Complex(ddouble.Cos(z.I), ddouble.Sin(z.I)) * ddouble.Exp(z.R);
        }
    }
}